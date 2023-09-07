using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Invoice;

[Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
public enum StampDutyPaid
{
    /// <summary>
    /// Yes.
    /// </summary>
    SI,
}