using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The SignaturePolicyId element is an explicit and unambiguous identifier
/// of a Signature Policy together with a hash value of the signature
/// policy, so it can be verified that the policy selected by the signer is
/// the one being used by the verifier. An explicit signature policy has a
/// globally unique reference, which, in this way, is bound to an
/// electronic signature by the signer as part of the signature
/// calculation.
/// </summary>
internal sealed class SignaturePolicyId
{
	/// <summary>
	/// The SigPolicyId element contains an identifier that uniquely
	/// identifies a specific version of the signature policy
	/// </summary>
	public ObjectIdentifier SigPolicyId { get; set; }

	/// <summary>
	/// The optional Transforms element can contain the transformations
	/// performed on the signature policy document before computing its
	/// hash
	/// </summary>
	public Transforms Transforms { get; set; }

	/// <summary>
	/// The SigPolicyHash element contains the identifier of the hash
	/// algorithm and the hash value of the signature policy
	/// </summary>
	public DigestAlgAndValueType SigPolicyHash { get; set; }

	/// <summary>
	/// The SigPolicyQualifier element can contain additional information
	/// qualifying the signature policy identifier
	/// </summary>
	public SigPolicyQualifiers SigPolicyQualifiers { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignaturePolicyId()
	{
		SigPolicyId = new ObjectIdentifier("SigPolicyId");
		Transforms = new Transforms();
		SigPolicyHash = new DigestAlgAndValueType("SigPolicyHash");
		SigPolicyQualifiers = new SigPolicyQualifiers();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return SigPolicyId != null
			&& SigPolicyId.HasChanged()
			|| Transforms != null
			&& Transforms.HasChanged()
			|| SigPolicyHash != null
			&& SigPolicyHash.HasChanged()
			|| SigPolicyQualifiers != null
			&& SigPolicyQualifiers.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:SigPolicyId", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("SigPolicyId missing");
		}
		SigPolicyId = new ObjectIdentifier("SigPolicyId");
		SigPolicyId.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("ds:Transforms", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			Transforms = new Transforms();
			Transforms.LoadXml((XmlElement)xmlNodeList.Item(0));
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:SigPolicyHash", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("SigPolicyHash missing");
		}
		SigPolicyHash = new DigestAlgAndValueType("SigPolicyHash");
		SigPolicyHash.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:SigPolicyQualifiers", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			SigPolicyQualifiers = new SigPolicyQualifiers();
			SigPolicyQualifiers.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignaturePolicyId", XadesSignedXml.XadesNamespaceUri);

		if (SigPolicyId != null && SigPolicyId.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SigPolicyId.GetXml(), true));
		}

		if (Transforms != null && Transforms.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(Transforms.GetXml(), true));
		}

		if (SigPolicyHash != null && SigPolicyHash.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SigPolicyHash.GetXml(), true));
		}

		if (SigPolicyQualifiers != null && SigPolicyQualifiers.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SigPolicyQualifiers.GetXml(), true));
		}

		return retVal;
	}
}