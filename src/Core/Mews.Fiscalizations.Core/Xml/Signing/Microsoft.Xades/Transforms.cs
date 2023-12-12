using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The Transforms element contains a collection of transformations
/// </summary>
internal sealed class Transforms
{
	/// <summary>
	/// A collection of transforms
	/// </summary>
	public TransformCollection TransformCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public Transforms()
	{
		TransformCollection = new TransformCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return TransformCollection.Count > 0;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);

		TransformCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("ds:Transform", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newTransform = new Transform();
					newTransform.LoadXml(iterationXmlElement);
					TransformCollection.Add(newTransform);
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
		var retVal = creationXmlDocument.CreateElement("Transforms", XadesSignedXml.XadesNamespaceUri);

		if (TransformCollection.Count > 0)
		{
			foreach (Transform transform in TransformCollection)
			{
				if (transform.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(transform.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}