using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Czechia.Communication;

/// <summary>
/// SignedXml resolves only plain Id attributes by default; WS-Security references the body through wsu:Id.
/// </summary>
internal sealed class SignedXmlWithId : SignedXml
{
    public SignedXmlWithId(XmlDocument document)
        : base(document)
    {
    }

    public override XmlElement? GetIdElement(XmlDocument? document, string idValue)
    {
        var element = base.GetIdElement(document, idValue);
        if (element is not null || document is null)
        {
            return element;
        }

        var namespaceManager = new XmlNamespaceManager(document.NameTable);
        namespaceManager.AddNamespace("wsu", Namespaces.WsUtility);
        var matches = document.SelectNodes($"//*[@wsu:Id='{idValue}']", namespaceManager);
        return matches?.Count == 1 ? matches[0] as XmlElement : null;
    }
}
