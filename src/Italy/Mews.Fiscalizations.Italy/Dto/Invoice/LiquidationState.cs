using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum LiquidationState
    {
        [XmlEnum("LS")]
        InLiquidation,
        [XmlEnum("LN")]
        NotInLiquidation
    }
}