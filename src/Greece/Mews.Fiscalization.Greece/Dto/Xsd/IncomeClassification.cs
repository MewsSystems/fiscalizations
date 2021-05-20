using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = IncomeClassification.Namespace)]
    public class IncomeClassification
    {
        public const string Namespace = "https://www.aade.gr/myDATA/incomeClassificaton/v1.0";

        [XmlElement(ElementName = "classificationType")]
        public IncomeClassificationType ClassificationType { get; set; }

        [XmlElement(ElementName = "classificationCategory", IsNullable = false)]
        public IncomeClassificationCategory ClassificationCategory { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }

        [XmlElement(ElementName = "id")]
        public byte SerialNumber { get; set; }

        [XmlIgnore]
        public bool SerialNumberSpecified { get; set; }
    }
}
