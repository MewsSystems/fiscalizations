using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of OCSPRefs
/// </summary>
internal sealed class OCSPRefs
{
	/// <summary>
	/// Collection of OCSP refs
	/// </summary>
	public OCSPRefCollection OCSPRefCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OCSPRefs()
	{
		OCSPRefCollection = new OCSPRefCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return OCSPRefCollection.Count > 0;
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

		OCSPRefCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:OCSPRef", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newOCSPRef = new OCSPRef();
					newOCSPRef.LoadXml(iterationXmlElement);
					OCSPRefCollection.Add(newOCSPRef);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "OCSPRefs", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		if (OCSPRefCollection.Count > 0)
		{
			foreach (OCSPRef ocspRef in OCSPRefCollection)
			{
				if (ocspRef.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(ocspRef.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}