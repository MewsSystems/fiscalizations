using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum VatExemptionCategory
    {
        [XmlEnum("1")]
        WithoutVatArticle3 = 1,
        [XmlEnum("2")]
        WithoutVatArticle5 = 2,
        [XmlEnum("3")]
        WithoutVatArticle13 = 3,
        [XmlEnum("4")]
        WithoutVatArticle14 = 4,
        [XmlEnum("5")]
        WithoutVatArticle16 = 5,
        [XmlEnum("6")]
        WithoutVatArticle19 = 6,
        [XmlEnum("7")]
        WithoutVatArticle22 = 7,
        [XmlEnum("8")]
        WithoutVatArticle24 = 8,
        [XmlEnum("9")]
        WithoutVatArticle25 = 9,
        [XmlEnum("10")]
        WithoutVatArticle26 = 10,
        [XmlEnum("11")]
        WithoutVatArticle27 = 11,
        [XmlEnum("12")]
        WithoutVatArticle27SeagoingVessels = 12,
        [XmlEnum("13")]
        WithoutVatArticle271CSeagoingVessels = 13,
        [XmlEnum("14")]
        WithoutVatArticle28 = 14,
        [XmlEnum("15")]
        WithoutVatArticle39 = 15,
        [XmlEnum("16")]
        WithoutVatArticle39A = 16,
        [XmlEnum("17")]
        WithoutVatArticle40 = 17,
        [XmlEnum("18")]
        WithoutVatArticle41 = 18,
        [XmlEnum("19")]
        WithoutVatArticle47 = 19,
        [XmlEnum("20")]
        VatIncludedArticle43 = 20,
        [XmlEnum("21")]
        VatIncludedArticle44 = 21,
        [XmlEnum("22")]
        VatIncludedArticle45 = 22,
        [XmlEnum("23")]
        VatIncludedArticle46 = 23
    }
}
