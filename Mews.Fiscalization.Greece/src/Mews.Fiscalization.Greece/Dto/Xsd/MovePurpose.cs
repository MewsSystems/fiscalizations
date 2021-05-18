using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum MovePurpose
    {
        [XmlEnum("1")]
        Sale = 1,
        [XmlEnum("2")]
        SalesOnBehalfOfThirdParties = 2,
        [XmlEnum("3")]
        Sampling = 3,
        [XmlEnum("4")]
        Exhibition = 4,
        [XmlEnum("5")]
        Return = 5,
        [XmlEnum("6")]
        Keeping = 6,
        [XmlEnum("7")]
        EditAssembly = 7,
        [XmlEnum("8")]
        BetweenEntityBranches = 8
    }
}
