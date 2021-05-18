using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class InvoiceParty
    {
        [XmlElement(ElementName = "vatNumber", IsNullable = false)]
        public string VatNumber { get; set; }

        [XmlElement(ElementName = "country", IsNullable = false)]
        public Country Country { get; set; }

        [XmlElement(ElementName = "branch")]
        public int Branch { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
    }
}
