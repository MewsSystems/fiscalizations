using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class InvoiceDetail
    {
        [XmlElement(ElementName = "lineNumber", IsNullable = false)]
        public int LineNumber { get; set; }

        [XmlElement(ElementName = "quantity")]
        public decimal Quantity { get; set; }

        [XmlIgnore]
        public bool QuantitySpecified { get; set; }

        [XmlElement(ElementName = "measurementUnit")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        [XmlIgnore]
        public bool UnitOfMeasurementSpecified { get; set; }

        [XmlElement(ElementName = "invoiceDetailType")]
        public SelfBillingRemarkType SelfBillingRemarkType { get; set; }

        [XmlIgnore]
        public bool SelfBillingRemarkTypeSpecified { get; set; }

        [XmlElement(ElementName = "netValue", IsNullable = false)]
        public decimal NetValue { get; set; }

        [XmlElement(ElementName = "vatCategory", IsNullable = false)]
        public VatCategory VatCategory { get; set; }

        [XmlElement(ElementName = "vatAmount", IsNullable = false)]
        public decimal VatAmount { get; set; }

        [XmlElement(ElementName = "vatExemptionCategory")]
        public VatExemptionCategory? VatExemptionCategory { get; set; }

        [XmlIgnore]
        public bool VatExemptionCategorySpecified { get; set; }

        [XmlElement(ElementName = "dienergia")]
        public ActivityUndertakingDeclaration ActivityUndertakingDeclaration { get; set; }

        [XmlElement(ElementName = "discountOption")]
        public bool DiscountOption { get; set; }

        [XmlIgnore]
        public bool DiscountOptionSpecified { get; set; }

        [XmlElement(ElementName = "withheldAmount")]
        public decimal WithheldTaxAmount { get; set; }

        [XmlIgnore]
        public bool WithheldTaxAmountSpecified { get; set; }

        [XmlElement(ElementName = "withheldPercentCategory")]
        public WithheldTaxCategory WithheldTaxCategory { get; set; }

        [XmlIgnore]
        public bool WithheldTaxCategorySpecified { get; set; }

        [XmlElement(ElementName = "stampDutyAmount")]
        public decimal StampDutyAmount { get; set; }

        [XmlIgnore]
        public bool StampDutyAmountSpecified { get; set; }

        [XmlElement(ElementName = "stampDutyPercentCategory")]
        public StampDutyCategory StampDutyCategory { get; set; }

        [XmlIgnore]
        public bool StampDutyCategorySpecified { get; set; }

        [XmlElement(ElementName = "feesAmount")]
        public decimal FeesAmount { get; set; }

        [XmlIgnore]
        public bool FeesAmountSpecified { get; set; }

        [XmlElement(ElementName = "feesPercentCategory")]
        public FeeCategory FeesCategory { get; set; }

        [XmlIgnore]
        public bool FeesCategorySpecified { get; set; }

        [XmlElement(ElementName = "otherTaxesPercentCategory")]
        public OtherTaxCategory OtherTaxesCategory { get; set; }

        [XmlIgnore]
        public bool OtherTaxesCategorySpecified { get; set; }

        [XmlElement(ElementName = "otherTaxesAmount")]
        public decimal OtherTaxesAmount { get; set; }

        [XmlIgnore]
        public bool OtherTaxesAmountSpecified { get; set; }

        [XmlElement(ElementName = "deductionsAmount")]
        public decimal DeductionsAmount { get; set; }

        [XmlIgnore]
        public bool DeductionsAmountSpecified { get; set; }

        [XmlElement(ElementName = "lineComments")]
        public string LineComments { get; set; }

        [XmlElement(ElementName = "incomeClassification")]
        public IncomeClassification[] IncomeClassification { get; set; }

        [XmlElement(ElementName = "expensesClassification")]
        public ExpenseClassification[] ExpenseClassification { get; set; }
    }
}
