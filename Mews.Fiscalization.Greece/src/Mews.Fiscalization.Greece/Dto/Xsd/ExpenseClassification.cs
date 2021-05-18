using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = ExpenseClassification.Namespace)]
    public class ExpenseClassification
    {
        public const string Namespace = "https://www.aade.gr/myDATA/expensesClassificaton/v1.0";

        [XmlElement(ElementName = "id")]
        public byte SerialNumber { get; set; }

        [XmlIgnore]
        public bool SerialNumberSpecified { get; set; }

        [XmlElement(ElementName = "classificationType")]
        public ExpenseClassificationType ClassificationType { get; set; }

        [XmlElement(ElementName = "classificationCategory")]
        public ExpenseClassificationCategory ClassificationCategory { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }
    }
}
