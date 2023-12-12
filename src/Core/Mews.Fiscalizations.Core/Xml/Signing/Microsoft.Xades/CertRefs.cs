using System;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The CertRefs element contains a collection of Cert elements
/// </summary>
internal sealed class CertRefs
{
	/// <summary>
	/// Collection of Certs
	/// </summary>
	public CertCollection CertCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CertRefs()
	{
		CertCollection = new CertCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CertCollection.Count > 0;
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
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newCert = new Cert();
					newCert.LoadXml(iterationXmlElement);
					CertCollection.Add(newCert);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CertRefs", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

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

		return retVal;
	}
}