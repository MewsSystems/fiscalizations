using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Communication
{
    internal class XmlManipulator
    {
        internal static T Deserialize<T>(XmlElement xmlElement)
            where T : class, new()
        {
            using (var reader = new StringReader(xmlElement.OuterXml))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return xmlSerializer.Deserialize(reader) as T;
            }
        }

        internal static XmlElement Serialize<T>(T value)
            where T : class
        {
            var xmlDocument = new XmlDocument();
            var navigator = xmlDocument.CreateNavigator();
            using (var writer = navigator.AppendChild())
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var nameSpaces = GetSiiNameSpaces();
                xmlSerializer.Serialize(writer, value, nameSpaces);
            }
            return xmlDocument.DocumentElement;
        }

        internal static XmlSerializerNamespaces GetSiiNameSpaces()
        {
            var result = new XmlSerializerNamespaces();
            result.Add("sii", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd");
            result.Add("siiR", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd");
            result.Add("siiLRC", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd");
            result.Add("siiLRRC", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd");
            return result;
        }
    }
}
