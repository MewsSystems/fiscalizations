using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class PriceAdjustment
    {
        private decimal _percentage;
        private decimal _amount;

        [XmlElement("Tipo", Form = XmlSchemaForm.Unqualified)]
        public PriceAdjustmentType Type { get; set; }

        [XmlElement("Percentuale", Form = XmlSchemaForm.Unqualified)]
        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool PercentageSpecified { get; set; }

        [XmlElement("Importo", Form = XmlSchemaForm.Unqualified)]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool AmountSpecified { get; set; }
    }
}