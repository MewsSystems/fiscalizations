using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The HashDataInfo class contains a uri attribute referencing a data object
/// and a ds:Transforms element indicating the transformations to make to this
/// data object.
/// The sequence of HashDataInfo elements will be used to produce the input of
/// the hash computation process whose result will be included in the
/// timestamp request to be sent to the TSA.
/// </summary>
internal sealed class HashDataInfo
{
	/// <summary>
	/// Uri referencing a data object
	/// </summary>
	public string UriAttribute { get; set; }

	/// <summary>
	/// Transformations to make to this data object
	/// </summary>
	public Transforms Transforms { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public HashDataInfo()
	{
		Transforms = new Transforms();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(UriAttribute) || Transforms != null && Transforms.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        if (xmlElement.HasAttribute("uri"))
		{
			UriAttribute = xmlElement.GetAttribute("uri");
		}
		else
		{
			UriAttribute = "";
			throw new CryptographicException("uri attribute missing");
		}

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:Transforms", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			Transforms = new Transforms();
			Transforms.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("HashDataInfo", XadesSignedXml.XadesNamespaceUri);

		retVal.SetAttribute("uri", UriAttribute);

		if (Transforms != null && Transforms.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(Transforms.GetXml(), true));
		}

		return retVal;
	}
}