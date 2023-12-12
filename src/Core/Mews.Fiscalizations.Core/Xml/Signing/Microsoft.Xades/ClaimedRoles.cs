using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The ClaimedRoles element contains a sequence of roles claimed by
/// the signer but not certified. Additional contents types may be
/// defined on a domain application basis and be part of this element.
/// The namespaces given to the corresponding XML schemas will allow
/// their unambiguous identification in the case these roles use XML.
/// </summary>
internal sealed class ClaimedRoles
{
	/// <summary>
	/// Collection of claimed roles
	/// </summary>
	public ClaimedRoleCollection ClaimedRoleCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public ClaimedRoles()
	{
		ClaimedRoleCollection = new ClaimedRoleCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return ClaimedRoleCollection.Count > 0;
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

		ClaimedRoleCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:ClaimedRole", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newClaimedRole = new ClaimedRole();
					newClaimedRole.LoadXml(iterationXmlElement);
					ClaimedRoleCollection.Add(newClaimedRole);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "ClaimedRoles", XadesSignedXml.XadesNamespaceUri);

		if (ClaimedRoleCollection.Count > 0)
		{
			foreach (ClaimedRole claimedRole in ClaimedRoleCollection)
			{
				if (claimedRole.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(claimedRole.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}