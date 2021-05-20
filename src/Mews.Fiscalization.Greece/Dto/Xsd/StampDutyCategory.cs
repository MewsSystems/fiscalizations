using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum StampDutyCategory
    {
        [XmlEnum("1")]
        Rate1_2 = 1,
        [XmlEnum("2")]
        Rate2_4 = 2,
        [XmlEnum("3")]
        Rate3_6 = 3
    }
}
