using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The CommitmentTypeQualifiers element provides means to include
/// additional qualifying information on the commitment made by the signer
/// </summary>
internal sealed class CommitmentTypeQualifier
{
	/// <summary>
	/// The generic XML element that represents a commitment type qualifier
	/// </summary>
	public XmlElement AnyXmlElement { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CommitmentTypeQualifier()
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CommitmentTypeQualifier", XadesSignedXml.XadesNamespaceUri);

		if (AnyXmlElement is not null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(AnyXmlElement, true));
		}

		return retVal;
	}
}