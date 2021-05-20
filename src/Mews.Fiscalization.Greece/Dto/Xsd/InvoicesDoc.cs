using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    [XmlRoot(ElementName = "InvoicesDoc", Namespace = InvoicesDoc.Namespace)]
    public class InvoicesDoc
    {
        public const string Namespace = "http://www.aade.gr/myDATA/invoice/v1.0";

        [XmlElement(ElementName = "invoice")]
        public Invoice[] Invoices { get; set; }
    }
}
