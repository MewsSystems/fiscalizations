using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of OtherRefs
/// </summary>
internal sealed class OtherRefs
{
	/// <summary>
	/// Collection of other refs
	/// </summary>
	public OtherRefCollection OtherRefCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OtherRefs()
	{
		OtherRefCollection = new OtherRefCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return OtherRefCollection.Count > 0;
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

		OtherRefCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:OtherRef", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newOtherRef = new OtherRef();
					newOtherRef.LoadXml(iterationXmlElement);
					OtherRefCollection.Add(newOtherRef);
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
		var retVal = creationXmlDocument.CreateElement("OtherRefs", XadesSignedXml.XadesNamespaceUri);

		if (OtherRefCollection.Count > 0)
		{
			foreach (OtherRef otherRef in OtherRefCollection)
			{
				if (otherRef.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(otherRef.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}