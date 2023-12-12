using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains a collection of OCSPValues
/// </summary>
internal sealed class OCSPValues
{
	/// <summary>
	/// Collection of OCSP values
	/// </summary>
	public OCSPValueCollection OCSPValueCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OCSPValues()
	{
		OCSPValueCollection = new OCSPValueCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return OCSPValueCollection.Count > 0;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

		OCSPValueCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xades:EncapsulatedOCSPValue", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext())
			{
				var iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newOCSPValue = new OCSPValue();
					newOCSPValue.LoadXml(iterationXmlElement);
					OCSPValueCollection.Add(newOCSPValue);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "OCSPValues", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);


		if (OCSPValueCollection.Count > 0)
		{
			foreach (OCSPValue ocspValue in OCSPValueCollection)
			{
				if (ocspValue.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(ocspValue.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}