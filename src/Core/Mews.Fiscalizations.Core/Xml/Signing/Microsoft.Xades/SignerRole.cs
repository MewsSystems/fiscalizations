using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// According to what has been stated in the Introduction clause, an
/// electronic signature produced in accordance with the present document
/// incorporates: "a commitment that has been explicitly endorsed under a
/// signature policy, at a given time, by a signer under an identifier,
/// e.g. a name or a pseudonym, and optionally a role".
/// While the name of the signer is important, the position of the signer
/// within a company or an organization can be even more important. Some
/// contracts may only be valid if signed by a user in a particular role,
/// e.g. a Sales Director. In many cases who the sales Director really is,
/// is not that important but being sure that the signer is empowered by his
/// company to be the Sales Director is fundamental.
/// </summary>
internal sealed class SignerRole
{
	/// <summary>
	/// The ClaimedRoles element contains a sequence of roles claimed by
	/// the signer but not certified. Additional contents types may be
	/// defined on a domain application basis and be part of this element.
	/// The namespaces given to the corresponding XML schemas will allow
	/// their unambiguous identification in the case these roles use XML.
	/// </summary>
	public ClaimedRoles ClaimedRoles { get; set; }

	/// <summary>
	/// The CertifiedRoles element contains one or more wrapped attribute
	/// certificates for the signer
	/// </summary>
	public CertifiedRoles CertifiedRoles { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignerRole()
	{
		ClaimedRoles = new ClaimedRoles();
		CertifiedRoles = new CertifiedRoles();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return ClaimedRoles != null && ClaimedRoles.HasChanged() || CertifiedRoles != null && CertifiedRoles.HasChanged();
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

		var xmlNodeList = xmlElement.SelectNodes("xsd:ClaimedRoles", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			ClaimedRoles = new ClaimedRoles();
			ClaimedRoles.LoadXml((XmlElement)xmlNodeList.Item(0));
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:CertifiedRoles", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			CertifiedRoles = new CertifiedRoles();
			CertifiedRoles.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignerRole", XadesSignedXml.XadesNamespaceUri);

		if (ClaimedRoles != null && ClaimedRoles.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(ClaimedRoles.GetXml(), true));
		}

		if (CertifiedRoles != null && CertifiedRoles.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CertifiedRoles.GetXml(), true));
		}

		return retVal;
	}
}