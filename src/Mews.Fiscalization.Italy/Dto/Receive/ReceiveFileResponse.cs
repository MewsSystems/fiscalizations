using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Receive
{
    [Serializable]
    [XmlRoot("rispostaSdIRiceviFile", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class ReceiveFileResponse
    {
        [XmlElement("IdentificativoSdI", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer", Order = 0)]
        public string SdiIdentification { get; set; }

        [XmlElement("DataOraRicezione", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public DateTime ReceivedOn { get; set; }

        [XmlElement("Errore", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public ReceiveFileError Error { get; set; }

        [XmlIgnore]
        public bool ErrorSpecified { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public enum ReceiveFileError
    {
        [XmlEnum("EI01")]
        EmptyFile,
        [XmlEnum("EI02")]
        ServiceUnavailable,
        [XmlEnum("EI03")]
        UnauthorizedUser
    }
}
