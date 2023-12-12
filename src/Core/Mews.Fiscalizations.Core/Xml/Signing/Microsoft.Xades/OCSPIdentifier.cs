using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class includes the name of the server that has produced the
/// referenced response (ResponderID element) and the time indication in
/// the "ProducedAt" field of the referenced response (ProducedAt element).
/// The optional URI attribute could serve to indicate where the OCSP
/// response identified is archived.
/// </summary>
internal sealed class OCSPIdentifier
{
	private DateTime producedAt;

	/// <summary>
	/// The optional URI attribute could serve to indicate where the OCSP
	/// response is archived
	/// </summary>
	public string UriAttribute { get; set; }

	/// <summary>
	/// The ID of the server that has produced the referenced response
	/// </summary>
	public string ResponderID { get; set; }

	/// <summary>
	/// Time indication in the referenced response
	/// </summary>
	public DateTime ProducedAt
	{
		get => producedAt;
		set => producedAt = value;
	}


	/// <summary>
	/// Identifier is by key
	/// </summary>
	public bool ByKey
	{
		get;
		set;
	}

	/// <summary>
	/// Default constructor
	/// </summary>
	public OCSPIdentifier()
	{
		producedAt = DateTime.MinValue;
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(UriAttribute) || !string.IsNullOrEmpty(ResponderID) || producedAt != DateTime.MinValue;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        if (xmlElement.HasAttribute("URI"))
		{
			UriAttribute = xmlElement.GetAttribute("URI");
		}

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xades:ResponderID", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			var child = xmlNodeList.Item(0).ChildNodes.Item(0);

			ByKey = child.Name.Contains("ByKey");
			ResponderID = child.InnerText;
		}

		xmlNodeList = xmlElement.SelectNodes("xades:ProducedAt", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			producedAt = XmlConvert.ToDateTime(xmlNodeList.Item(0).InnerText, XmlDateTimeSerializationMode.Local);
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		XmlElement bufferXmlElement;

		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "OCSPIdentifier", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		if (!string.IsNullOrEmpty(UriAttribute))
		{
			retVal.SetAttribute("URI", UriAttribute);
		}

		if (!string.IsNullOrEmpty(ResponderID))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "ResponderID", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

			XmlElement bufferXmlElement2;

			if (!ByKey && ResponderID.Contains(","))
			{
				bufferXmlElement2 = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "ByName", XadesSignedXml.XadesNamespaceUri);
			}
			else
			{
				bufferXmlElement2 = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "ByKey", XadesSignedXml.XadesNamespaceUri);
			}

			bufferXmlElement2.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);
			bufferXmlElement2.InnerText = ResponderID;
                
			bufferXmlElement.AppendChild(bufferXmlElement2);

			retVal.AppendChild(bufferXmlElement);
		}

		if (producedAt != DateTime.MinValue)
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "ProducedAt", XadesSignedXml.XadesNamespaceUri);

			var truncatedDateTime = producedAt.AddTicks(-(producedAt.Ticks % TimeSpan.TicksPerSecond));

			bufferXmlElement.InnerText = XmlConvert.ToString(truncatedDateTime, XmlDateTimeSerializationMode.Local);        

			bufferXmlElement.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);
			retVal.AppendChild(bufferXmlElement);
		}

		return retVal;
	}
}