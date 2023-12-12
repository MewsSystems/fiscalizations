using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of SigPolicyQualifiers
/// </summary>
internal sealed class SigPolicyQualifiers
{
	/// <summary>
	/// A collection of sig policy qualifiers
	/// </summary>
	public SigPolicyQualifierCollection SigPolicyQualifierCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SigPolicyQualifiers()
	{
		SigPolicyQualifierCollection = new SigPolicyQualifierCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return SigPolicyQualifierCollection.Count > 0;
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

		SigPolicyQualifierCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:SigPolicyQualifier", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var subElement = (XmlElement)iterationXmlElement.SelectSingleNode("xsd:SPURI", xmlNamespaceManager);
					if (subElement != null)
					{
						var newSPUri = new SPUri();
						newSPUri.LoadXml(iterationXmlElement);
						SigPolicyQualifierCollection.Add(newSPUri);
					}
					else
					{
						subElement = (XmlElement)iterationXmlElement.SelectSingleNode("xsd:SPUserNotice", xmlNamespaceManager);
						if (subElement != null)
						{
							var newSPUserNotice = new SPUserNotice();
							newSPUserNotice.LoadXml(iterationXmlElement);
							SigPolicyQualifierCollection.Add(newSPUserNotice);
						}
						else
						{
							var newSigPolicyQualifier = new SigPolicyQualifier();
							newSigPolicyQualifier.LoadXml(iterationXmlElement);
							SigPolicyQualifierCollection.Add(newSigPolicyQualifier);
						}
					}
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SigPolicyQualifiers", XadesSignedXml.XadesNamespaceUri);

		if (SigPolicyQualifierCollection.Count > 0)
		{
			foreach (SigPolicyQualifier sigPolicyQualifier in SigPolicyQualifierCollection)
			{
				if (sigPolicyQualifier.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(sigPolicyQualifier.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}