using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The CertifiedRoles element contains one or more wrapped attribute
/// certificates for the signer
/// </summary>
internal sealed class CertifiedRoles
{
	/// <summary>
	/// Collection of certified roles
	/// </summary>
	public CertifiedRoleCollection CertifiedRoleCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CertifiedRoles()
	{
		CertifiedRoleCollection = new CertifiedRoleCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CertifiedRoleCollection.Count > 0;
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

		CertifiedRoleCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:CertifiedRole", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newCertifiedRole = new EncapsulatedPKIData("CertifiedRole");
					newCertifiedRole.LoadXml(iterationXmlElement);
					CertifiedRoleCollection.Add(newCertifiedRole);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CertifiedRoles", XadesSignedXml.XadesNamespaceUri);

		if (CertifiedRoleCollection.Count > 0)
		{
			foreach (EncapsulatedPKIData certifiedRole in CertifiedRoleCollection)
			{
				if (certifiedRole.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(certifiedRole.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}