using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class has as purpose to provide the simple substitution of the
/// certificate. It contains references to certificates and digest values
/// computed on them
/// </summary>
internal sealed class SigningCertificate
{
	/// <summary>
	/// A collection of certs
	/// </summary>
	public CertCollection CertCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SigningCertificate()
	{
		CertCollection = new CertCollection();
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

		CertCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:Cert", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newCert = new Cert();
					newCert.LoadXml(iterationXmlElement);
					CertCollection.Add(newCert);
				}
			}
		}
		finally 
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SigningCertificate", XadesSignedXml.XadesNamespaceUri);

		if (CertCollection.Count > 0)
		{
			foreach (Cert cert in CertCollection)
			{
				if (cert.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(cert.GetXml(), true));
				}
			}
		}
		else
		{
			throw new CryptographicException("SigningCertificate.Certcollection should have count > 0");
		}

		return retVal;
	}
}