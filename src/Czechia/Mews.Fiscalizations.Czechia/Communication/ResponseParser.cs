using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using Mews.Fiscalizations.Czechia.Model;

namespace Mews.Fiscalizations.Czechia.Communication;

internal static class ResponseParser
{
    private static readonly Regex ConfirmationCodePattern = new(
        @"\A[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-4[0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}-[0-9a-fA-F]{2}\z",
        RegexOptions.Compiled
    );

    public static SendRevenueResult Parse(string responseXml, Guid messageUuid, string requestXml, Option<string> globalTransactionId)
    {
        var document = new XmlDocument { PreserveWhitespace = true, XmlResolver = null };
        try
        {
            document.LoadXml(responseXml);
        }
        catch (XmlException e)
        {
            throw new EetException($"EET response is not a valid XML document: {e.Message}", responseXml, e);
        }

        var namespaceManager = new XmlNamespaceManager(document.NameTable);
        namespaceManager.AddNamespace("soapenv", Namespaces.Soap);
        namespaceManager.AddNamespace("eet", Namespaces.Eet);

        var fault = document.SelectSingleNode("/soapenv:Envelope/soapenv:Body/soapenv:Fault", namespaceManager);
        if (fault is not null)
        {
            var faultString = fault.SelectSingleNode("faultstring")?.InnerText.Trim();
            throw new EetException($"EET returned a SOAP fault: {faultString}", responseXml);
        }

        var odpoved = document.SelectSingleNode("/soapenv:Envelope/soapenv:Body/eet:Odpoved", namespaceManager) as XmlElement
            ?? throw new EetException("EET response does not contain the Odpoved element.", responseXml);
        var hlavicka = odpoved.SelectSingleNode("eet:Hlavicka", namespaceManager) as XmlElement;
        var potvrzeni = odpoved.SelectSingleNode("eet:Potvrzeni", namespaceManager) as XmlElement;
        var chyba = odpoved.SelectSingleNode("eet:Chyba", namespaceManager) as XmlElement;

        var warnings = odpoved.SelectNodes("eet:Varovani", namespaceManager)!
            .OfType<XmlElement>()
            .Select(w => new SendRevenueWarning(ParseInt(w.GetAttribute("kod_varov"), responseXml), Normalize(w.InnerText)))
            .ToList();

        Try<SendRevenueSuccess, SendRevenueError> outcome;
        bool isTest;
        if (potvrzeni is not null)
        {
            var received = Attribute(hlavicka, "dat_prij").Map(ParseDateTime);
            if (received.IsEmpty)
            {
                throw new EetException("EET acknowledgement does not contain dat_prij.", responseXml);
            }
            var confirmationCode = potvrzeni.GetAttribute("pok");
            if (!ConfirmationCodePattern.IsMatch(confirmationCode))
            {
                throw new EetException($"EET acknowledgement contains an invalid confirmation code (pok) '{confirmationCode}'.", responseXml);
            }
            outcome = Try.Success<SendRevenueSuccess, SendRevenueError>(new SendRevenueSuccess(confirmationCode, received.Get()));
            isTest = ParseBool(potvrzeni.GetAttribute("test"));
        }
        else if (chyba is not null)
        {
            var error = new SendRevenueError(
                code: ParseInt(chyba.GetAttribute("kod"), responseXml),
                message: Normalize(chyba.InnerText),
                rejected: Attribute(hlavicka, "dat_odmit").Map(ParseDateTime)
            );
            outcome = Try.Error<SendRevenueSuccess, SendRevenueError>(error);
            isTest = ParseBool(chyba.GetAttribute("test"));
        }
        else
        {
            throw new EetException("EET response contains neither Potvrzeni nor Chyba.", responseXml);
        }

        return new SendRevenueResult(messageUuid, outcome, warnings, isTest, globalTransactionId, requestXml, responseXml);
    }

    private static Option<string> Attribute(XmlElement? element, string name)
    {
        return element is not null && element.HasAttribute(name) ? Option.Create(element.GetAttribute(name)) : Option.Empty<string>();
    }

    private static DateTimeOffset ParseDateTime(string value)
    {
        return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
    }

    private static int ParseInt(string value, string responseXml)
    {
        return int.TryParse(value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var result)
            ? result
            : throw new EetException($"EET response contains an invalid code '{value}'.", responseXml);
    }

    private static bool ParseBool(string value)
    {
        return value is "true" or "1";
    }

    private static string Normalize(string text)
    {
        return string.Join(" ", text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
    }
}
