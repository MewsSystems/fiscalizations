using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum StampDutyPaid
    {
        /// <summary>
        /// Yes.
        /// </summary>
        SI,
    }
}