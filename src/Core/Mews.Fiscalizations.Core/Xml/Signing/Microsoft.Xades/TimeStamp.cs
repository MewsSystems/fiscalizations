using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains timestamp information
/// </summary>
internal sealed class TimeStamp
{
    private EncapsulatedPKIData encapsulatedTimeStamp;
    private XMLTimeStamp xmlTimeStamp;
    private readonly string prefix;
    private readonly string namespaceUri;

    /// <summary>
    /// The name of the element when serializing
    /// </summary>
    public string TagName { get; set; }

    public string Id { get; set; }

    /// <summary>
    /// A collection of hash data infos
    /// </summary>
    public HashDataInfoCollection HashDataInfoCollection { get; set; }

    /// <summary>
    /// The time-stamp generated by a TSA encoded as an ASN.1 data
    /// object
    /// </summary>
    public EncapsulatedPKIData EncapsulatedTimeStamp
    {
        get => encapsulatedTimeStamp;
        set
        {
            encapsulatedTimeStamp = value;
            if (encapsulatedTimeStamp != null)
            {
                xmlTimeStamp = null;
            }
        }
    }

    /// <summary>
    /// The time-stamp generated by a TSA encoded as a generic XML
    /// timestamp
    /// </summary>
    public XMLTimeStamp XMLTimeStamp
    {
        get => xmlTimeStamp;
        set
        {
            xmlTimeStamp = value;
            if (xmlTimeStamp != null)
            {
                encapsulatedTimeStamp = null;
            }
        }
    }

    public CanonicalizationMethod CanonicalizationMethod { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public TimeStamp(string prefix, string namespaceUri)
    {
        HashDataInfoCollection = new HashDataInfoCollection();
        encapsulatedTimeStamp = new EncapsulatedPKIData("EncapsulatedTimeStamp");
        xmlTimeStamp = null;

        this.prefix = prefix;
        this.namespaceUri = namespaceUri;
    }

    /// <summary>
    /// Constructor with TagName
    /// </summary>
    /// <param name="tagName">Name of the tag when serializing with GetXml</param>
    public TimeStamp(string tagName)
        : this(XadesSignedXml.XmlXadesPrefix, XadesSignedXml.XadesNamespaceUri)
    {
        this.TagName = tagName;
    }

    /// <summary>
    /// Constructor with TagName and prefix
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="prefix"></param>
    /// <param name="namespaceUri"></param>
    public TimeStamp(string tagName, string prefix, string namespaceUri)
        : this(prefix, namespaceUri)
    {
        this.TagName = tagName;
    }

    /// <summary>
    /// Check to see if something has changed in this instance and needs to be serialized
    /// </summary>
    /// <returns>Flag indicating if a member needs serialization</returns>
    public bool HasChanged()
    {
        return HashDataInfoCollection.Count > 0 || encapsulatedTimeStamp != null && encapsulatedTimeStamp.HasChanged() || xmlTimeStamp != null && xmlTimeStamp.HasChanged();
    }

    /// <summary>
    /// Load state from an XML element
    /// </summary>
    /// <param name="xmlElement">XML element containing new state</param>
    public void LoadXml(XmlElement xmlElement)
    {
        ArgumentNullException.ThrowIfNull(xmlElement);

        Id = xmlElement.HasAttribute("Id") ? xmlElement.GetAttribute("Id") : "";

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
        xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);
        xmlNamespaceManager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);

        HashDataInfoCollection.Clear();
        var xmlNodeList = xmlElement.SelectNodes("xades:HashDataInfo", xmlNamespaceManager);
        var enumerator = xmlNodeList.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var iterationXmlElement = enumerator.Current as XmlElement;
                if (iterationXmlElement != null)
                {
                    var newHashDataInfo = new HashDataInfo();
                    newHashDataInfo.LoadXml(iterationXmlElement);
                    HashDataInfoCollection.Add(newHashDataInfo);
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

        var canonicalizationNode = xmlElement.SelectSingleNode("ds:CanonicalizationMethod", xmlNamespaceManager);

        if (canonicalizationNode != null)
        {
            CanonicalizationMethod = new CanonicalizationMethod();
            CanonicalizationMethod.LoadXml((XmlElement)canonicalizationNode);
        }

        xmlNodeList = xmlElement.SelectNodes("xades:EncapsulatedTimeStamp", xmlNamespaceManager);

        if (xmlNodeList.Count != 0)
        {
            encapsulatedTimeStamp = new EncapsulatedPKIData("EncapsulatedTimeStamp");
            encapsulatedTimeStamp.LoadXml((XmlElement)xmlNodeList.Item(0));
            xmlTimeStamp = null;
        }
        else
        {
            XmlNode nodeEncapsulatedTimeStamp = null;

            foreach (XmlNode node in xmlElement.ChildNodes)
            {
                if (node.Name == "EncapsulatedTimeStamp")
                {
                    nodeEncapsulatedTimeStamp = node;
                    break;
                }
            }

            if (nodeEncapsulatedTimeStamp != null)
            {
                encapsulatedTimeStamp = new EncapsulatedPKIData("EncapsulatedTimeStamp");
                encapsulatedTimeStamp.LoadXml((XmlElement)nodeEncapsulatedTimeStamp);
                xmlTimeStamp = null;
            }
            else
            {
                xmlNodeList = xmlElement.SelectNodes("xades:XMLTimeStamp", xmlNamespaceManager);
                if (xmlNodeList.Count != 0)
                {
                    xmlTimeStamp = new XMLTimeStamp();
                    xmlTimeStamp.LoadXml((XmlElement)xmlNodeList.Item(0));
                    encapsulatedTimeStamp = null;

                }
                else
                {
                    throw new CryptographicException("EncapsulatedTimeStamp or XMLTimeStamp missing");
                }
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

        var retVal = creationXmlDocument.CreateElement(prefix, TagName, namespaceUri);

        retVal.SetAttribute("Id", Id);

        if (CanonicalizationMethod != null)
        {
            retVal.AppendChild(creationXmlDocument.ImportNode(CanonicalizationMethod.GetXml(), true));
        }

        if (HashDataInfoCollection.Count > 0)
        {
            foreach (HashDataInfo hashDataInfo in HashDataInfoCollection)
            {
                if (hashDataInfo.HasChanged())
                {
                    retVal.AppendChild(creationXmlDocument.ImportNode(hashDataInfo.GetXml(), true));
                }
            }
        }

        if (encapsulatedTimeStamp != null && encapsulatedTimeStamp.HasChanged())
        {
            retVal.AppendChild(creationXmlDocument.ImportNode(encapsulatedTimeStamp.GetXml(), true));
        }
        else
        {
            if (xmlTimeStamp != null && xmlTimeStamp.HasChanged())
            {
                retVal.AppendChild(creationXmlDocument.ImportNode(xmlTimeStamp.GetXml(), true));
            }
            else
            {
                throw new CryptographicException("EncapsulatedTimeStamp or XMLTimeStamp element missing");
            }
        }

        return retVal;
    }
}