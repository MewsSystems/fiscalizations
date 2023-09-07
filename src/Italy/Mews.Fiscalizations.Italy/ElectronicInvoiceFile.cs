using System.Text;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using Mews.Fiscalizations.Italy.Utils;

namespace Mews.Fiscalizations.Italy;

public class ElectronicInvoiceFile
{
    private const string XmlFileHeader = @"<?xml version=""1.0"" encoding=""utf-8""?>";

    public byte[] Data { get; }

    public string FileName { get; }

    public string Content
    {
        get { return Encoding.UTF8.GetString(Data); }
    }

    public ElectronicInvoiceFile(ElectronicInvoice invoice)
    {
        Data = Encoding.UTF8.GetBytes(Serialize(invoice));
        FileName = FileUtils.SanitizePath($"{invoice.Header.TransmissionData.SequentialNumber}.xml");
    }

    private string Serialize(ElectronicInvoice invoice)
    {
        var parameters = new XmlSerializationParameters(namespaces: new XmlNamespace("p", ElectronicInvoice.Namespace).ToEnumerable());
        var xml = XmlSerializer.Serialize(invoice, parameters).OuterXml;
        return $@"{XmlFileHeader}{xml}";
    }
}