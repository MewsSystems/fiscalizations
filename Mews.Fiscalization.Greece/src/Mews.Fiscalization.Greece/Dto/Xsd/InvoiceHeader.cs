using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class InvoiceHeader
    {
        [XmlElement(ElementName = "series", IsNullable = false)]
        public string Series { get; set; }

        [XmlElement(ElementName = "aa", IsNullable = false)]
        public string SerialNumber { get; set; }

        [XmlElement(ElementName = "issueDate", DataType = "date", IsNullable = false)]
        public DateTime IssueDate { get; set; }

        [XmlElement(ElementName = "invoiceType", IsNullable = false)]
        public InvoiceType InvoiceType { get; set; }

        [XmlElement(ElementName = "vatPaymentSuspension")]
        public bool VatPaymentSuspension { get; set; }

        [XmlIgnore]
        public bool VatPaymentSuspensionSpecified { get; set; }

        [XmlElement(ElementName = "currency")]
        public Currency? Currency { get; set; }

        [XmlIgnore]
        public bool CurrencySpecified { get; set; }

        [XmlElement(ElementName = "exchangeRate")]
        public decimal ExchangeRate { get; set; }

        [XmlIgnore]
        public bool ExchangeRateSpecified { get; set; }

        [XmlElement(ElementName = "correlatedInvoices")]
        public long CorrelatedInvoices { get; set; }

        [XmlIgnore]
        public bool CorrelatedInvoicesSpecified { get; set; }

        [XmlElement(ElementName = "selfPricing")]
        public bool SelfPricing { get; set; }

        [XmlIgnore]
        public bool SelfPricingSpecified { get; set; }

        [XmlElement(ElementName = "dispatchDate", DataType = "date")]
        public DateTime DispatchDate { get; set; }

        [XmlIgnore]
        public bool DispatchDateSpecified { get; set; }

        [XmlElement(ElementName = "dispatchTime", DataType = "time")]
        public DateTime DispatchTime { get; set; }

        [XmlIgnore]
        public bool DispatchTimeSpecified { get; set; }

        [XmlElement(ElementName = "vehicleNumber")]
        public string VehicleNumber { get; set; }

        [XmlElement(ElementName = "movePurpose")]
        public MovePurpose MovePurpose { get; set; }

        [XmlIgnore]
        public bool MovePurposeSpecified { get; set; }
    }
}
