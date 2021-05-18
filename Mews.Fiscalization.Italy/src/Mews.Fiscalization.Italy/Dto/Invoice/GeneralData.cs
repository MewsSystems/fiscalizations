using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class GeneralData
    {
        [XmlElement("DatiGeneraliDocumento", Form = XmlSchemaForm.Unqualified)]
        public GeneralDocumentData GeneralDocumentData { get; set; }

        [XmlElement("DatiOrdineAcquisto", Form = XmlSchemaForm.Unqualified)]
        public OrderData[] OrderData { get; set; }

        /// <summary>
        /// Optional: the intention is to record the fact that the document(invoice or similar) is issued against a supply/service disciplined by a contract.
        /// </summary>
        [XmlElement("DatiContratto", Form = XmlSchemaForm.Unqualified)]
        public OrderData[] ContractData { get; set; }

        /// <summary>
        /// Optional: the intention is to record the fact that the document(invoice or similar) is issued against a supply/service disciplined by a agreement.
        /// </summary>
        [XmlElement("DatiConvenzione", Form = XmlSchemaForm.Unqualified)]
        public OrderData[] AgreementData { get; set; }

        /// <summary>
        /// Optional: he intention is to record the fact that the document(invoice or similar) is issued against a receipt procedure, identified and registered in internal IT systems.
        /// </summary>
        [XmlElement("DatiRicezione", Form = XmlSchemaForm.Unqualified)]
        public OrderData[] ReceptionData { get; set; }

        /// <summary>
        /// Optional: the intention is to record the fact that the document(invoice or similar) is connected t o a previously issued invoice(this is the case, for example, for credit notes or invoices for a balance connected to preceding invoices for down payments).
        /// </summary>
        [XmlElement("DatiFattureCollegate", Form = XmlSchemaForm.Unqualified)]
        public OrderData[] ConnectedInvoicesData { get; set; }

        /// <summary>
        /// Required if the document can be classified as invoicing on the basis of works progress reports(SAL) with defined phases.
       /// </summary>
        [XmlElement("DatiSAL", Form = XmlSchemaForm.Unqualified)]
        public WorkProgressReportData[] WorkProgressReportData { get; set; }

        /// <summary>
        /// Required if there is a transport document (or, in the cases in which it is still contemplated, a packing list) which certifies the transfer of the goods from the seller to the buyer and which must be indicated on the deferred invoice.
        /// </summary>
        [XmlElement("DatiDDT", Form = XmlSchemaForm.Unqualified)]
        public TransportDocumentData[] TransportDocumentData { get; set; }

        [XmlElement("DatiTrasporto", Form = XmlSchemaForm.Unqualified)]
        public TransportData TransportData { get; set; }

        /// <summary>
        /// Required if there is an invoice which summarises the complementary transactions of each quarter of the calendar year carried out by the carrier on behalf of the customer pursuant to art. 74, section 4, Italian Pres.Decree 633/72 .
        /// </summary>
        [XmlElement("FatturaPrincipale", Form = XmlSchemaForm.Unqualified)]
        public MainInvoice MainInvoice { get; set; }
    }
}