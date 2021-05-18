using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using Mews.Fiscalization.Italy.Dto.XmlSignature;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace), XmlRoot("FatturaElettronica", Namespace = ElectronicInvoice.Namespace, IsNullable = false)]
    public class ElectronicInvoice
    {
        public const string Namespace = "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2"; 

        public ElectronicInvoice()
        {
            Version = VersioneSchemaType.FPA12;
        }

        [XmlElement("FatturaElettronicaHeader", Form = XmlSchemaForm.Unqualified)]
        public ElectronicInvoiceHeader Header { get; set; }

        [XmlElement("FatturaElettronicaBody", Form = XmlSchemaForm.Unqualified)]
        public ElectronicInvoiceBody[] Body { get; set; }

        [XmlElement("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        [XmlAttribute("versione")]
        public VersioneSchemaType Version { get; set; }
    }
}
