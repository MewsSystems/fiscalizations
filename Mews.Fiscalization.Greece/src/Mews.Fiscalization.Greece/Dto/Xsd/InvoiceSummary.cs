using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class InvoiceSummary
    {
        [XmlElement(ElementName = "totalNetValue")]
        public decimal TotalNetValue { get; set; }

        [XmlElement(ElementName = "totalVatAmount")]
        public decimal TotalVatAmount { get; set; }

        [XmlElement(ElementName = "totalWithheldAmount")]
        public decimal TotalWithheldAmount { get; set; }

        [XmlElement(ElementName = "totalFeesAmount")]
        public decimal TotalFeesAmount { get; set; }

        [XmlElement(ElementName = "totalStampDutyAmount")]
        public decimal TotalStampDutyAmount { get; set; }

        [XmlElement(ElementName = "totalOtherTaxesAmount")]
        public decimal TotalOtherTaxesAmount { get; set; }

        [XmlElement(ElementName = "totalDeductionsAmount")]
        public decimal TotalDeductionsAmount { get; set; }

        [XmlElement(ElementName = "totalGrossValue")]
        public decimal TotalGrossValue { get; set; }

        [XmlElement(ElementName = "incomeClassification")]
        public IncomeClassification[] IncomeClassification { get; set; }

        [XmlElement(ElementName = "expensesClassification")]
        public ExpenseClassification[] ExpenseClassification { get; set; }
    }
}
