using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Core.Xml;

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

    public static T Deserialize<T>(string content, XmlSerializationParameters parameters = null)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        var contentBytes = parameters.ToOption().GetOrElse(new XmlSerializationParameters()).Encoding.GetBytes(content);
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