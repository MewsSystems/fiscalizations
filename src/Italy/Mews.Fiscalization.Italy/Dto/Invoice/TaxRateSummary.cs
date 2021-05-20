using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class TaxRateSummary
    {
        private decimal _vatRate;
        private decimal _complementaryExpenses;
        private decimal _rounding;
        private decimal _taxableAmount;
        private decimal _taxAmount;
        private string _normativeReference;
        private TaxKind? _kind;
        private VatDueDate? _vatDueDate;

        /// <summary>
        /// Required. Percentage.
        /// </summary>
        [XmlElement("AliquotaIVA", Form = XmlSchemaForm.Unqualified)]
        public decimal VatRate
        {
            get { return _vatRate; }
            set { _vatRate = DtoUtils.NormalizeDecimal(value); }
        }

        /// <summary>
        /// Required if at least one of the invoice lines has Kind filled in. In those cases, this needs to match.
        /// </summary>
        [XmlElement("Natura", Form = XmlSchemaForm.Unqualified)]
        public TaxKind? Kind
        {
            get { return _kind; }
            set
            {
                _kind = value;
                KindSpecified = value != null;
            }
        }

        [XmlIgnore]
        public bool KindSpecified { get; private set; }

        [XmlElement("SpeseAccessorie", Form = XmlSchemaForm.Unqualified)]
        public decimal ComplementaryExpenses
        {
            get { return _complementaryExpenses; }
            set { _complementaryExpenses = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool ComplementaryExpensesSpecified { get; set; }

        [XmlElement("Arrotondamento", Form = XmlSchemaForm.Unqualified)]
        public decimal Rounding
        {
            get { return _rounding; }
            set { _rounding = DtoUtils.NormalizeDecimal(value, precision: 8); }
        }

        [XmlIgnore]
        public bool RoundingSpecified { get; set; }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("ImponibileImporto", Form = XmlSchemaForm.Unqualified)]
        public decimal TaxableAmount
        {
            get { return _taxableAmount; }
            set { _taxableAmount = DtoUtils.NormalizeDecimal(value); }
        }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("Imposta", Form = XmlSchemaForm.Unqualified)]
        public decimal TaxAmount
        {
            get { return _taxAmount; }
            set { _taxAmount = DtoUtils.NormalizeDecimal(value); }
        }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("EsigibilitaIVA", Form = XmlSchemaForm.Unqualified)]
        public VatDueDate? VatDueDate
        {
            get { return _vatDueDate; }
            set
            {
                _vatDueDate = value;
                VatDueDateSpecified = value != null;
            }
        }

        [XmlIgnore]
        public bool VatDueDateSpecified { get; private set; }

        /// <summary>
        /// Required if Kind is filled in and therefore in the case of transactions which are exempt from  VAT or in the case of a reversed charge.
        /// </summary>
        [XmlElement("RiferimentoNormativo", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string NormativeReference
        {
            get { return _normativeReference; }
            set { _normativeReference = value.NormalizeString(); }
        }
    }
}