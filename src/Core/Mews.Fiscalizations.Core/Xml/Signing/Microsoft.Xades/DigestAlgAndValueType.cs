using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class indicates the algortithm used to calculate the digest and
/// the digest value itself
/// </summary>
internal sealed class DigestAlgAndValueType
{
	/// <summary>
	/// The name of the element when serializing
	/// </summary>
	public string TagName { get; set; }

	/// <summary>
	/// Indicates the digest algorithm
	/// </summary>
	public DigestMethod DigestMethod { get; set; }

	/// <summary>
	/// Contains the value of the digest
	/// </summary>
	public byte[] DigestValue { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public DigestAlgAndValueType()
	{
		DigestMethod = new DigestMethod();
		DigestValue = null;
	}

	/// <summary>
	/// Constructor with TagName
	/// </summary>
	/// <param name="tagName">Name of the tag when serializing with GetXml</param>
	public DigestAlgAndValueType(string tagName) : this()
	{
		this.TagName = tagName;
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return DigestMethod is not null && DigestMethod.HasChanged() || DigestValue is not null && DigestValue.Length > 0;
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


		var xmlNodeList = xmlElement.SelectNodes("ds:DigestMethod", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("DigestMethod missing");
		}
		DigestMethod = new DigestMethod();
		DigestMethod.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("ds:DigestValue", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("DigestValue missing");
		}
		DigestValue = Convert.FromBase64String(xmlNodeList.Item(0).InnerText);
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, TagName, XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		if (DigestMethod is not null && DigestMethod.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(DigestMethod.GetXml(), true));
		}
		else
		{
			throw new CryptographicException("DigestMethod element missing in DigestAlgAndValueType");
		}

		if (DigestValue is not null && DigestValue.Length > 0)
		{
			var bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlDSigPrefix, "DigestValue", SignedXml.XmlDsigNamespaceUrl);
			bufferXmlElement.SetAttribute("xmlns:xades", XadesSignedXml.XadesNamespaceUri);
                
			bufferXmlElement.InnerText = Convert.ToBase64String(DigestValue);
			retVal.AppendChild(bufferXmlElement);
		}
		else
		{
			throw new CryptographicException("DigestValue element missing in DigestAlgAndValueType");
		}

		return retVal;
	}
}