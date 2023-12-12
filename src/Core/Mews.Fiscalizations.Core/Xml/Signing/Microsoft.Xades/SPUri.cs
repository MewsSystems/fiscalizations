using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// SPUri represents the URL where the copy of the Signature Policy may be
/// obtained.  The class derives from SigPolicyQualifier.
/// </summary>
internal sealed class SPUri : SigPolicyQualifier
{
	/// <summary>
	/// Uri for the sig policy qualifier
	/// </summary>
	public string Uri { get; set; }

	/// <summary>
	/// Inherited generic element, not used in the SPUri class
	/// </summary>
	public override XmlElement AnyXmlElement
	{
		get => null;
		set => throw new CryptographicException("Setting AnyXmlElement on a SPUri is not supported");
	}

	/// <summary>
	/// Default constructor
	/// </summary>
	public SPUri()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public override bool HasChanged()
	{
		return !string.IsNullOrEmpty(Uri);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public override void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:SPURI", xmlNamespaceManager);

		Uri = ((XmlElement)xmlNodeList.Item(0)).InnerText;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public override XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("SigPolicyQualifier", XadesSignedXml.XadesNamespaceUri);

		var bufferXmlElement = creationXmlDocument.CreateElement("SPURI", XadesSignedXml.XadesNamespaceUri);
		bufferXmlElement.InnerText = Uri;
		retVal.AppendChild(creationXmlDocument.ImportNode(bufferXmlElement, true));

		return retVal;
	}
}