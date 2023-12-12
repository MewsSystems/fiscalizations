using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The CommitmentTypeQualifier element provides means to include
/// additional qualifying information on the commitment made by the signer
/// </summary>
internal sealed class CommitmentTypeQualifiers
{
	/// <summary>
	/// Collection of commitment type qualifiers
	/// </summary>
	public CommitmentTypeQualifierCollection CommitmentTypeQualifierCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CommitmentTypeQualifiers()
	{
		CommitmentTypeQualifierCollection = new CommitmentTypeQualifierCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CommitmentTypeQualifierCollection.Count > 0;
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

		CommitmentTypeQualifierCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:CommitmentTypeQualifier", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				if (enumerator.Current is XmlElement iterationXmlElement)
				{
					var newCommitmentTypeQualifier = new CommitmentTypeQualifier();
					newCommitmentTypeQualifier.LoadXml(iterationXmlElement);
					CommitmentTypeQualifierCollection.Add(newCommitmentTypeQualifier);
				}
			}
		}
		finally 
		{
			var disposable = enumerator as IDisposable;
			disposable?.Dispose();
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CommitmentTypeQualifiers", XadesSignedXml.XadesNamespaceUri);

		if (CommitmentTypeQualifierCollection.Count > 0)
		{
			foreach (CommitmentTypeQualifier commitmentTypeQualifier in CommitmentTypeQualifierCollection)
			{
				if (commitmentTypeQualifier.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(commitmentTypeQualifier.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}