using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class OtherInvoiceLineData
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string TipoDato { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string RiferimentoTesto { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public int RiferimentoNumero { get; set; }

        [XmlIgnore]
        public bool RiferimentoNumeroSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime RiferimentoData { get; set; }

        [XmlIgnore]
        public bool RiferimentoDataSpecified { get; set; }
    }
}