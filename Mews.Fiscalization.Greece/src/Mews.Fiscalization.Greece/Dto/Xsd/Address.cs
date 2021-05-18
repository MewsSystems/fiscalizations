using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class Address
    {
        [XmlElement(ElementName = "street")]
        public string Street { get; set; }

        [XmlElement(ElementName = "number")]
        public string Number { get; set; }

        [XmlElement(ElementName = "postalCode", IsNullable = false)]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "city", IsNullable = false)]
        public string City { get; set; }
    }
}
