using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class EconomicIndexRegistration
    {
        private decimal _shareCapital;

        [XmlElement("Ufficio", Form = XmlSchemaForm.Unqualified)]
        public string OfficeId { get; set; }

        [XmlElement("NumeroREA", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string IndexNumber { get; set; }

        [XmlElement("CapitaleSociale", Form = XmlSchemaForm.Unqualified)]
        public decimal ShareCapital
        {
            get { return _shareCapital; }
            set { _shareCapital = DtoUtils.NormalizeDecimal(value); }
        }

        [XmlIgnore]
        public bool ShareCapitalSpecified { get; set; }

        [XmlElement("SocioUnico", Form = XmlSchemaForm.Unqualified)]
        public ShareholderDistribution ShareholderDistribution { get; set; }

        [XmlIgnore]
        public bool ShareholderDistributionSpecified { get; set; }

        [XmlElement("StatoLiquidazione", Form = XmlSchemaForm.Unqualified)]
        public LiquidationState LiquidationState { get; set; }
    }
}