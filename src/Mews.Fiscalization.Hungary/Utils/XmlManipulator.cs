using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Hungary.Utils
{
    public static class XmlManipulator
    {
        public static string Serialize<T>(T value)
            where T : class
        {
            var xmlDocument = new XmlDocument();
            var navigator = xmlDocument.CreateNavigator();
            using (var writer = navigator.AppendChild())
            {
                var xmlSerializer = new XmlSerializer(typeof(T), ServiceInfo.XmlNamespace);
                xmlSerializer.Serialize(writer, value);
            }
            return xmlDocument.OuterXml;
        }

        public static T Deserialize<T>(string value)
            where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(value));
        }
    }
}