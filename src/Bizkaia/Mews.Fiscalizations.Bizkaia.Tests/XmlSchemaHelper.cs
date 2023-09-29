using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Mews.Fiscalizations.Bizkaia.Tests;

public static class XmlSchemaHelper
{
    public static bool XmlSchemaValidationSucceeds(string xmlFilenameToValidate, string validatingXsd)
    {
        if (!File.Exists(validatingXsd) || !File.Exists(xmlFilenameToValidate))
        {
            return false;
        }

        //loads the xsd file
        MemoryStream xsd = new MemoryStream(File.ReadAllBytes(validatingXsd));
        var eventHandler = new ValidationEventHandler(Validation);
        var schema = XmlSchema.Read(xsd, eventHandler);

        if (schema is null)
        {
            return false;
        }

        var settings = new XmlReaderSettings();
        settings.Schemas.Add(schema);
        settings.ValidationType = ValidationType.Schema;

        //loads the xml file
        var xml = new MemoryStream(File.ReadAllBytes(xmlFilenameToValidate));
        var reader = XmlReader.Create(xml, settings);

        var document = new XmlDocument();
        document.Load(reader);

        try
        {
            document.Validate(eventHandler);
            return true;
        }
        catch
        {
            return false;
        }
        
    }

    private static void Validation(object sender, ValidationEventArgs e)
    {
        switch (e.Severity)
        {
            case XmlSeverityType.Error:
                throw new InvalidOperationException($"ERROR {e.Message}");
            case XmlSeverityType.Warning:
                throw new InvalidOperationException($"WARNING {e.Message}");
                
        }
    }
}
