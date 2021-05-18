using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum OtherTaxCategory
    {
        [XmlEnum("1")]
        FireInsurancePremiumsA1 = 1,
        [XmlEnum("2")]
        FireInsurancePremiumsA2 = 2,
        [XmlEnum("3")]
        LifeInsurancePremiumsB = 3,
        [XmlEnum("4")]
        OtherInsurancePremiumsC = 4,
        [XmlEnum("5")]
        ZeroTaxExemptInsurancePremiumsD = 5,
        [XmlEnum("6")]
        Hotels1Or2Stars = 6,
        [XmlEnum("7")]
        Hotels3Stars = 7,
        [XmlEnum("8")]
        Hotels4Stars = 8,
        [XmlEnum("9")]
        Hotels5Stars = 9,
        [XmlEnum("10")]
        RoomsOrApartments = 10,
        [XmlEnum("11")]
        TvBroadcastCommercials = 11,
        [XmlEnum("12")]
        LuxuryTaxIntraCommunityOrImport = 12,
        [XmlEnum("13")]
        LuxuryTaxDomesticGoods = 13,
        [XmlEnum("14")]
        CasinosAdmissionTicketPrice = 14
    }
}
