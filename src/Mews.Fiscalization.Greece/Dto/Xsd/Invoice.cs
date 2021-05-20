using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class Invoice
    {
        [XmlElement(ElementName = "uid")]
        public string InvoiceId { get; set; }

        [XmlElement(ElementName = "mark")]
        public long InvoiceMark { get; set; }

        [XmlIgnore]
        public bool InvoiceMarkSpecified { get; set; }

        [XmlElement(ElementName = "cancelledByMark")]
        public long InvoiceCancelationMark { get; set; }

        [XmlIgnore]
        public bool InvoiceCancelationMarkSpecified { get; set; }

        [XmlElement(ElementName = "authenticationCode")]
        public string AuthenticationCode { get; set; }

        [XmlElement(ElementName = "transmissionFailure")]
        public TransmissionFailure TransmissionFailure { get; set; }

        [XmlIgnore]
        public bool TransmissionFailureSpecified { get; set; }

        [XmlElement(ElementName = "issuer")]
        public InvoiceParty InvoiceIssuer { get; set; }

        [XmlElement(ElementName = "counterpart")]
        public InvoiceParty InvoiceCounterpart { get; set; }

        [XmlElement(ElementName = "invoiceHeader", IsNullable = false)]
        public InvoiceHeader InvoiceHeader { get; set; }

        [XmlArray(ElementName = "paymentMethods")]
        [XmlArrayItem(ElementName = "paymentMethodDetails")]
        public PaymentMethod[] PaymentMethods { get; set; }

        [XmlElement(ElementName = "invoiceDetails", IsNullable = false)]
        public InvoiceDetail[] InvoiceDetails { get; set; }

        [XmlArray(ElementName = "taxesTotals")]
        [XmlArrayItem(ElementName = "taxes")]
        public Tax[] Taxes { get; set; }

        [XmlElement(ElementName = "invoiceSummary", IsNullable = false)]
        public InvoiceSummary InvoiceSummary { get; set; }
    }
}
