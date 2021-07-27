using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class Buyer
    {
        [XmlElement("DatiAnagrafici", Form = XmlSchemaForm.Unqualified)]
        public SimpleIdentityData IdentityData { get; set; }

        [XmlElement("Sede", Form = XmlSchemaForm.Unqualified)]
        public Address OfficeAddress { get; set; }
    }
}