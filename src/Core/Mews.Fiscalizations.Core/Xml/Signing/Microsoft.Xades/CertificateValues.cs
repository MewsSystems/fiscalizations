using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The CertificateValues element contains the full set of certificates
/// that have been used to validate	the electronic signature, including the
/// signer's certificate. However, it is not necessary to include one of
/// those certificates into this property, if the certificate is already
/// present in the ds:KeyInfo element of the signature.
/// In fact, both the signer certificate (referenced in the mandatory
/// SigningCertificate property element) and all certificates referenced in
/// the CompleteCertificateRefs property element must be present either in
/// the ds:KeyInfo element of the signature or in the CertificateValues
/// property element.
/// </summary>
internal sealed class CertificateValues
{
	/// <summary>
	/// Optional Id of the certificate values element
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// A collection of encapsulated X509 certificates
	/// </summary>
	public EncapsulatedX509CertificateCollection EncapsulatedX509CertificateCollection { get; set; }

	/// <summary>
	/// Collection of other certificates
	/// </summary>
	public OtherCertificateCollection OtherCertificateCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CertificateValues()
	{
		EncapsulatedX509CertificateCollection = new EncapsulatedX509CertificateCollection();
		OtherCertificateCollection = new OtherCertificateCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Id) || EncapsulatedX509CertificateCollection.Count > 0 || OtherCertificateCollection.Count > 0;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
		XmlElement iterationXmlElement;
        ArgumentNullException.ThrowIfNull(xmlElement);
        Id = xmlElement.HasAttribute("Id") ? xmlElement.GetAttribute("Id") : "";

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

		EncapsulatedX509CertificateCollection.Clear();
		OtherCertificateCollection.Clear();

		var xmlNodeList = xmlElement.SelectNodes("xades:EncapsulatedX509Certificate", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newEncapsulatedX509Certificate = new EncapsulatedX509Certificate();
					newEncapsulatedX509Certificate.LoadXml(iterationXmlElement);
					EncapsulatedX509CertificateCollection.Add(newEncapsulatedX509Certificate);
				}
			}
		}
		finally
		{
			var disposable = enumerator as IDisposable;
			disposable?.Dispose();
		}

		xmlNodeList = xmlElement.SelectNodes("xades:OtherCertificate", xmlNamespaceManager);
		enumerator = xmlNodeList.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement is not null)
				{
					var newOtherCertificate = new OtherCertificate();
					newOtherCertificate.LoadXml(iterationXmlElement);
					OtherCertificateCollection.Add(newOtherCertificate);
				}
			}
		}
		finally
		{
			var disposable = enumerator as IDisposable;
			disposable?.Dispose();
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CertificateValues", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		if (!string.IsNullOrEmpty(Id))
		{
			retVal.SetAttribute("Id", Id);
		}

		if (EncapsulatedX509CertificateCollection.Count > 0)
		{
			foreach (EncapsulatedX509Certificate encapsulatedX509Certificate in EncapsulatedX509CertificateCollection)
			{
				if (encapsulatedX509Certificate.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(encapsulatedX509Certificate.GetXml(), true));
				}
			}
		}
		if (OtherCertificateCollection.Count > 0)
		{
			foreach (OtherCertificate otherCertificate in OtherCertificateCollection)
			{
				if (otherCertificate.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(otherCertificate.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}