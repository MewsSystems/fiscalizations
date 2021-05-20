using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class WorkProgressReportData
    {
        [XmlElement("RiferimentoFase", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PhaseReference { get; set; }
    }
}