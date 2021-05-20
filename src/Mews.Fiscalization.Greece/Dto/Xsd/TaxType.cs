using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum TaxType
    {
        [XmlEnum("1")]
        WithheldTaxes = 1,
        [XmlEnum("2")]
        Fees = 2,
        [XmlEnum("3")]
        OtherTaxes = 3,
        [XmlEnum("4")]
        StampDuty = 4,
        [XmlEnum("5")]
        Deductions = 5
    }
}
