using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The SignedDataObjectProperties element contains properties that qualify
/// some of the signed data objects
/// </summary>
internal sealed class SignedDataObjectProperties
{
	/// <summary>
	/// Collection of signed data object formats
	/// </summary>
	public DataObjectFormatCollection DataObjectFormatCollection { get; set; }

	/// <summary>
	/// Collection of commitment type indications
	/// </summary>
	public CommitmentTypeIndicationCollection CommitmentTypeIndicationCollection { get; set; }

	/// <summary>
	/// Collection of all data object timestamps
	/// </summary>
	public AllDataObjectsTimeStampCollection AllDataObjectsTimeStampCollection { get; set; }

	/// <summary>
	/// Collection of individual data object timestamps
	/// </summary>
	public IndividualDataObjectsTimeStampCollection IndividualDataObjectsTimeStampCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignedDataObjectProperties()
	{
		DataObjectFormatCollection = new DataObjectFormatCollection();
		CommitmentTypeIndicationCollection = new CommitmentTypeIndicationCollection();
		AllDataObjectsTimeStampCollection = new AllDataObjectsTimeStampCollection();
		IndividualDataObjectsTimeStampCollection = new IndividualDataObjectsTimeStampCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return DataObjectFormatCollection.Count > 0
		       || CommitmentTypeIndicationCollection.Count > 0
		       || AllDataObjectsTimeStampCollection.Count > 0
		       || IndividualDataObjectsTimeStampCollection.Count > 0;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
		XmlElement iterationXmlElement;
		TimeStamp newTimeStamp;

        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		DataObjectFormatCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xsd:DataObjectFormat", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newDataObjectFormat = new DataObjectFormat();
					newDataObjectFormat.LoadXml(iterationXmlElement);
					DataObjectFormatCollection.Add(newDataObjectFormat);
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

		xmlNodeList = xmlElement.SelectNodes("xsd:CommitmentTypeIndication", xmlNamespaceManager);
		enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					var newCommitmentTypeIndication = new CommitmentTypeIndication();
					newCommitmentTypeIndication.LoadXml(iterationXmlElement);
					CommitmentTypeIndicationCollection.Add(newCommitmentTypeIndication);
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

		xmlNodeList = xmlElement.SelectNodes("xsd:AllDataObjectsTimeStamp", xmlNamespaceManager);
		enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					newTimeStamp = new TimeStamp("AllDataObjectsTimeStamp");
					newTimeStamp.LoadXml(iterationXmlElement);
					AllDataObjectsTimeStampCollection.Add(newTimeStamp);
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

		xmlNodeList = xmlElement.SelectNodes("xsd:IndividualDataObjectsTimeStamp", xmlNamespaceManager);
		enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					newTimeStamp = new TimeStamp("IndividualDataObjectsTimeStamp");
					newTimeStamp.LoadXml(iterationXmlElement);
					IndividualDataObjectsTimeStampCollection.Add(newTimeStamp);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignedDataObjectProperties", XadesSignedXml.XadesNamespaceUri);

		if (DataObjectFormatCollection.Count > 0)
		{
			foreach (DataObjectFormat dataObjectFormat in DataObjectFormatCollection)
			{
				if (dataObjectFormat.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(dataObjectFormat.GetXml(), true));
				}
			}
		}

		if (CommitmentTypeIndicationCollection.Count > 0)
		{
			foreach (CommitmentTypeIndication commitmentTypeIndication in CommitmentTypeIndicationCollection)
			{
				if (commitmentTypeIndication.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(commitmentTypeIndication.GetXml(), true));
				}
			}
		}

		if (AllDataObjectsTimeStampCollection.Count > 0)
		{
			foreach (TimeStamp timeStamp in AllDataObjectsTimeStampCollection)
			{
				if (timeStamp.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(timeStamp.GetXml(), true));
				}
			}
		}

		if (IndividualDataObjectsTimeStampCollection.Count > 0)
		{
			foreach (TimeStamp timeStamp in IndividualDataObjectsTimeStampCollection)
			{
				if (timeStamp.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(timeStamp.GetXml(), true));
				}
			}
		}

		return retVal;
	}
}