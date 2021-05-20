using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class Contact
    {
        [XmlElement("Telefono", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Phone { get; set; }

        [XmlElement("Fax", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Fax { get; set; }

        [XmlElement("Email", Form = XmlSchemaForm.Unqualified)]
        public string Email { get; set; }
    }
}