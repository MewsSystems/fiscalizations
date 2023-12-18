using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The properties that qualify the signature itself or the signer are
/// included as content of the SignedSignatureProperties element
/// </summary>
internal sealed class SignedSignatureProperties
{
	private DateTime signingTime;

	/// <summary>
	/// The signing time property specifies the time at which the signer
	/// performed the signing process. This is a signed property that
	/// qualifies the whole signature. An XML electronic signature aligned
	/// with the present document MUST contain exactly one SigningTime element .
	/// </summary>
	public DateTime SigningTime
	{
		get => signingTime;
		set => signingTime = value;
	}

	/// <summary>
	/// The SigningCertificate property is designed to prevent the simple
	/// substitution of the certificate. This property contains references
	/// to certificates and digest values computed on them. The certificate
	/// used to verify the signature shall be identified in the sequence;
	/// the signature policy may mandate other certificates be present,
	/// that may include all the certificates up to the point of trust.
	/// This is a signed property that qualifies the signature. An XML
	/// electronic signature aligned with the present document MUST contain
	/// exactly one SigningCertificate.
	/// </summary>
	public SigningCertificate SigningCertificate { get; set; }

	/// <summary>
	/// The signature policy is a set of rules for the creation and
	/// validation of an electronic signature, under which the signature
	/// can be determined to be valid. A given legal/contractual context
	/// may recognize a particular signature policy as meeting its
	/// requirements.
	/// An XML electronic signature aligned with the present document MUST
	/// contain exactly one SignaturePolicyIdentifier element.
	/// </summary>
	public SignaturePolicyIdentifier SignaturePolicyIdentifier { get; set; }

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
	public SignatureProductionPlace SignatureProductionPlace { get; set; }

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
	public SignerRole SignerRole { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignedSignatureProperties()
	{
		signingTime = DateTime.MinValue;
		SigningCertificate = new SigningCertificate();
		SignaturePolicyIdentifier = new SignaturePolicyIdentifier();
		SignatureProductionPlace = new SignatureProductionPlace();
		SignerRole = new SignerRole();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return true;
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

		var xmlNodeList = xmlElement.SelectNodes("xsd:SigningTime", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("SigningTime missing");
		}
		signingTime = XmlConvert.ToDateTime(xmlNodeList.Item(0).InnerText, XmlDateTimeSerializationMode.Local);

		xmlNodeList = xmlElement.SelectNodes("xsd:SigningCertificate", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("SigningCertificate missing");
		}
		SigningCertificate = new SigningCertificate();
		SigningCertificate.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:SignaturePolicyIdentifier", xmlNamespaceManager);
		if (xmlNodeList.Count > 0)
		{
			SignaturePolicyIdentifier = new SignaturePolicyIdentifier();
			SignaturePolicyIdentifier.LoadXml((XmlElement)xmlNodeList.Item(0));
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:SignatureProductionPlace", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			SignatureProductionPlace = new SignatureProductionPlace();
			SignatureProductionPlace.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		else
		{
			SignatureProductionPlace = null;
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:SignerRole", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			SignerRole = new SignerRole();
			SignerRole.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		else
		{
			SignerRole = null;
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignedSignatureProperties", XadesSignedXml.XadesNamespaceUri);

		if (signingTime == DateTime.MinValue)
		{
			signingTime = DateTime.Now;
		}

		var bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SigningTime", XadesSignedXml.XadesNamespaceUri);
                                   
		var truncatedDateTime = signingTime.AddTicks(-(signingTime.Ticks % TimeSpan.TicksPerSecond));

		bufferXmlElement.InnerText = XmlConvert.ToString(truncatedDateTime, XmlDateTimeSerializationMode.Local);            

		retVal.AppendChild(bufferXmlElement);

		if (SigningCertificate != null && SigningCertificate.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SigningCertificate.GetXml(), true));
		}
		else
		{
			throw new CryptographicException("SigningCertificate element missing in SignedSignatureProperties");
		}

		if (SignaturePolicyIdentifier != null && SignaturePolicyIdentifier.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SignaturePolicyIdentifier.GetXml(), true));
		}

		if (SignatureProductionPlace != null && SignatureProductionPlace.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SignatureProductionPlace.GetXml(), true));
		}

		if (SignerRole != null && SignerRole.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SignerRole.GetXml(), true));
		}

		return retVal;
	}
}