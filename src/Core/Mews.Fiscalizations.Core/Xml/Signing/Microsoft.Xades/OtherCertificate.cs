using System.Xml;
using System.Collections;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The OtherCertificate element is a placeholder for potential future
/// new formats of certificates
/// </summary>
internal sealed class OtherCertificate : ArrayList
{
	/// <summary>
	/// The generic XML element that represents any certificate
	/// </summary>
	public XmlElement AnyXmlElement { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OtherCertificate()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return AnyXmlElement is not null;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
		AnyXmlElement = xmlElement;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("OtherCertificate", XadesSignedXml.XadesNamespaceUri);

		if (AnyXmlElement != null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(AnyXmlElement, true));
		}

		return retVal;
	}
}