using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class ElectronicInvoiceBody
    {
        [XmlElement("DatiGenerali", Form = XmlSchemaForm.Unqualified)]
        public GeneralData GeneralData { get; set; }

        [XmlElement("DatiBeniServizi", Form = XmlSchemaForm.Unqualified)]
        public ServiceData ServiceData { get; set; }

        /// <summary>
        /// Required if the documented is related to sale of new vehicles.
        /// </summary>
        [XmlElement("DatiVeicoli", Form = XmlSchemaForm.Unqualified)]
        public VehicleData VehicleData { get; set; }

        [XmlElement("DatiPagamento", Form = XmlSchemaForm.Unqualified)]
        public PaymentData[] PaymentData { get; set; }

        [XmlElement("Allegati", Form = XmlSchemaForm.Unqualified)]
        public InvoiceAttachment[] Attachments { get; set; }
    }
}