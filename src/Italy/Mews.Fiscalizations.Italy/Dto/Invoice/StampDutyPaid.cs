using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
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