using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class ElectronicInvoiceHeader
    {
        [XmlElement("DatiTrasmissione", Form = XmlSchemaForm.Unqualified)]
        public TransmissionData TransmissionData { get; set; }

        [XmlElement("CedentePrestatore", Form = XmlSchemaForm.Unqualified)]
        public Provider Provider { get; set; }

        [XmlElement("RappresentanteFiscale", Form = XmlSchemaForm.Unqualified)]
        public TaxRepresentative TaxRepresentative { get; set; }

        [XmlElement("CessionarioCommittente", Form = XmlSchemaForm.Unqualified)]
        public Buyer Buyer { get; set; }

        /// <summary>
        /// Required if invoices are being issued by a third party intermediary.
        /// </summary>
        [XmlElement("TerzoIntermediarioOSoggettoEmittente", Form = XmlSchemaForm.Unqualified)]
        public Intermediary Intermediary { get; set; }

        /// <summary>
        /// Required if the invoice is issued by a subject other than seller/provider;
        /// </summary>
        [XmlElement("SoggettoEmittente", Form = XmlSchemaForm.Unqualified)]
        public IssuerType Issuer { get; set; }

        [XmlIgnore]
        public bool IssuerSpecified { get; set; }
    }
}