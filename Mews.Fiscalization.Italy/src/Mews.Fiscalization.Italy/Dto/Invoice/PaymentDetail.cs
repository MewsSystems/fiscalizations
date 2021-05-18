using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class PaymentDetail
    {
        private decimal _paymentAmount;
        private decimal _advancePaymentDiscount;
        private decimal _advancePaymentPenalty;
        private string _payerFirstName;
        private string _payerLastName;
        private string _payerTaxCode;

        /// <summary>
        /// Optional. Only if it's different from seller/provider.
        /// </summary>
        [XmlElement("Beneficiario", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Beneficiary { get; set; }

        [XmlElement("ModalitaPagamento", Form = XmlSchemaForm.Unqualified)]
        public PaymentMethod PaymentMethod { get; set; }

        [XmlElement("DataRiferimentoTerminiPagamento", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime PaymentTermsReferenceDate { get; set; }

        [XmlIgnore]
        public bool PaymentTermsReferenceDateSpecified { get; set; }

        [XmlElement("GiorniTerminiPagamento", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PaymentTermDays { get; set; }

        [XmlElement("DataScadenzaPagamento", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime PaymentTermExpiryDate { get; set; }

        [XmlIgnore]
        public bool PaymentTermExpiryDateSpecified { get; set; }

        [XmlElement("ImportoPagamento", Form = XmlSchemaForm.Unqualified)]
        public decimal PaymentAmount
        {
            get { return _paymentAmount; }
            set { _paymentAmount = DtoUtils.NormalizeDecimal(value); }
        }

        /// <summary>
        /// The  code  of  the  post office to  which the  payment must be sent if this is necessary for the payment method.
        /// </summary>
        [XmlElement("CodUfficioPostale", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Zip { get; set; }

        [XmlElement("CognomeQuietanzante", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string PayerLastName
        {
            get { return _payerLastName; }
            set { _payerLastName = value.NormalizeString(); }
        }

        [XmlElement("NomeQuietanzante", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string PayerFirstName
        {
            get { return _payerFirstName; }
            set { _payerFirstName = value.NormalizeString(); }
        }

        [XmlElement("CFQuietanzante", Form = XmlSchemaForm.Unqualified)]
        public string PayerTaxCode
        {
            get { return _payerTaxCode; }
            set { _payerTaxCode = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("TitoloQuietanzante", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string PayerTitle { get; set; }

        /// <summary>
        /// If necessary for specified payment method.
        /// </summary>
        [XmlElement("IstitutoFinanziario", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string BankIdentification { get; set; }

        [XmlElement("IBAN", Form = XmlSchemaForm.Unqualified)]
        public string Iban { get; set; }

        [XmlElement("ABI", Form = XmlSchemaForm.Unqualified)]
        public string Abi { get; set; }

        [XmlElement("CAB", Form = XmlSchemaForm.Unqualified)]
        public string Cab { get; set; }

        [XmlElement("BIC", Form = XmlSchemaForm.Unqualified)]
        public string Bic { get; set; }

        [XmlElement("ScontoPagamentoAnticipato", Form = XmlSchemaForm.Unqualified)]
        public decimal AdvancePaymentDiscount
        {
            get { return _advancePaymentDiscount; }
            set { _advancePaymentDiscount = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool AdvancePaymentDiscountSpecified { get; set; }

        [XmlElement("DataLimitePagamentoAnticipato", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime AdvancePaymentDeadline { get; set; }

        [XmlIgnore]
        public bool AdvancePaymentDeadlineSpecified { get; set; }

        [XmlElement("PenalitaPagamentiRitardati", Form = XmlSchemaForm.Unqualified)]
        public decimal AdvancePaymentPenalty
        {
            get { return _advancePaymentPenalty; }
            set { _advancePaymentPenalty = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool AdvancePaymentPenaltySpecified { get; set; }

        [XmlElement("DataDecorrenzaPenale", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime PenaltyStartingDate { get; set; }

        [XmlIgnore]
        public bool PenaltyStartingDateSpecified { get; set; }

        [XmlElement("CodicePagamento", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string PaymentCode { get; set; }
    }
}