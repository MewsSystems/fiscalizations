using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains identifying numbers for a group of textual statements
/// so that the XAdES based application can get the explicit notices from a
/// notices file
/// </summary>
internal sealed class NoticeNumbers
{
	/// <summary>
	/// Collection of notice numbers
	/// </summary>
	public NoticeNumberCollection NoticeNumberCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public NoticeNumbers()
	{
		NoticeNumberCollection = new NoticeNumberCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return NoticeNumberCollection.Count > 0;
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

		NoticeNumberCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:int", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newNoticeNumber = int.Parse(iterationXmlElement.InnerText);
					NoticeNumberCollection.Add(newNoticeNumber);
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
		var retVal = creationXmlDocument.CreateElement("NoticeNumbers", XadesSignedXml.XadesNamespaceUri);

		if (NoticeNumberCollection.Count > 0)
		{
			foreach (int noticeNumber in NoticeNumberCollection)
			{
				var bufferXmlElement = creationXmlDocument.CreateElement("int", XadesSignedXml.XadesNamespaceUri);
				bufferXmlElement.InnerText = noticeNumber.ToString();
				retVal.AppendChild(bufferXmlElement);
			}
		}

		return retVal;
	}
}