using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class StampDutyData
    {
        public StampDutyData()
        {
            StampDutyPaid = StampDutyPaid.SI;
        }

        [XmlElement("BolloVirtuale", Form = XmlSchemaForm.Unqualified)]
        public StampDutyPaid StampDutyPaid { get; set; }

        [XmlElement("ImportoBollo", Form = XmlSchemaForm.Unqualified)]
        public decimal AmountPaid { get; set; }
    }
}