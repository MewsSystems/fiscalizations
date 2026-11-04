using Mews.Fiscalizations.Czechia.Communication;

namespace Mews.Fiscalizations.Czechia.Tests;

/// <summary>
/// Response examples from chapters 3.4.3 and 3.5.3 of the EET 2.0 data interface description.
/// </summary>
public class ResponseParserTests
{
    private static readonly Guid MessageUuid = Guid.Parse("123e4567-e89b-42d3-a456-426655440000");

    [Test]
    public void ParsesProductionAcknowledgement()
    {
        var result = Parse("""
            <eet:Odpoved>
                <eet:Hlavicka uuid_zpravy="123e4567-e89b-42d3-a456-426655440000" dat_prij="2027-03-04T18:25:21+01:00" />
                <eet:Potvrzeni pok="987a6be5-6af5-44f3-b4fc-987654321000-02" />
            </eet:Odpoved>
            """);

        Assert.That(result.IsSuccess, Is.True);
        var success = result.Success.Get();
        Assert.That(success.ConfirmationCode, Is.EqualTo("987a6be5-6af5-44f3-b4fc-987654321000-02"));
        Assert.That(success.Received, Is.EqualTo(new DateTimeOffset(2027, 3, 4, 18, 25, 21, TimeSpan.FromHours(1))));
        Assert.That(result.IsTest, Is.False);
        Assert.That(result.Warnings, Is.Empty);
        Assert.That(result.GlobalTransactionId.Get(), Is.EqualTo("trace-1"));
    }

    [Test]
    public void ParsesPlaygroundAcknowledgementWithWarnings()
    {
        var result = Parse("""
            <eet:Odpoved>
                <eet:Hlavicka uuid_zpravy="123e4567-e89b-42d3-a456-426655440000" dat_prij="2027-03-04T18:25:21+01:00" />
                <eet:Potvrzeni pok="987a6be5-6af5-44f3-b4fc-987654321000-ff" test="true" />
                <eet:Varovani kod_varov="1" >
                    EIC poplatnika v datove zprave se neshoduje s EIC
            v certifikatu
                </eet:Varovani>
                <eet:Varovani kod_varov="2" >
                    Chybny format EIC poverujiciho poplatnika
                </eet:Varovani>
            </eet:Odpoved>
            """);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.IsTest, Is.True);
        Assert.That(result.Warnings.Select(w => w.Code), Is.EqualTo(new[] { 1, 2 }));
        Assert.That(result.Warnings[0].Message, Is.EqualTo("EIC poplatnika v datove zprave se neshoduje s EIC v certifikatu"));
    }

    [Test]
    public void ParsesError()
    {
        var result = Parse("""
            <eet:Odpoved>
                <eet:Hlavicka uuid_zpravy="123e4567-e89b-42d3-a456-426655440000" dat_odmit="2027-03-04T18:25:21+01:00" />
                <eet:Chyba kod="7" test="true" >
                    Datova zprava je prilis velka
                </eet:Chyba>
            </eet:Odpoved>
            """);

        Assert.That(result.IsSuccess, Is.False);
        var error = result.Error.Get();
        Assert.That(error.Code, Is.EqualTo(7));
        Assert.That(error.Message, Is.EqualTo("Datova zprava je prilis velka"));
        Assert.That(error.Rejected.Get(), Is.EqualTo(new DateTimeOffset(2027, 3, 4, 18, 25, 21, TimeSpan.FromHours(1))));
        Assert.That(error.IsTemporary, Is.False);
        Assert.That(error.IsSuccessfulVerification, Is.False);
        Assert.That(result.IsTest, Is.True);
    }

    [Test]
    public void ParsesTemporaryErrorWithoutMessageUuid()
    {
        var result = Parse("""
            <eet:Odpoved>
                <eet:Hlavicka dat_odmit="2027-03-04T18:25:21+01:00" />
                <eet:Chyba kod="-1">Docasna technicka chyba zpracovani - odeslete prosim datovou zpravu pozdeji</eet:Chyba>
            </eet:Odpoved>
            """);

        var error = result.Error.Get();
        Assert.That(error.Code, Is.EqualTo(-1));
        Assert.That(error.IsTemporary, Is.True);
        Assert.That(result.IsTest, Is.False);
    }

    [Test]
    public void ParsesSuccessfulVerificationWithWarnings()
    {
        var result = Parse("""
            <eet:Odpoved>
                <eet:Hlavicka uuid_zpravy="123e4567-e89b-42d3-a456-426655440000" dat_odmit="2027-03-04T18:25:21+01:00" />
                <eet:Chyba kod="0" test="true">Datovou zpravu evidovane trzby v overovacim modu se podarilo zpracovat</eet:Chyba>
                <eet:Varovani kod_varov="7">EIC poverujiciho poplatnika se shoduje s EIC poplatnika</eet:Varovani>
            </eet:Odpoved>
            """);

        Assert.That(result.Error.Get().IsSuccessfulVerification, Is.True);
        Assert.That(result.Warnings.Single().Code, Is.EqualTo(7));
    }

    [TestCase("<eet:Potvrzeni />")]
    [TestCase("<eet:Potvrzeni pok=\"\" />")]
    [TestCase("<eet:Potvrzeni pok=\"987a6be5-6af5-44f3-b4fc-987654321000\" />")]
    public void AcknowledgementWithoutValidConfirmationCodeThrows(string potvrzeni)
    {
        var envelope = $"""
            <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:eet="http://fs.gov.cz/eet/schema/v4">
                <soapenv:Body><eet:Odpoved><eet:Hlavicka uuid_zpravy="123e4567-e89b-42d3-a456-426655440000" dat_prij="2027-03-04T18:25:21+01:00" />{potvrzeni}</eet:Odpoved></soapenv:Body>
            </soapenv:Envelope>
            """;
        Assert.Throws<EetException>(() => ResponseParser.Parse(envelope, MessageUuid, "<request/>", Option.Empty<string>()));
    }

    [Test]
    public void SoapFaultThrows()
    {
        var response = """
            <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"><soapenv:Body><soapenv:Fault><faultcode>soapenv:Server</faultcode><faultstring>Internal Error</faultstring></soapenv:Fault></soapenv:Body></soapenv:Envelope>
            """;
        var exception = Assert.Throws<EetException>(() => ResponseParser.Parse(response, MessageUuid, "<request/>", Option.Empty<string>()));
        Assert.That(exception!.Message, Does.Contain("Internal Error"));
        Assert.That(exception.Response, Is.EqualTo(response));
    }

    [Test]
    public void NonXmlResponseThrows()
    {
        Assert.Throws<EetException>(() => ResponseParser.Parse("<html>Service Unavailable", MessageUuid, "<request/>", Option.Empty<string>()));
    }

    private static SendRevenueResult Parse(string odpoved)
    {
        var envelope = $"""
            <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:eet="http://fs.gov.cz/eet/schema/v4">
                <soapenv:Header/>
                <soapenv:Body>{odpoved}</soapenv:Body>
            </soapenv:Envelope>
            """;
        var result = ResponseParser.Parse(envelope, MessageUuid, "<request/>", Option.Create("trace-1"));
        Assert.That(result.MessageUuid, Is.EqualTo(MessageUuid));
        Assert.That(result.ResponseXml, Is.EqualTo(envelope));
        return result;
    }
}
