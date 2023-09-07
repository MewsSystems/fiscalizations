using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Invoice;

[Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
public enum PriceAdjustmentType
{
    /// <summary>
    /// Discount.
    /// </summary>
    SC,
    /// <summary>
    /// Extra charge.
    /// </summary>
    MG,
}