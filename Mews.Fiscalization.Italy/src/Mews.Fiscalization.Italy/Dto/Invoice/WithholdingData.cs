using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class WithholdingData
    {
        private decimal _withholdingAmount;
        private decimal _withHoldingRate;

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("TipoRitenuta", Form = XmlSchemaForm.Unqualified)]
        public WithholdingType WithholdingType { get; set; }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("ImportoRitenuta", Form = XmlSchemaForm.Unqualified)]
        public decimal WithholdingAmount
        {
            get { return _withholdingAmount; }
            set { _withholdingAmount = DtoUtils.NormalizeDecimal(value); }
        }

        /// <summary>
        /// Required. Contains percentage withheld. (20 % is represented as 20.0)
        /// </summary>
        [XmlElement("AliquotaRitenuta", Form = XmlSchemaForm.Unqualified)]
        public decimal WithHoldingRate
        {
            get { return _withHoldingRate; }
            set { _withHoldingRate = DtoUtils.NormalizeDecimal(value); }
        }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("CausalePagamento", Form = XmlSchemaForm.Unqualified)]
        public PaymentReason PaymentReason { get; set; }
    }
}