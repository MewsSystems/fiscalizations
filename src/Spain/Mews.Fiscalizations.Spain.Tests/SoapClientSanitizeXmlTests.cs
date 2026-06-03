using Mews.Fiscalizations.Spain.Communication;
using NUnit.Framework;

namespace Mews.Fiscalizations.Spain.Tests;

[TestFixture]
public class SoapClientSanitizeXmlTests
{
    [Test]
    public void SanitizeXml_WithBareAmpersandInCompanyName_EscapesIt()
    {
        var xml = "<Nombre>EIT CULTURE & CREATIVITY SOUTH WEST, S.L.</Nombre>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo("<Nombre>EIT CULTURE &amp; CREATIVITY SOUTH WEST, S.L.</Nombre>"));
    }

    [Test]
    public void SanitizeXml_WithAlreadyEscapedAmpersand_DoesNotDoubleEscape()
    {
        var xml = "<Nombre>EIT CULTURE &amp; CREATIVITY SOUTH WEST, S.L.</Nombre>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo(xml));
    }

    [Test]
    public void SanitizeXml_WithMultipleBareAmpersands_EscapesAll()
    {
        var xml = "<Nombre>A & B & C</Nombre>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo("<Nombre>A &amp; B &amp; C</Nombre>"));
    }

    [Test]
    public void SanitizeXml_WithOtherXmlEntities_PreservesThem()
    {
        var xml = "<Data>&lt;value&gt; &apos;test&apos; &quot;quoted&quot;</Data>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo(xml));
    }

    [Test]
    public void SanitizeXml_WithNumericCharacterReferences_PreservesThem()
    {
        var xml = "<Data>&#169; &#x00A9;</Data>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo(xml));
    }

    [Test]
    public void SanitizeXml_WithMixedBareAndEscapedAmpersands_OnlyEscapesBare()
    {
        var xml = "<Nombre>A &amp; B & C</Nombre>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo("<Nombre>A &amp; B &amp; C</Nombre>"));
    }

    [Test]
    public void SanitizeXml_WithNoAmpersands_ReturnsUnchanged()
    {
        var xml = "<Nombre>Simple Company S.L.</Nombre>";

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Is.EqualTo(xml));
    }

    [Test]
    public void SanitizeXml_WithFullSoapEnvelope_SanitizesBody()
    {
        var xml =
            """
            <s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
              <s:Body>
                <VNifV2Sal:Nombre>EIT CULTURE & CREATIVITY SOUTH WEST, S.L.</VNifV2Sal:Nombre>
              </s:Body>
            </s:Envelope>
            """;

        var result = SoapClient.SanitizeXml(xml);

        Assert.That(result, Does.Contain("EIT CULTURE &amp; CREATIVITY SOUTH WEST, S.L."));
        Assert.That(result, Does.Not.Contain("& CREATIVITY"));
    }
}
