using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;

public static class XmlSchemaHelper
{
    public static void RunXmlSchemaValidation(XmlElement element, string validatingXsdFilename, Dictionary<string, string> schemasDictionary)
    {
        var settings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Ignore,
            ValidationType = ValidationType.Schema,
        };
        
        using (var reader = new StringReader(element.OuterXml))
        using (var xmlReader = XmlReader.Create(reader, settings))
        {
            var xDoc = XDocument.Load(xmlReader);
            var schemas = new XmlSchemaSet();

            foreach (var kvp in schemasDictionary)
            {
                schemas.Add(kvp.Key, kvp.Value);
            }

            xDoc.Validate(schemas, null);

        }

    }

}
