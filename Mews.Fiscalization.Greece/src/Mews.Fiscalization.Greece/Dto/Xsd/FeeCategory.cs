using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum FeeCategory
    {
        [XmlEnum("1")]
        MonthlyBillsLess50 = 1,
        [XmlEnum("2")]
        MonthlyBillsBetween50And100 = 2,
        [XmlEnum("3")]
        MonthlyBillsBetween100And150 = 3,
        [XmlEnum("4")]
        MonthlyBillsGreater150 = 4,
        [XmlEnum("5")]
        PrepaidTelephonyFee = 5,
        [XmlEnum("6")]
        SubscriptionTelevisionFee = 6,
        [XmlEnum("7")]
        LandlineSubscriberFee = 7,
        [XmlEnum("8")]
        EcotaxAndPlasticBagTax = 8,
        [XmlEnum("9")]
        OliveFruitFlyControlContribution = 9
    }
}
