using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum PaymentMethodType
    {
        [XmlEnum("1")]
        DomesticPaymentsAccountNumber = 1,
        [XmlEnum("2")]
        ForeignMethodsAccountNumber = 2,
        [XmlEnum("3")]
        Cash = 3,
        [XmlEnum("4")]
        Check = 4,
        [XmlEnum("5")]
        OnCredit = 5
    }
}
