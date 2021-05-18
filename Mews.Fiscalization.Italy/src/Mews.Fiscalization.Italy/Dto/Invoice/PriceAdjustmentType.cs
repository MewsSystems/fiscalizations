using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
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
}