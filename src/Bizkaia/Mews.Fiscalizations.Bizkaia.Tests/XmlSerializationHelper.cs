using FuncSharp;
using Mews.Fiscalizations.Core.Xml;
using System.Text;
using System.Xml;
using XmlSerializer = Mews.Fiscalizations.Core.Xml.XmlSerializer;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    public static class XmlSerializationHelper<T> where T: class
    {
        public static XmlElement Serialize(T t, IEnumerable<string> namespaces) 
        {
            return XmlSerializer.Serialize(t, new XmlSerializationParameters(
            encoding: Encoding.UTF8,
            namespaces: namespaces.Select(ns => new XmlNamespace(ns))
            ));
        }

        public static T Deserialize(string filename)
        {
            string content = File.ReadAllText(filename);
            return XmlSerializer.Deserialize<T>(content);
        }
    }
}
