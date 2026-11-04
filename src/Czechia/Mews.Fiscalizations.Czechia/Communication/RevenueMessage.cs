using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Xml;
using Mews.Fiscalizations.Czechia.Model;

namespace Mews.Fiscalizations.Czechia.Communication;

/// <summary>
/// Builds the signed SOAP envelope of the registered sale data message (Trzba) as described in chapter 5 of the EET 2.0 data interface.
/// </summary>
internal static class RevenueMessage
{
    private const string BodyId = "Body-1";
    private const string TokenId = "X509-1";
    private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:sszzz";

    public static XmlDocument Create(RevenueRecord record, Guid messageUuid, DateTimeOffset sent, bool isFirstAttempt, EetMode mode)
    {
        var document = new XmlDocument { PreserveWhitespace = true };
        var envelope = document.CreateElement("soapenv", "Envelope", Namespaces.Soap);
        var header = document.CreateElement("soapenv", "Header", Namespaces.Soap);
        var body = document.CreateElement("soapenv", "Body", Namespaces.Soap);
        body.SetAttribute("Id", Namespaces.WsUtility, BodyId);
        body.Attributes["Id", Namespaces.WsUtility]!.Prefix = "wsu";

        var trzba = document.CreateElement("eet", "Trzba", Namespaces.Eet);
        var hlavicka = document.CreateElement("eet", "Hlavicka", Namespaces.Eet);
        hlavicka.SetAttribute("uuid_zpravy", messageUuid.ToString("D"));
        hlavicka.SetAttribute("dat_odesl", Format(sent));
        hlavicka.SetAttribute("prvni_zaslani", Format(isFirstAttempt));
        if (mode == EetMode.Verification)
        {
            hlavicka.SetAttribute("overeni", Format(true));
        }

        var data = document.CreateElement("eet", "Data", Namespaces.Eet);
        data.SetAttribute("eic_popl", record.TaxpayerTaxId.Value);
        record.DelegatingTaxpayerTaxId.Match(id => data.SetAttribute("eic_poverujiciho", id.Value));
        if (record.IsOnBehalfOfMultipleTaxpayers)
        {
            data.SetAttribute("povereni_vice_popl", Format(true));
        }
        data.SetAttribute("id_jednotky", record.RegistrationUnitId.ToString());
        data.SetAttribute("id_pokl", record.RegistryId.Value);
        data.SetAttribute("porad_cis", record.BillNumber.Value);
        data.SetAttribute("dat_trzby", Format(record.Created));
        data.SetAttribute("celk_trzba", record.Revenue.Total.ToString());
        record.Revenue.DepositToBeDrawn.Match(v => data.SetAttribute("urceno_cerp_zuct", v.ToString()));
        record.Revenue.DrawnDeposit.Match(v => data.SetAttribute("cerp_zuct", v.ToString()));

        trzba.AppendChild(hlavicka);
        trzba.AppendChild(data);
        body.AppendChild(trzba);
        envelope.AppendChild(header);
        envelope.AppendChild(body);
        document.AppendChild(envelope);
        return document;
    }

    public static XmlDocument Sign(XmlDocument document, Certificate certificate)
    {
        var namespaceManager = new XmlNamespaceManager(document.NameTable);
        namespaceManager.AddNamespace("soapenv", Namespaces.Soap);
        var header = (XmlElement)document.SelectSingleNode("/soapenv:Envelope/soapenv:Header", namespaceManager)!;

        var security = document.CreateElement("wsse", "Security", Namespaces.WsSecurity);
        security.SetAttribute("mustUnderstand", Namespaces.Soap, "1");
        security.Attributes["mustUnderstand", Namespaces.Soap]!.Prefix = "soapenv";
        var token = document.CreateElement("wsse", "BinarySecurityToken", Namespaces.WsSecurity);
        token.SetAttribute("EncodingType", Namespaces.Base64Binary);
        token.SetAttribute("ValueType", Namespaces.X509v3);
        token.SetAttribute("Id", Namespaces.WsUtility, TokenId);
        token.Attributes["Id", Namespaces.WsUtility]!.Prefix = "wsu";
        token.InnerText = Convert.ToBase64String(certificate.X509Certificate.RawData);
        security.AppendChild(token);
        header.AppendChild(security);

        var signedXml = new SignedXmlWithId(document)
        {
            SigningKey = certificate.PrivateKey
        };
        signedXml.SignedInfo!.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
        signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;

        var reference = new Reference($"#{BodyId}") { DigestMethod = SignedXml.XmlDsigSHA256Url };
        reference.AddTransform(new XmlDsigExcC14NTransform());
        signedXml.AddReference(reference);

        var keyInfo = new KeyInfo();
        keyInfo.AddClause(new SecurityTokenReference(TokenId));
        signedXml.KeyInfo = keyInfo;

        signedXml.ComputeSignature();
        security.AppendChild(document.ImportNode(signedXml.GetXml(), deep: true));
        return document;
    }

    internal static string Format(DateTimeOffset value)
    {
        var truncated = new DateTimeOffset(value.Ticks - value.Ticks % TimeSpan.TicksPerSecond, value.Offset);
        return truncated.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
    }

    private static string Format(bool value) => value ? "true" : "false";
}
