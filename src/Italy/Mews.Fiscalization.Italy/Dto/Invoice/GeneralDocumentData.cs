using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class GeneralDocumentData
    {
        private decimal? _totalAmount;
        private decimal _rounding;
        private string _documentNumber;

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("TipoDocumento", Form = XmlSchemaForm.Unqualified)]
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("Divisa", Form = XmlSchemaForm.Unqualified)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("Data", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Required. Needs to be unique within (Seller[VatNumber], DocumentType, IssueData[year])
        /// </summary>
        [XmlElement("Numero", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { _documentNumber = value.NormalizeString(extendedAscii: false); }
        }

        /// <summary>
        /// Required if the seller/provider is subject to withholding tax representing either prepaid tax or the definitive taxation.
        /// </summary>
        [XmlElement("DatiRitenuta", Form = XmlSchemaForm.Unqualified)]
        public WithholdingData WithholdingData { get; set; }

        /// <summary>
        /// Required if stamp duty must be paid according to the type of document/transaction.
        /// </summary>
        [XmlElement("DatiBollo", Form = XmlSchemaForm.Unqualified)]
        public StampDutyData StampDutyData { get; set; }

        /// <summary>
        /// Required if the seller/provider is a subject held to pay pension contributions to his/her own professional fund or to INPS (or to both).
        /// </summary>
        [XmlElement("DatiCassaPrevidenziale", Form = XmlSchemaForm.Unqualified)]
        public PensionFundData[] PensionFundData { get; set; }

        /// <summary>
        /// Optional: the intention is to record the fact that a discount or an extra charge is applied by the seller/provider on the total amount of the document.
        /// </summary>
        [XmlElement("ScontoMaggiorazione", Form = XmlSchemaForm.Unqualified)]
        public PriceAdjustment[] PriceAdjustments { get; set; }

        /// <summary>
        /// Recommended. Total amount including VAT.
        /// </summary>
        [XmlElement("ImportoTotaleDocumento", Form = XmlSchemaForm.Unqualified)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = DtoUtils.NormalizeDecimal(value);
                TotalAmountSpecified = value != null;
            }
        }

        [XmlIgnore]
        public bool TotalAmountSpecified { get; private set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [XmlElement("Arrotondamento", Form = XmlSchemaForm.Unqualified)]
        public decimal Rounding
        {
            get { return _rounding; }
            set { _rounding = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool RoundingSpecified { get; set; }

        /// <summary>
        /// Recommended.
        /// </summary>
        [XmlElement("Causale", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string[] Reason { get; set; }

        /// <summary>
        /// YES, but only if the document has been is sued according to methods  and terms  laid down  by ministerial  decree pursuant  to article 73 of Italian Presidential Decree 633/72; this enables the seller/provider to issue several documents in the same year with the same numb
        /// to indicate  whether  the check  must be  carried  out  on the unique numbers  of the  documents  in  the same  year  for  the same transferee/provider; if it is filled in, the check will be carried out.
        /// </summary>
        [XmlElement("Art73", Form = XmlSchemaForm.Unqualified)]
        public Art73Type Art73 { get; set; }

        [XmlIgnore]
        public bool Art73Specified { get; set; }
    }
}