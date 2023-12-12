using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains an identifier of a signature policy
/// </summary>
internal sealed class SignaturePolicyIdentifier
{
	private SignaturePolicyId signaturePolicyId;
	private bool signaturePolicyImplied;

	/// <summary>
	/// The SignaturePolicyId element is an explicit and unambiguous identifier
	/// of a Signature Policy together with a hash value of the signature
	/// policy, so it can be verified that the policy selected by the signer is
	/// the one being used by the verifier. An explicit signature policy has a
	/// globally unique reference, which, in this way, is bound to an
	/// electronic signature by the signer as part of the signature
	/// calculation.
	/// </summary>
	public SignaturePolicyId SignaturePolicyId
	{
		get => signaturePolicyId;
		set
		{
			signaturePolicyId = value;
			signaturePolicyImplied = false;
		}
	}

	/// <summary>
	/// The empty SignaturePolicyImplied element will appear when the
	/// data object(s) being signed and other external data imply the
	/// signature policy
	/// </summary>
	public bool SignaturePolicyImplied
	{
		get => signaturePolicyImplied;
		set
		{
			signaturePolicyImplied = value;
			if (signaturePolicyImplied)
			{
				signaturePolicyId = null;
			}
		}
	}

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignaturePolicyIdentifier()
	{
		signaturePolicyId = new SignaturePolicyId();
		signaturePolicyImplied = false;
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return signaturePolicyId != null && signaturePolicyId.HasChanged() || signaturePolicyImplied;
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

		var xmlNodeList = xmlElement.SelectNodes("xsd:SignaturePolicyId", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			signaturePolicyId = new SignaturePolicyId();
			signaturePolicyId.LoadXml((XmlElement)xmlNodeList.Item(0));
			signaturePolicyImplied = false;
		}
		else
		{
			xmlNodeList = xmlElement.SelectNodes("xsd:SignaturePolicyImplied", xmlNamespaceManager);
			if (xmlNodeList.Count != 0)
			{
				signaturePolicyImplied = true;
				signaturePolicyId = null;
			}
			else
			{
				throw new CryptographicException("SignaturePolicyId or SignaturePolicyImplied missing");
			}
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignaturePolicyIdentifier", XadesSignedXml.XadesNamespaceUri);

		if (signaturePolicyImplied)
		{
			var bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignaturePolicyImplied", XadesSignedXml.XadesNamespaceUri);
			retVal.AppendChild(bufferXmlElement);
		}
		else
		{
			if (signaturePolicyId != null && signaturePolicyId.HasChanged())
			{
				retVal.AppendChild(creationXmlDocument.ImportNode(signaturePolicyId.GetXml(), true));
			}
			else
			{
				throw new CryptographicException("SignaturePolicyId or SignaturePolicyImplied missing in SignaturePolicyIdentifier");
			}
		}

		return retVal;
	}
}