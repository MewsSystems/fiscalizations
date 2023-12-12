using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The Transform element contains a single transformation
/// </summary>
internal sealed class CanonicalizationMethod
{
    /// <summary>
    /// Algorithm of the transformation
    /// </summary>
    public string Algorithm { get; set; }

    /// <summary>
    /// Load state from an XML element
    /// </summary>
    /// <param name="xmlElement">XML element containing new state</param>
    public void LoadXml(XmlElement xmlElement)
    {
        if (xmlElement is null)
        {
            throw new ArgumentNullException("xmlElement");
        }
        if (xmlElement.HasAttribute("Algorithm"))
        {
            Algorithm = xmlElement.GetAttribute("Algorithm");
        }
        else
        {
            Algorithm = "";
        }
    }

    /// <summary>
    /// Returns the XML representation of the this object
    /// </summary>
    /// <returns>XML element containing the state of this object</returns>
    public XmlElement GetXml()
    {
        var creationXmlDocument = new XmlDocument();
        var retVal = creationXmlDocument.CreateElement("ds", "CanonicalizationMethod", SignedXml.XmlDsigNamespaceUrl);

        if (Algorithm is not null)
        {
            retVal.SetAttribute("Algorithm", Algorithm);
        }
        else
        {
            retVal.SetAttribute("Algorithm", "");
        }

        return retVal;
    }
}