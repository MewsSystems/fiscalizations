using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Czechia.Communication;

internal sealed class SecurityTokenReference : KeyInfoClause
{
    private readonly string _binarySecurityTokenId;

    public SecurityTokenReference(string binarySecurityTokenId)
    {
        _binarySecurityTokenId = binarySecurityTokenId;
    }

    public override XmlElement GetXml()
    {
        var document = new XmlDocument();
        var tokenReference = document.CreateElement("wsse", "SecurityTokenReference", Namespaces.WsSecurity);
        var reference = document.CreateElement("wsse", "Reference", Namespaces.WsSecurity);
        reference.SetAttribute("URI", $"#{_binarySecurityTokenId}");
        reference.SetAttribute("ValueType", Namespaces.X509v3);
        tokenReference.AppendChild(reference);
        return tokenReference;
    }

    public override void LoadXml(XmlElement element)
    {
        throw new NotSupportedException();
    }
}
