using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a roles claimed by the signer but not it is not a
/// certified role
/// </summary>
internal sealed class ClaimedRole
{
	/// <summary>
	/// The generic XML element that represents a claimed role
	/// </summary>
	public XmlElement AnyXmlElement { get; set; }

	public string InnerText { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public ClaimedRole()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return AnyXmlElement is not null || !string.IsNullOrEmpty(InnerText);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
		AnyXmlElement = xmlElement;
		InnerText = xmlElement.InnerText;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "ClaimedRole", XadesSignedXml.XadesNamespaceUri);

		if (!string.IsNullOrEmpty(InnerText))
		{
			retVal.InnerText = InnerText;
		}

		if (AnyXmlElement is not null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(AnyXmlElement, true));
		}

		return retVal;
	}
}