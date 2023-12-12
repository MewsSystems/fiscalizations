using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// In some transactions the purported place where the signer was at the time
/// of signature creation may need to be indicated. In order to provide this
/// information a new property may be included in the signature.
/// This property specifies an address associated with the signer at a
/// particular geographical (e.g. city) location.
/// This is a signed property that qualifies the signer.
/// An XML electronic signature aligned with the present document MAY contain
/// at most one SignatureProductionPlace element.
/// </summary>
internal sealed class SignatureProductionPlace
{
	/// <summary>
	/// City where signature was produced
	/// </summary>
	public string City { get; set; }

	/// <summary>
	/// State or province where signature was produced
	/// </summary>
	public string StateOrProvince { get; set; }

	/// <summary>
	/// Postal code of place where signature was produced
	/// </summary>
	public string PostalCode { get; set; }

	/// <summary>
	/// Country where signature was produced
	/// </summary>
	public string CountryName { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignatureProductionPlace()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(StateOrProvince) || !string.IsNullOrEmpty(PostalCode) || !string.IsNullOrEmpty(CountryName);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:City", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			City = xmlNodeList.Item(0).InnerText;
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:PostalCode", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			PostalCode = xmlNodeList.Item(0).InnerText;
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:StateOrProvince", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			StateOrProvince = xmlNodeList.Item(0).InnerText;
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:CountryName", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			CountryName = xmlNodeList.Item(0).InnerText;
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		XmlElement bufferXmlElement;

		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignatureProductionPlace", XadesSignedXml.XadesNamespaceUri);

		if (!string.IsNullOrEmpty(City))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "City", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = City;
			retVal.AppendChild(bufferXmlElement);
		}

		if (!string.IsNullOrEmpty(StateOrProvince))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "StateOrProvince", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = StateOrProvince;
			retVal.AppendChild(bufferXmlElement);
		}

		if (!string.IsNullOrEmpty(PostalCode))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "PostalCode", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = PostalCode;
			retVal.AppendChild(bufferXmlElement);
		}

		if (!string.IsNullOrEmpty(CountryName))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CountryName", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = CountryName;
			retVal.AppendChild(bufferXmlElement);
		}

		return retVal;
	}
}