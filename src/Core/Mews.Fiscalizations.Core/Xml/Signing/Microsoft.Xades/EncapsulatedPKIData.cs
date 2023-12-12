using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// EncapsulatedPKIData is used to incorporate a piece of PKI data
/// into an XML structure whereas the PKI data is encoded using an ASN.1
/// encoding mechanism. Examples of such PKI data that are widely used at
/// the time include X509 certificates and revocation lists, OCSP responses,
/// attribute certificates and time-stamps.
/// </summary>
internal class EncapsulatedPKIData
{
	/// <summary>
	/// The name of the element when serializing
	/// </summary>
	public string TagName { get; set; }

	/// <summary>
	/// The optional ID attribute can be used to make a reference to an element
	/// of this data type.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Base64 encoded content of this data type 
	/// </summary>
	public byte[] PkiData { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public EncapsulatedPKIData()
	{
	}

	/// <summary>
	/// Constructor with TagName
	/// </summary>
	/// <param name="tagName">Name of the tag when serializing with GetXml</param>
	public EncapsulatedPKIData(string tagName)
	{
		this.TagName = tagName;
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Id) || PkiData != null && PkiData.Length > 0;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        Id = xmlElement.HasAttribute("Id") ? xmlElement.GetAttribute("Id") : "";

		PkiData = Convert.FromBase64String(xmlElement.InnerText);
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, TagName, XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("Encoding", "http://uri.etsi.org/01903/v1.2.2#DER");

		if (!string.IsNullOrEmpty(Id))
		{
			retVal.SetAttribute("Id", Id);
		}

		if (PkiData is {Length: > 0})
		{
			retVal.InnerText = Convert.ToBase64String(PkiData, Base64FormattingOptions.InsertLineBreaks);
		}

		return retVal;
	}
}