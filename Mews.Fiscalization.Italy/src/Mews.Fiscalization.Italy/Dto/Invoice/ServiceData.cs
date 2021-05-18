using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class ServiceData
    {
        [XmlElement("DettaglioLinee", Form = XmlSchemaForm.Unqualified)]
        public InvoiceLine[] InvoiceLines { get; set; }

        [XmlElement("DatiRiepilogo", Form = XmlSchemaForm.Unqualified)]
        public TaxRateSummary[] TaxSummary { get; set; }
    }
}