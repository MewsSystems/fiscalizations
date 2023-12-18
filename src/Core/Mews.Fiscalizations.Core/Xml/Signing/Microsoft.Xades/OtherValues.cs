using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of OtherValues
/// </summary>
internal sealed class OtherValues
{
	/// <summary>
	/// Collection of other values
	/// </summary>
	public OtherValueCollection OtherValueCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OtherValues()
	{
		OtherValueCollection = new OtherValueCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return OtherValueCollection.Count > 0;
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

		OtherValueCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:OtherValue", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newOtherValue = new OtherValue();
					newOtherValue.LoadXml(iterationXmlElement);
					OtherValueCollection.Add(newOtherValue);
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
		var retVal = creationXmlDocument.CreateElement("OtherValues", XadesSignedXml.XadesNamespaceUri);

		if (OtherValueCollection.Count > 0)
		{
			foreach (OtherValue otherValue in OtherValueCollection)
			{
				if (otherValue.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(otherValue.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}