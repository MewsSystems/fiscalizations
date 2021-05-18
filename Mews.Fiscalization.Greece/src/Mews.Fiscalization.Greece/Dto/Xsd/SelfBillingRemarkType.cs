using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum SelfBillingRemarkType
    {
        [XmlEnum("1")]
        ThirdPartySalesClearance = 1,
        [XmlEnum("2")]
        FeeFromThirdPartySales = 2
    }
}
