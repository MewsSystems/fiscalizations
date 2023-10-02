using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Mews.Fiscalizations.Bizkaia.Tests;

public static class XmlSchemaHelper
{
    public static bool XmlSchemaValidationSucceeds(XmlElement element, string validatingXsdFilename, Dictionary<string, string> schemasDictionary)
    {
        if (!File.Exists(validatingXsdFilename))
        {
            return false;
        }

        var settings = new XmlReaderSettings();
        settings.DtdProcessing = DtdProcessing.Ignore;
        settings.ValidationType = ValidationType.Schema;
        try
        {
            using (StringReader reader = new StringReader(element.OuterXml))
            using (XmlReader xmlReader = XmlReader.Create(reader, settings))
            {
                var xDoc = XDocument.Load(xmlReader);
                var schemas = new XmlSchemaSet();

                foreach (var kvp in schemasDictionary)
                {
                    schemas.Add(kvp.Key, kvp.Value);
                }

                xDoc.Validate(schemas, null);

                return true;
            }
        }
        catch (Exception e)
        {
            var msg = e.Message;
            return false;
        }
    }


}
