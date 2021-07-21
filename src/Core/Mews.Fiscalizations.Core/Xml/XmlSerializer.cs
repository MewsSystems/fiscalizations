using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Core.Xml
{
    public sealed class XmlSerializer
    {
        public static XmlElement Serialize<T>(T value, XmlSerializationData serializationData = null)
            where T : class
        {
            var namespaceSerializer = new XmlSerializerNamespaces();
            var namespaces = serializationData.ToOption().Match(
                d => d.Namespaces.Flatten(),
                _ => Enumerable.Empty<XmlNamespace>()
            );
            namespaces.ForEach(n => namespaceSerializer.Add(n.Prefix, n.Url));

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

        public static T Deserialize<T>(string content, XmlSerializationData serializationData = null)
            where T : class
        {
            Check.NonEmpty(content, $"Empty {nameof(content)} cannot be deserialized.");

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var contentBytes = serializationData.ToOption().GetOrElse(new XmlSerializationData()).Encoding.GetBytes(content);
            using (var stream = new MemoryStream(contentBytes, index: 0, count: contentBytes.Length))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    reader.XmlResolver = null;
                    return (T)serializer.Deserialize(reader);
                }
            }
        }
    }
}
