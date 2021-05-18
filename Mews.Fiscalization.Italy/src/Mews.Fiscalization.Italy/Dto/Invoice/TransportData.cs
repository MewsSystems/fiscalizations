using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class TransportData
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public DatiAnagraficiVettoreType DatiAnagraficiVettore { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string MezzoTrasporto { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string CausaleTrasporto { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string NumeroColli { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Descrizione { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string UnitaMisuraPeso { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public decimal PesoLordo { get; set; }

        [XmlIgnore]
        public bool PesoLordoSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public decimal PesoNetto { get; set; }

        [XmlIgnore]
        public bool PesoNettoSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public DateTime DataOraRitiro { get; set; }

        [XmlIgnore]
        public bool DataOraRitiroSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime DataInizioTrasporto { get; set; }

        [XmlIgnore]
        public bool DataInizioTrasportoSpecified { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string TipoResa { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public Address IndirizzoResa { get; set; }

        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public DateTime DataOraConsegna { get; set; }

        [XmlIgnore]
        public bool DataOraConsegnaSpecified { get; set; }
    }
}