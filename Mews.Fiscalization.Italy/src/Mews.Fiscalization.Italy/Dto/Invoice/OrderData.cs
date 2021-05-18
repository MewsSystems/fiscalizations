using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class OrderData
    {
        /// <summary>
        /// Required if the purchase order refers to a specific invoice line.
        /// </summary>
        [XmlElement("RiferimentoNumeroLinea", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string[] LineNumber { get; set; }

        /// <summary>
        /// Required.
        /// </summary>
        [XmlElement("IdDocumento", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Recommended.
        /// </summary>
        [XmlElement("Data", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime OrderDate { get; set; }

        [XmlIgnore]
        public bool OrderDateSpecified { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [XmlElement("NumItem", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string ItemNumber { get; set; }

        /// <summary>
        /// Optional. Indicates the job or agreement to which the purchase order refers.
        /// </summary>
        [XmlElement("CodiceCommessaConvenzione", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string JobAgreementCode { get; set; }

        /// <summary>
        /// Required if if the work falls within the scope of art. 25 of Italian Decree Law 66/2014, converted into Law no. 89 of 23 June 2014.
        /// </summary>
        [XmlElement("CodiceCUP", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string ProjectCode { get; set; }

        /// <summary>
        /// Required if if the work falls within the scope of art. 25 of Italian Decree Law 66/2014, converted into Law no. 89 of 23 June 2014.
        /// </summary>
        [XmlElement("CodiceCIG", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string TenderCode { get; set; }
    }
}