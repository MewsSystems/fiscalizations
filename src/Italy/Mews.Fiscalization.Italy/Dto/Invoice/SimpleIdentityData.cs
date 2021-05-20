using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class SimpleIdentityData
    {
        private string _taxCode;

        [XmlElement("IdFiscaleIVA", Form = XmlSchemaForm.Unqualified)]
        public SenderId VatTaxId { get; set; }

        [XmlElement("CodiceFiscale", Form = XmlSchemaForm.Unqualified)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("Anagrafica", Form = XmlSchemaForm.Unqualified)]
        public Identity Identity { get; set; }
    }
}