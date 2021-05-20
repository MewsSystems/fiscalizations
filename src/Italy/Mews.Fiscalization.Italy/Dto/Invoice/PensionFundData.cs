using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class PensionFundData
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public TipoCassaType TipoCassa { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public decimal AlCassa { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public decimal ImportoContributoCassa { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public decimal ImponibileCassa { get; set; }

        [XmlIgnore]
        public bool ImponibileCassaSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public decimal AliquotaIVA { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public RitenutaType Ritenuta { get; set; }

        [XmlIgnore]
        public bool RitenutaSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public TaxKind Natura { get; set; }

        [XmlIgnore]
        public bool NaturaSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string RiferimentoAmministrazione { get; set; }
    }
}