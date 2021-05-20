using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public class Response
    {
        [XmlElement(ElementName = "index")]
        public int Index { get; set; }

        [XmlIgnore]
        public bool IndexSpecified { get; set; }

        [XmlElement(ElementName = "statusCode")]
        public StatusCode StatusCode { get; set; }

        [XmlElement(ElementName = "invoiceUid")]
        public string InvoiceUid { get; set; }

        [XmlElement(ElementName = "invoiceMark")]
        public long InvoiceMark { get; set; }

        [XmlIgnore]
        public bool InvoiceMarkSpecified { get; set; }

        [XmlElement(ElementName = "classificationMark")]
        public long ClassificationMark { get; set; }

        [XmlIgnore]
        public bool ClassificationMarkSpecified { get; set; }

        [XmlElement(ElementName = "cancellationMark")]
        public long CancellationMark { get; set; }

        [XmlIgnore]
        public bool CancellationMarkSpecified { get; set; }

        [XmlElement(ElementName = "authenticationCode")]
        public string AuthenticationCode { get; set; }

        [XmlArray(ElementName = "errors")]
        [XmlArrayItem(ElementName = "error")]
        public Error[] Errors { get; set; }
    }
}