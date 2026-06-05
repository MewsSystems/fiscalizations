using System.Security.Cryptography.X509Certificates;
using Mews.Fiscalizations.Spain.Communication;
using NUnit.Framework;

namespace Mews.Fiscalizations.Spain.Tests;

[TestFixture]
public class SoapClientGetSoapBodyTests
{
#pragma warning disable SYSLIB0026 // X509Certificate2() is obsolete but X509CertificateLoader is net9+ only
    private static readonly SoapClient Client = new(
        new Uri("https://localhost"),
        new X509Certificate2(),
        TimeSpan.FromSeconds(5)
    );
#pragma warning restore SYSLIB0026

    [TestCase("Simple Company S.L.", Description = "No ampersand")]
    [TestCase("EIT CULTURE &amp; CREATIVITY S.L.", Description = "Already escaped ampersand")]
    [TestCase("EIT CULTURE & CREATIVITY S.L.", Description = "Bare ampersand")]
    [TestCase("A & B & C S.L.", Description = "Multiple bare ampersands")]
    [TestCase("A &amp; B & C S.L.", Description = "Mixed bare and escaped ampersands")]
    public void GetSoapBody_ParsesCompanyName(string nombre)
    {
        var soapXml = BuildNifResponse(nombre);

        var body = Client.GetSoapBody(soapXml);

        var ns = new System.Xml.XmlNamespaceManager(body.OwnerDocument.NameTable);
        ns.AddNamespace("v", "http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Sal.xsd");
        var result = body.SelectSingleNode("//v:Resultado", ns)?.InnerText;
        Assert.That(result, Is.EqualTo("IDENTIFICADO"));
    }

    private static string BuildNifResponse(string nombre)
    {
        return
            $"""
            <?xml version="1.0" encoding="UTF-8"?>
            <env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope/">
              <env:Body>
                <VNifV2Sal xmlns="http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Sal.xsd">
                  <Contribuyente>
                    <Nif>B19718626</Nif>
                    <Nombre>{nombre}</Nombre>
                    <Resultado>IDENTIFICADO</Resultado>
                  </Contribuyente>
                </VNifV2Sal>
              </env:Body>
            </env:Envelope>
            """;
    }
}
