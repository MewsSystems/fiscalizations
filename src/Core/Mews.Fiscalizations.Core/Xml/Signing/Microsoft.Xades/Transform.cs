using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The Transform element contains a single transformation
/// </summary>
internal sealed class Transform
{
	/// <summary>
	/// Algorithm of the transformation
	/// </summary>
	public string Algorithm { get; set; }

	/// <summary>
	/// XPath of the transformation
	/// </summary>
	public string XPath { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public Transform()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Algorithm) || !string.IsNullOrEmpty(XPath);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        Algorithm = xmlElement.HasAttribute("Algorithm") ? xmlElement.GetAttribute("Algorithm") : "";

		var xmlNodeList = xmlElement.SelectNodes("XPath");
		XPath = xmlNodeList.Count != 0 ? xmlNodeList.Item(0).InnerText : "";
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("ds", "Transform", SignedXml.XmlDsigNamespaceUrl);

		retVal.SetAttribute("Algorithm", Algorithm ?? "");

		if (!string.IsNullOrEmpty(XPath))
		{
			var bufferXmlElement = creationXmlDocument.CreateElement("ds", "XPath", SignedXml.XmlDsigNamespaceUrl);
			bufferXmlElement.InnerText = XPath;
			retVal.AppendChild(bufferXmlElement);
		}

		return retVal;
	}
}