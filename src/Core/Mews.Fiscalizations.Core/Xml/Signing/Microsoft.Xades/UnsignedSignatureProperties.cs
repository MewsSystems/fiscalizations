using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// UnsignedSignatureProperties may contain properties that qualify XML
/// signature itself or the signer
/// </summary>
internal sealed class UnsignedSignatureProperties
{
	/// <summary>
	/// A collection of counter signatures
	/// </summary>
	public CounterSignatureCollection CounterSignatureCollection { get; set; }

	/// <summary>
	/// A collection of signature timestamps
	/// </summary>
	public SignatureTimeStampCollection SignatureTimeStampCollection { get; set; }

	/// <summary>
	/// This clause defines the XML element containing the sequence of
	/// references to the full set of CA certificates that have been used
	/// to validate the electronic signature up to (but not including) the
	/// signer's certificate. This is an unsigned property that qualifies
	/// the signature.
	/// An XML electronic signature aligned with the present document MAY
	/// contain at most one CompleteCertificateRefs element.
	/// </summary>
	public CompleteCertificateRefs CompleteCertificateRefs { get; set; }

	/// <summary>
	/// This clause defines the XML element containing a full set of
	/// references to the revocation data that have been used in the
	/// validation of the signer and CA certificates.
	/// This is an unsigned property that qualifies the signature.
	/// The XML electronic signature aligned with the present document
	/// MAY contain at most one CompleteRevocationRefs element.
	/// </summary>
	public CompleteRevocationRefs CompleteRevocationRefs { get; set; }

	/// <summary>
	/// Flag indicating if the RefsOnlyTimeStamp element (or several) is
	/// present (RefsOnlyTimeStampFlag = true).  If one or more
	/// sigAndRefsTimeStamps are present, RefsOnlyTimeStampFlag will be false.
	/// </summary>
	public bool RefsOnlyTimeStampFlag { get; set; }

	/// <summary>
	/// A collection of sig and refs timestamps
	/// </summary>
	public SignatureTimeStampCollection SigAndRefsTimeStampCollection { get; set; }

	/// <summary>
	/// A collection of refs only timestamps
	/// </summary>
	public SignatureTimeStampCollection RefsOnlyTimeStampCollection { get; set; }

	/// <summary>
	/// Certificate values
	/// </summary>
	public CertificateValues CertificateValues { get; set; }

	/// <summary>
	/// Revocation values
	/// </summary>
	public RevocationValues RevocationValues { get; set; }

	/// <summary>
	/// A collection of signature timestamp
	/// </summary>
	public SignatureTimeStampCollection ArchiveTimeStampCollection { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public UnsignedSignatureProperties()
	{
		CounterSignatureCollection = new CounterSignatureCollection();
		SignatureTimeStampCollection = new SignatureTimeStampCollection();
		CompleteCertificateRefs = new CompleteCertificateRefs();
		CompleteRevocationRefs = new CompleteRevocationRefs();
		RefsOnlyTimeStampFlag = false;
		SigAndRefsTimeStampCollection = new SignatureTimeStampCollection();
		RefsOnlyTimeStampCollection = new SignatureTimeStampCollection();
		CertificateValues = new CertificateValues();
		RevocationValues = new RevocationValues();
		ArchiveTimeStampCollection = new SignatureTimeStampCollection();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		var retVal = CounterSignatureCollection.Count > 0;

		if (SignatureTimeStampCollection.Count > 0)
		{
			retVal = true;
		}

		if (CompleteCertificateRefs != null && CompleteCertificateRefs.HasChanged())
		{
			retVal = true;
		}

		if (CompleteRevocationRefs != null && CompleteRevocationRefs.HasChanged())
		{
			retVal = true;
		}

		if (SigAndRefsTimeStampCollection.Count > 0)
		{
			retVal = true;
		}

		if (RefsOnlyTimeStampCollection.Count > 0)
		{
			retVal = true;
		}

		if (CertificateValues != null && CertificateValues.HasChanged())
		{
			retVal = true;
		}

		if (RevocationValues != null && RevocationValues.HasChanged())
		{
			retVal = true;
		}

		if (ArchiveTimeStampCollection.Count > 0)
		{
			retVal = true;
		}

		return retVal;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	/// <param name="counterSignedXmlElement">Element containing parent signature (needed if there are counter signatures)</param>
	public void LoadXml(XmlElement xmlElement, XmlElement counterSignedXmlElement)
	{
		XmlElement iterationXmlElement;
		XadesSignedXml newXadesSignedXml;
		TimeStamp newTimeStamp;

        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);
		xmlNamespaceManager.AddNamespace("xadesv141", XadesSignedXml.XadesNamespace141Uri);

		CounterSignatureCollection.Clear();
		var xmlNodeList = xmlElement.SelectNodes("xades:CounterSignature", xmlNamespaceManager);
		var enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					if (counterSignedXmlElement != null)
					{
						newXadesSignedXml = new XadesSignedXml(counterSignedXmlElement);
					}
					else
					{
						newXadesSignedXml = new XadesSignedXml();
					}

					XmlElement counterSignatureElement = null;
					for (var childNodeCounter = 0; (childNodeCounter < iterationXmlElement.ChildNodes.Count) && (counterSignatureElement == null); childNodeCounter++)
					{
						if (iterationXmlElement.ChildNodes[childNodeCounter] is XmlElement)
						{
							counterSignatureElement = (XmlElement)iterationXmlElement.ChildNodes[childNodeCounter];
						}
					}
					if (counterSignatureElement != null)
					{
						newXadesSignedXml.LoadXml(counterSignatureElement);
						CounterSignatureCollection.Add(newXadesSignedXml);
					}
					else
					{
						throw new CryptographicException("CounterSignature element does not contain signature");
					}
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

		SignatureTimeStampCollection.Clear();
		xmlNodeList = xmlElement.SelectNodes("xades:SignatureTimeStamp", xmlNamespaceManager);
		enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					newTimeStamp = new TimeStamp("SignatureTimeStamp");
					newTimeStamp.LoadXml(iterationXmlElement);
					SignatureTimeStampCollection.Add(newTimeStamp);
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

		xmlNodeList = xmlElement.SelectNodes("xades:CompleteCertificateRefs", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			CompleteCertificateRefs = new CompleteCertificateRefs();
			CompleteCertificateRefs.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		else
		{
			CompleteCertificateRefs = null;
		}

		xmlNodeList = xmlElement.SelectNodes("xades:CompleteRevocationRefs", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			CompleteRevocationRefs = new CompleteRevocationRefs();
			CompleteRevocationRefs.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		else
		{
			CompleteRevocationRefs = null;
		}

		SigAndRefsTimeStampCollection.Clear();
		RefsOnlyTimeStampCollection.Clear();

		xmlNodeList = xmlElement.SelectNodes("xades:SigAndRefsTimeStamp", xmlNamespaceManager);
		if (xmlNodeList.Count > 0)
		{
			RefsOnlyTimeStampFlag = false;
			enumerator = xmlNodeList.GetEnumerator();
			try 
			{
				while (enumerator.MoveNext()) 
				{
					iterationXmlElement = enumerator.Current as XmlElement;
					if (iterationXmlElement != null)
					{
						newTimeStamp = new TimeStamp("SigAndRefsTimeStamp");
						newTimeStamp.LoadXml(iterationXmlElement);
						SigAndRefsTimeStampCollection.Add(newTimeStamp);
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
		else
		{
			xmlNodeList = xmlElement.SelectNodes("xades:RefsOnlyTimeStamp", xmlNamespaceManager);
			if (xmlNodeList.Count > 0)
			{
				RefsOnlyTimeStampFlag = true;
				enumerator = xmlNodeList.GetEnumerator();
				try 
				{
					while (enumerator.MoveNext()) 
					{
						iterationXmlElement = enumerator.Current as XmlElement;
						if (iterationXmlElement != null)
						{
							newTimeStamp = new TimeStamp("RefsOnlyTimeStamp");
							newTimeStamp.LoadXml(iterationXmlElement);
							RefsOnlyTimeStampCollection.Add(newTimeStamp);
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
			else
			{
				RefsOnlyTimeStampFlag = false;
			}
		}

		xmlNodeList = xmlElement.SelectNodes("xades:CertificateValues", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			CertificateValues = new CertificateValues();
			CertificateValues.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		else
		{
			CertificateValues = null;
		}

		xmlNodeList = xmlElement.SelectNodes("xades:RevocationValues", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			RevocationValues = new RevocationValues();
			RevocationValues.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
		else
		{
			RevocationValues = null;
		}

		ArchiveTimeStampCollection.Clear();
		xmlNodeList = xmlElement.SelectNodes("xades:ArchiveTimeStamp", xmlNamespaceManager);

		enumerator = xmlNodeList.GetEnumerator();
		try 
		{
			while (enumerator.MoveNext()) 
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					newTimeStamp = new TimeStamp("ArchiveTimeStamp");
					newTimeStamp.LoadXml(iterationXmlElement);
					ArchiveTimeStampCollection.Add(newTimeStamp);
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

		xmlNodeList = xmlElement.SelectNodes("xadesv141:ArchiveTimeStamp", xmlNamespaceManager);

		enumerator = xmlNodeList.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				iterationXmlElement = enumerator.Current as XmlElement;
				if (iterationXmlElement != null)
				{
					newTimeStamp = new TimeStamp("ArchiveTimeStamp", "xadesv141", XadesSignedXml.XadesNamespace141Uri);
					newTimeStamp.LoadXml(iterationXmlElement);
					ArchiveTimeStampCollection.Add(newTimeStamp);
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
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "UnsignedSignatureProperties", XadesSignedXml.XadesNamespaceUri);

		if (CounterSignatureCollection.Count > 0)
		{
			foreach (XadesSignedXml xadesSignedXml in CounterSignatureCollection)
			{
				var bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CounterSignature", XadesSignedXml.XadesNamespaceUri);
				bufferXmlElement.AppendChild(creationXmlDocument.ImportNode(xadesSignedXml.GetXml(), true));
				retVal.AppendChild(creationXmlDocument.ImportNode(bufferXmlElement, true));
			}
		}

		if (SignatureTimeStampCollection.Count > 0)
		{
			foreach (TimeStamp timeStamp in SignatureTimeStampCollection)
			{
				if (timeStamp.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(timeStamp.GetXml(), true));
				}
			}
		}

		if (CompleteCertificateRefs != null && CompleteCertificateRefs.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CompleteCertificateRefs.GetXml(), true));
		}

		if (CompleteRevocationRefs != null && CompleteRevocationRefs.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CompleteRevocationRefs.GetXml(), true));
		}

		if (!RefsOnlyTimeStampFlag)
		{
			foreach (TimeStamp timeStamp in SigAndRefsTimeStampCollection)
			{
				if (timeStamp.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(timeStamp.GetXml(), true));
				}
			}
		}
		else
		{
			foreach (TimeStamp timeStamp in RefsOnlyTimeStampCollection)
			{
				if (timeStamp.HasChanged())
				{
					retVal.AppendChild(creationXmlDocument.ImportNode(timeStamp.GetXml(), true));
				}
			}
		}

		if (CertificateValues != null && CertificateValues.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CertificateValues.GetXml(), true));
		}

		if (RevocationValues != null && RevocationValues.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(RevocationValues.GetXml(), true));
		}

		if (ArchiveTimeStampCollection.Count > 0)
		{
			foreach (TimeStamp timeStamp in ArchiveTimeStampCollection)
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