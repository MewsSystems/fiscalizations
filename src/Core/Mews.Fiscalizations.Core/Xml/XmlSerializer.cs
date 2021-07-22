using System.IO;
using System.Xml;
using System.Xml.Serialization;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Xml
{
    public sealed class XmlSerializer
    {
        public static XmlElement Serialize<T>(T value, XmlSerializationParameters parameters = null)
            where T : class
        {
            var namespaceSerializer = new XmlSerializerNamespaces();
            var namespaces = parameters.ToOption().FlatMap(d => d.Namespaces).Flatten();

            foreach (var ns in namespaces)
            {
                namespaceSerializer.Add(ns.Prefix, ns.Url);
            }

            var xmlDocument = new XmlDocument();
            var navigator = xmlDocument.CreateNavigator();
            using (var writer = navigator.AppendChild())
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                xmlSerializer.Serialize(writer, value, namespaceSerializer);
                writer.Flush();
                writer.Close();
            }
            return xmlDocument.DocumentElement;
        }

        public static T Deserialize<T>(XmlElement xmlElement)
            where T : class
        {
            using (var reader = new StringReader(xmlElement.OuterXml))
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return xmlSerializer.Deserialize(reader) as T;
            }
        }

        public static T Deserialize<T>(string content)
            where T : class
        {
            return Deserialize<T>(ToXmlElement(content));
        }

        private static XmlElement ToXmlElement(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);
            return document.DocumentElement;
        }
    }
}
