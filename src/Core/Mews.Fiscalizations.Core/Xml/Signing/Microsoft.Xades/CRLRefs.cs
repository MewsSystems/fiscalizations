using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// Class that contains a collection of CRL references
/// </summary>
internal sealed class CRLRefs
{
	/// <summary>
	/// Collection of 
	/// </summary>
	public CRLRefCollection CRLRefCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CRLRefs()
	{
		CRLRefCollection = new CRLRefCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CRLRefCollection.Count > 0;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

		CRLRefCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xades:CRLRef", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newCRLRef = new CRLRef();
					newCRLRef.LoadXml(iterationXmlElement);
					CRLRefCollection.Add(newCRLRef);
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
		var retVal = creationXmlDocument.CreateElement("CRLRefs", XadesSignedXml.XadesNamespaceUri);

		if (CRLRefCollection.Count > 0)
		{
			foreach (CRLRef crlRef in CRLRefCollection)
			{
				if (crlRef.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(crlRef.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}