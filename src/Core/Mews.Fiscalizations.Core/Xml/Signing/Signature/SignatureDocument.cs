using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;
using Mews.Fiscalizations.Core.Xml.Signing.Utils;

namespace Mews.Fiscalizations.Core.Xml.Signing.Signature;

public sealed class SignatureDocument
{
    public XmlDocument Document { get; set; }

    public XadesSignedXml XadesSignature { get; set; }

    public byte[] GetDocumentBytes()
    {
        CheckSignatureDocument(this);

        using var ms = new MemoryStream();
        Save(ms);

        return ms.ToArray();
    }

    public void Save(Stream output)
    {
        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding()
        };
        using var writer = XmlWriter.Create(output, settings);
        Document.Save(writer);
    }

    internal void UpdateDocument()
    {
        Document ??= new XmlDocument();

        if (Document.DocumentElement is not null)
        {
            var xmlNode = Document.SelectSingleNode("//*[@Id='" + XadesSignature.Signature.Id + "']");
            if (xmlNode is not null)
            {
                var nm = new XmlNamespaceManager(Document.NameTable);
                nm.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);
                nm.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);

                var xmlQPNode = xmlNode.SelectSingleNode("ds:Object/xades:QualifyingProperties", nm);
                var xmlUnsignedPropertiesNode = xmlNode.SelectSingleNode("ds:Object/xades:QualifyingProperties/xades:UnsignedProperties", nm);

                if (xmlUnsignedPropertiesNode is not null)
                {
                    var xmlUnsignedSignaturePropertiesNode = xmlNode.SelectSingleNode("ds:Object/xades:QualifyingProperties/xades:UnsignedProperties/xades:UnsignedSignatureProperties", nm);
                    var xmlUnsignedPropertiesNew = XadesSignature.XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.GetXml();
                    foreach (XmlNode childNode in xmlUnsignedPropertiesNew.ChildNodes)
                    {
                        if (childNode.Attributes["Id"] != null && xmlUnsignedSignaturePropertiesNode.SelectSingleNode("//*[@Id='" + childNode.Attributes["Id"].Value + "']") is null)
                        {
                            var newNode = Document.ImportNode(childNode, deep: true);
                            xmlUnsignedSignaturePropertiesNode.AppendChild(newNode);
                        }
                    }

                    if (XadesSignature.XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.CounterSignatureCollection.Count > 0)
                    {
                        foreach (XadesSignedXml counterSign in XadesSignature.XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.CounterSignatureCollection)
                        {
                            if (xmlNode.SelectSingleNode("//*[@Id='" + counterSign.Signature.Id + "']") == null)
                            {
                                var xmlCounterSignatureNode = Document.CreateElement(XadesSignedXml.XmlXadesPrefix, "CounterSignature", XadesSignedXml.XadesNamespaceUri);
                                xmlUnsignedSignaturePropertiesNode.AppendChild(xmlCounterSignatureNode);
                                xmlCounterSignatureNode.AppendChild(Document.ImportNode(counterSign.GetXml(), deep: true));
                            }
                        }
                    }
                }
                else
                {
                    xmlUnsignedPropertiesNode = Document.ImportNode(XadesSignature.XadesObject.QualifyingProperties.UnsignedProperties.GetXml(), deep: true);
                    xmlQPNode.AppendChild(xmlUnsignedPropertiesNode);
                }
            }
            else
            {
                var xmlSigned = XadesSignature.GetXml();
                var canonicalizedElement = XMLUtil.ApplyTransform(xmlSigned, new XmlDsigC14NTransform());

                var doc = new XmlDocument
                {
                    PreserveWhitespace = true
                };
                doc.LoadXml(Encoding.UTF8.GetString(canonicalizedElement));

                var canonSignature = Document.ImportNode(doc.DocumentElement, deep: true);
                XadesSignature.GetSignatureElement().AppendChild(canonSignature);
            }
        }
        else
        {
            Document.LoadXml(XadesSignature.GetXml().OuterXml);
        }
    }

    private static void CheckSignatureDocument(SignatureDocument sigDocument)
    {
        ArgumentNullException.ThrowIfNull(sigDocument);

        if (sigDocument.Document is null || sigDocument.XadesSignature is null)
        {
            throw new Exception("There is no information about the signature.");
        }
    }
}