using FuncSharp;
using Mews.Fiscalizations.Core.Xml;
using System.Text;
using System.Xml;
using XmlSerializer = Mews.Fiscalizations.Core.Xml.XmlSerializer;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    public static class XmlSerializationHelper<T>
    {
        public static XmlElement Serialize<T>(T t, IEnumerable<string> namespaces) where T : class
        {
            return XmlSerializer.Serialize(t, new XmlSerializationParameters(
            encoding: Encoding.UTF8,
            namespaces: namespaces.Select(ns => new XmlNamespace(ns))
            ));
        }

        public static T Deserialize<T>(string filename) where T: class
        {
            string content = File.ReadAllText(filename);
            return XmlSerializer.Deserialize<T>(content);
        }
    }
}
