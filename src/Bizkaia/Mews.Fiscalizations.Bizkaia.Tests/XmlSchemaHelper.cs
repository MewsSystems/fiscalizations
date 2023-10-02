using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Mews.Fiscalizations.Bizkaia.Tests;

public static class XmlSchemaHelper
{
    private const string SignatureSchemaFilename = @"..\..\..\Xsd\xmldsig-core-schema.xsd";
    private const string NamespaceName = "xmldsig-core-schema";

    public static bool XmlSchemaValidationSucceeds(string xmlFilenameToValidate, string validatingXsd)
    {
        if (!File.Exists(validatingXsd) || !File.Exists(xmlFilenameToValidate) || !File.Exists(SignatureSchemaFilename))
        {
            return false;
        }

        XDocument xDoc = null;
        var settings = new XmlReaderSettings();
        settings.DtdProcessing = DtdProcessing.Parse;
        try
        {
            using (XmlReader xmlReader = XmlReader.Create(xmlFilenameToValidate, settings))
            {
                xDoc = XDocument.Load(xmlReader);
                var schemas = new XmlSchemaSet();
                
                schemas.Add(NamespaceName, SignatureSchemaFilename);
                using (var fs = File.OpenRead(SignatureSchemaFilename))
                using (var reader = XmlReader.Create(fs, new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Parse
                })) { schemas.Add(@"http://www.w3.org/2000/09/xmldsig#", reader); }

                xDoc.Validate(schemas, null);

                return true;
            }
        } catch (Exception e)
        {
            var msg = e.Message;
            return false;
        }
        
        
    }


}
