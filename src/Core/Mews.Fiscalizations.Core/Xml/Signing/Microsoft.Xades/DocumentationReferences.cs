using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of DocumentationReferences
/// </summary>
internal sealed class DocumentationReferences
{
	/// <summary>
	/// Collection of documentation references
	/// </summary>
	public DocumentationReferenceCollection DocumentationReferenceCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public DocumentationReferences()
	{
		DocumentationReferenceCollection = new DocumentationReferenceCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return DocumentationReferenceCollection.Count > 0;
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

		DocumentationReferenceCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:DocumentationReference", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newDocumentationReference = new DocumentationReference();
					newDocumentationReference.LoadXml(iterationXmlElement);
					DocumentationReferenceCollection.Add(newDocumentationReference);
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
		var retVal = creationXmlDocument.CreateElement("DocumentationReferences", XadesSignedXml.XadesNamespaceUri);

		if (DocumentationReferenceCollection.Count > 0)
		{
			foreach (DocumentationReference documentationReference in DocumentationReferenceCollection)
			{
				if (documentationReference.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(documentationReference.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}