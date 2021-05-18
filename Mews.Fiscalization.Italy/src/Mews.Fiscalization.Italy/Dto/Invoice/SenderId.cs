using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class SenderId
    {
        private string _taxCode;
        private string _countryCode;

        [XmlElement("IdPaese", Form = XmlSchemaForm.Unqualified)]
        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("IdCodice", Form = XmlSchemaForm.Unqualified)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value.NonEmptyValueOrNull(); }
        }
    }
}