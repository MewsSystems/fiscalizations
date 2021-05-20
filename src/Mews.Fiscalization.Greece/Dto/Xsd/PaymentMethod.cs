using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class PaymentMethod
    {
        [XmlElement(ElementName = "type", IsNullable = false)]
        public PaymentMethodType PaymentMethodType { get; set; }

        [XmlElement(ElementName = "amount", IsNullable = false)]
        public decimal Amount { get; set; }

        [XmlElement(ElementName = "paymentMethodInfo")]
        public string Info { get; set; }
    }
}
