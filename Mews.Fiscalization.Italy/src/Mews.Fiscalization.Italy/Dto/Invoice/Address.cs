using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class Address
    {
        private string _zip;
        private string _street;
        private string _city;
        private string _houseNumber;
        private string _provinceCode;
        private string _countryCode;

        public Address()
        {
            CountryCode = "IT";
        }

        [XmlElement("Indirizzo", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Street
        {
            get { return _street; }
            set { _street = value.NormalizeString(); }
        }

        [XmlElement("NumeroCivico", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string HouseNumber
        {
            get { return _houseNumber; }
            set { _houseNumber = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("CAP", Form = XmlSchemaForm.Unqualified)]
        public string Zip
        {
            get { return _zip; }
            set { _zip = value.NormalizeZip().NonEmptyValueOrNull(); }
        }

        [XmlElement("Comune", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string City
        {
            get { return _city; }
            set { _city = value.NormalizeString(); }
        }

        [XmlElement("Provincia", Form = XmlSchemaForm.Unqualified)]
        public string ProvinceCode
        {
            get { return _provinceCode; }
            set { _provinceCode = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("Nazione", Form = XmlSchemaForm.Unqualified)]
        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value.NonEmptyValueOrNull(); }
        }
    }
}