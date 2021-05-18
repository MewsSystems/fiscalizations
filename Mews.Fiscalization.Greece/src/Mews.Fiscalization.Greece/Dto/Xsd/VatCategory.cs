using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum VatCategory
    {
        [XmlEnum("1")]
        Vat24 = 1,
        [XmlEnum("2")]
        Vat13 = 2,
        [XmlEnum("3")]
        Vat6 = 3,
        [XmlEnum("4")]
        Vat17 = 4,
        [XmlEnum("5")]
        Vat9 = 5,
        [XmlEnum("6")]
        Vat4 = 6,
        [XmlEnum("7")]
        Vat0 = 7,
        [XmlEnum("8")]
        WithoutVat = 8
    }
}
