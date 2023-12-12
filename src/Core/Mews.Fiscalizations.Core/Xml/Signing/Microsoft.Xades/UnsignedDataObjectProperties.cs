using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The UnsignedDataObjectProperties element may contain properties that
/// qualify some of the signed data objects.
/// </summary>
internal sealed class UnsignedDataObjectProperties
{
	/// <summary>
	/// A collection of unsigned data object properties
	/// </summary>
	public UnsignedDataObjectPropertyCollection UnsignedDataObjectPropertyCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public UnsignedDataObjectProperties()
	{
		UnsignedDataObjectPropertyCollection = new UnsignedDataObjectPropertyCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return UnsignedDataObjectPropertyCollection.Count > 0;
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

		UnsignedDataObjectPropertyCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:UnsignedDataObjectProperty", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newUnsignedDataObjectProperty = new UnsignedDataObjectProperty();
					newUnsignedDataObjectProperty.LoadXml(iterationXmlElement);
					UnsignedDataObjectPropertyCollection.Add(newUnsignedDataObjectProperty);
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
		var retVal = creationXmlDocument.CreateElement("UnsignedDataObjectProperties", XadesSignedXml.XadesNamespaceUri);

		if (UnsignedDataObjectPropertyCollection.Count > 0)
		{
			foreach (UnsignedDataObjectProperty unsignedDataObjectProperty in UnsignedDataObjectPropertyCollection)
			{
				if (unsignedDataObjectProperty.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(unsignedDataObjectProperty.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}