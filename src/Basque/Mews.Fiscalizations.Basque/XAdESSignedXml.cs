using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Basque
{
    internal sealed class XAdESSignedXml : SignedXml
    {
        private readonly List<DataObject> DataObjects = new List<DataObject>();

        public XAdESSignedXml(XmlDocument document)
            : base(document)
        {
        }

        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            var xmlElement = base.GetIdElement(doc, id);
            if (xmlElement.IsNotNull())
            {
                return xmlElement;
            }

            foreach (var dataObject in DataObjects)
            {
                var nodeWithSameId = FindNodeWithAttributeValueIn(dataObject.Data, "Id", id);
                if (nodeWithSameId.IsNotNull())
                {
                    return nodeWithSameId;
                }
            }
            if (KeyInfo.IsNotNull())
            {
                var nodeWithSameId = FindNodeWithAttributeValueIn(KeyInfo.GetXml().SelectNodes("."), "Id", id);
                if (nodeWithSameId.IsNotNull())
                {
                    return nodeWithSameId;
                }
            }
            return null;
        }

        public new void AddObject(DataObject dataObject)
        {
            base.AddObject(dataObject);
            DataObjects.Add(dataObject);
        }

        public XmlElement FindNodeWithAttributeValueIn(XmlNodeList nodeList, string attributeName, string value)
        {
            foreach (XmlNode node in nodeList)
            {
                var nodeWithSameId = FindNodeWithAttributeValueIn(node, attributeName, value);
                if (nodeWithSameId.IsNotNull())
                {
                    return nodeWithSameId;
                }
            }
            return null;
        }

        private XmlElement FindNodeWithAttributeValueIn(XmlNode node, string attributeName, string value)
        {
            var attributeValueInNode = GetAttributeValueInNodeOrNull(node, attributeName);
            if ((attributeValueInNode.IsNotNull()) && attributeValueInNode.Equals(value))
            {
                return (XmlElement)node;
            }
            return FindNodeWithAttributeValueIn(node.ChildNodes, attributeName, value);
        }

        private string GetAttributeValueInNodeOrNull(XmlNode node, string attributeName)
        {
            if (node.Attributes.IsNotNull())
            {
                var attribute = node.Attributes[attributeName];
                if (attribute.IsNotNull())
                {
                    return attribute.Value;
                }
            }
            return null;
        }
    }
}