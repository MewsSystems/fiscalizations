using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Schema;
using Mews.Fiscalizations.Czechia.Communication;

namespace Mews.Fiscalizations.Czechia.Tests;

public class RevenueMessageTests
{
    private static readonly Certificate Certificate = TestCertificates.Create();

    [Test]
    public void MinimalMessageMatchesOfficialSchema()
    {
        var record = TestData.CreateRecord("CZ00000019");
        var document = RevenueMessage.Create(record, Guid.NewGuid(), DateTimeOffset.Now, isFirstAttempt: true, EetMode.Operational);

        Assert.That(ValidateAgainstSchema(document), Is.Empty);
        var data = SelectTrzba(document, "eet:Data");
        Assert.That(data.HasAttribute("eic_poverujiciho"), Is.False);
        Assert.That(data.HasAttribute("povereni_vice_popl"), Is.False);
        Assert.That(data.HasAttribute("urceno_cerp_zuct"), Is.False);
        Assert.That(data.HasAttribute("cerp_zuct"), Is.False);
        Assert.That(SelectTrzba(document, "eet:Hlavicka").HasAttribute("overeni"), Is.False);
    }

    [Test]
    public void FullMessageMatchesOfficialSchema()
    {
        var uuid = Guid.NewGuid();
        var record = TestData.CreateRecord(
            "CZ683555118",
            total: -187.20m,
            depositToBeDrawn: 25m,
            drawnDeposit: 0m,
            delegatingTaxId: "CZ00000019",
            isOnBehalfOfMultipleTaxpayers: true,
            created: new DateTimeOffset(2027, 6, 9, 5, 25, 28, 999, TestData.CzechSummerOffset)
        );
        var sent = new DateTimeOffset(2027, 1, 9, 4, 25, 28, TimeSpan.FromHours(1));
        var document = RevenueMessage.Create(record, uuid, sent, isFirstAttempt: false, EetMode.Verification);

        Assert.That(ValidateAgainstSchema(document), Is.Empty);
        var header = SelectTrzba(document, "eet:Hlavicka");
        Assert.That(header.GetAttribute("uuid_zpravy"), Is.EqualTo(uuid.ToString()));
        Assert.That(header.GetAttribute("dat_odesl"), Is.EqualTo("2027-01-09T04:25:28+01:00"));
        Assert.That(header.GetAttribute("prvni_zaslani"), Is.EqualTo("false"));
        Assert.That(header.GetAttribute("overeni"), Is.EqualTo("true"));

        var data = SelectTrzba(document, "eet:Data");
        Assert.That(data.GetAttribute("eic_popl"), Is.EqualTo("CZ683555118"));
        Assert.That(data.GetAttribute("eic_poverujiciho"), Is.EqualTo("CZ00000019"));
        Assert.That(data.GetAttribute("povereni_vice_popl"), Is.EqualTo("true"));
        Assert.That(data.GetAttribute("id_jednotky"), Is.EqualTo("243"));
        Assert.That(data.GetAttribute("dat_trzby"), Is.EqualTo("2027-06-09T05:25:28+02:00"));
        Assert.That(data.GetAttribute("celk_trzba"), Is.EqualTo("-187.20"));
        Assert.That(data.GetAttribute("urceno_cerp_zuct"), Is.EqualTo("25.00"));
        Assert.That(data.GetAttribute("cerp_zuct"), Is.EqualTo("0.00"));
    }

    [Test]
    public void SignatureCoversOnlyTheBodyAndVerifies()
    {
        var document = CreateSignedMessage();
        var signedXml = LoadSignature(document);

        Assert.That(signedXml.SignedInfo!.CanonicalizationMethod, Is.EqualTo(SignedXml.XmlDsigExcC14NTransformUrl));
        Assert.That(signedXml.SignedInfo.SignatureMethod, Is.EqualTo(SignedXml.XmlDsigRSASHA256Url));
        Assert.That(signedXml.SignedInfo.References, Has.Count.EqualTo(1));
        var reference = (Reference)signedXml.SignedInfo.References[0]!;
        Assert.That(reference.DigestMethod, Is.EqualTo(SignedXml.XmlDsigSHA256Url));
        Assert.That(reference.TransformChain[0], Is.TypeOf<XmlDsigExcC14NTransform>());

        var body = Select(document, "/soapenv:Envelope/soapenv:Body");
        Assert.That(reference.Uri, Is.EqualTo("#" + body.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")));
        Assert.That(signedXml.CheckSignature(Certificate.X509Certificate, verifySignatureOnly: true), Is.True);
    }

    [Test]
    public void BinarySecurityTokenContainsTheSigningCertificate()
    {
        var document = CreateSignedMessage();
        var token = Select(document, "/soapenv:Envelope/soapenv:Header/wsse:Security/wsse:BinarySecurityToken");
        Assert.That(token.GetAttribute("ValueType"), Does.EndWith("#X509v3"));
        Assert.That(token.GetAttribute("EncodingType"), Does.EndWith("#Base64Binary"));
        Assert.That(Convert.FromBase64String(token.InnerText), Is.EqualTo(Certificate.X509Certificate.RawData));

        var tokenReference = Select(document, "//wsse:SecurityTokenReference/wsse:Reference");
        Assert.That(tokenReference.GetAttribute("URI"), Is.EqualTo("#" + token.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")));
    }

    [Test]
    public void SignatureSurvivesSerializationAndDetectsTampering()
    {
        var serialized = CreateSignedMessage().OuterXml;

        var reloaded = new XmlDocument { PreserveWhitespace = true };
        reloaded.LoadXml(serialized);
        Assert.That(LoadSignature(reloaded).CheckSignature(Certificate.X509Certificate, verifySignatureOnly: true), Is.True);

        var tampered = new XmlDocument { PreserveWhitespace = true };
        tampered.LoadXml(serialized.Replace("celk_trzba=\"1234.50\"", "celk_trzba=\"1.00\""));
        Assert.That(LoadSignature(tampered).CheckSignature(Certificate.X509Certificate, verifySignatureOnly: true), Is.False);
    }

    [Test]
    public void SubSecondPrecisionIsTruncated()
    {
        var value = new DateTimeOffset(2027, 3, 4, 18, 25, 21, 750, TimeSpan.FromHours(1));
        Assert.That(RevenueMessage.Format(value), Is.EqualTo("2027-03-04T18:25:21+01:00"));
        Assert.That(RevenueMessage.Format(new DateTimeOffset(2027, 3, 4, 17, 25, 21, TimeSpan.Zero)), Is.EqualTo("2027-03-04T17:25:21+00:00"));
    }

    private static XmlDocument CreateSignedMessage()
    {
        var record = TestData.CreateRecord("CZ00000019");
        var document = RevenueMessage.Create(record, Guid.NewGuid(), DateTimeOffset.Now, isFirstAttempt: true, EetMode.Operational);
        return RevenueMessage.Sign(document, Certificate);
    }

    private static SignedXml LoadSignature(XmlDocument document)
    {
        var signedXml = new SignedXmlWithId(document);
        signedXml.LoadXml(Select(document, "//ds:Signature"));
        return signedXml;
    }

    private static XmlElement SelectTrzba(XmlDocument document, string path)
    {
        return Select(document, $"/soapenv:Envelope/soapenv:Body/eet:Trzba/{path}");
    }

    private static XmlElement Select(XmlDocument document, string xpath)
    {
        var namespaces = new XmlNamespaceManager(document.NameTable);
        namespaces.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
        namespaces.AddNamespace("eet", "http://fs.gov.cz/eet/schema/v4");
        namespaces.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
        namespaces.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
        return (XmlElement)document.SelectSingleNode(xpath, namespaces)!;
    }

    private static List<string> ValidateAgainstSchema(XmlDocument envelope)
    {
        var schemas = new XmlSchemaSet();
        schemas.Add("http://fs.gov.cz/eet/schema/v4", Path.Combine(TestContext.CurrentContext.TestDirectory, "Schemas", "EETXMLSchema.xsd"));

        var trzba = new XmlDocument();
        trzba.AppendChild(trzba.ImportNode(SelectTrzba(envelope, "."), deep: true));
        trzba.Schemas = schemas;

        var errors = new List<string>();
        trzba.Validate((_, e) => errors.Add(e.Message));
        return errors;
    }
}
