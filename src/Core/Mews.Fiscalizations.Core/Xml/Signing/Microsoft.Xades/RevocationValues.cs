using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The RevocationValues element is used to hold the values of the
/// revocation information which are to be shipped with the XML signature
/// in case of an XML Advanced Electronic Signature with Extended
/// Validation Data (XAdES-X-Long). This is a unsigned property that
/// qualifies the signature. An XML electronic signature aligned with the
/// present document MAY contain at most one RevocationValues element.
/// </summary>
internal sealed class RevocationValues
{
	/// <summary>
	/// Optional Id for the XML element
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Certificate Revocation Lists
	/// </summary>
	public CRLValues CRLValues { get; set; }

	/// <summary>
	/// Responses from an online certificate status server
	/// </summary>
	public OCSPValues OCSPValues { get; set; }

	/// <summary>
	/// Placeholder for other revocation information is provided for future
	/// use
	/// </summary>
	public OtherValues OtherValues { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public RevocationValues()
	{
		CRLValues = new CRLValues();
		OCSPValues = new OCSPValues();
		OtherValues = new OtherValues();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Id)
		       || CRLValues != null
		       && CRLValues.HasChanged()
		       || OCSPValues != null
		       && OCSPValues.HasChanged()
		       || OtherValues != null
		       && OtherValues.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        Id = xmlElement.HasAttribute("Id") ? xmlElement.GetAttribute("Id") : "";

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xades:CRLValues", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			CRLValues = new CRLValues();
			CRLValues.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		xmlNodeList = xmlElement.SelectNodes("xades:OCSPValues", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			OCSPValues = new OCSPValues();
			OCSPValues.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		xmlNodeList = xmlElement.SelectNodes("xades:OtherValues", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			OtherValues = new OtherValues();
			OtherValues.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "RevocationValues", XadesSignedXml.XadesNamespaceUri);
		if (!string.IsNullOrEmpty(Id))
		{
			retVal.SetAttribute("Id", Id);
		}
		if (CRLValues != null && CRLValues.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CRLValues.GetXml(), true));
		}
		if (OCSPValues != null && OCSPValues.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(OCSPValues.GetXml(), true));
		}
		if (OtherValues != null && OtherValues.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(OtherValues.GetXml(), true));
		}

		return retVal;
	}
}