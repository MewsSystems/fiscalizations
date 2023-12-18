using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of CRL values
/// </summary>
internal sealed class CRLValues
{
	/// <summary>
	/// Collection of CRLValues
	/// </summary>
	public CRLValueCollection CRLValueCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CRLValues()
	{
		CRLValueCollection = new CRLValueCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CRLValueCollection.Count > 0;
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

		CRLValueCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:EncapsulatedCRLValue", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newCRLValue = new CRLValue();
					newCRLValue.LoadXml(iterationXmlElement);
					CRLValueCollection.Add(newCRLValue);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CRLValues", XadesSignedXml.XadesNamespaceUri);

		if (CRLValueCollection.Count > 0)
		{
			foreach (CRLValue crlValue in CRLValueCollection)
			{
				if (crlValue.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(crlValue.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}