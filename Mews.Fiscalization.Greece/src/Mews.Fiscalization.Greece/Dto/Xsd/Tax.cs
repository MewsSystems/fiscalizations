using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlRoot(Namespace = InvoicesDoc.Namespace)]
    public class Tax
    {
        [XmlElement(ElementName = "id")]
        public byte LineNumber { get; set; }

        [XmlIgnore]
        public bool LineNumberSpecified { get; set; }

        [XmlElement(ElementName = "taxType", IsNullable = false)]
        public TaxType TaxType { get; set; }

        [XmlElement(ElementName = "taxCategory")]
        public byte TaxCategory { get; set; }

        [XmlElement(ElementName = "underlyingValue")]
        public decimal UnderlyingValue { get; set; }

        [XmlIgnore]
        public bool UnderlyingValueSpecified { get; set; }

        [XmlElement(ElementName = "taxAmount", IsNullable = false)]
        public decimal TaxAmount { get; set; }
    }
}
