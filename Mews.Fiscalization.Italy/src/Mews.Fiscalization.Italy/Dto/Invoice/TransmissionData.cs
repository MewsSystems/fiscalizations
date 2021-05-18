using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class TransmissionData
    {
        private string _sequentialNumber;
        private string _destinationCode;
        private string _destinationEmail;

        public TransmissionData()
        {
            TransmissionFormat = TransmissionFormat.FPA12;
        }

        [XmlElement("IdTrasmittente", Form = XmlSchemaForm.Unqualified)]
        public SenderId TransmitterId { get; set; }

        /// <summary>
        /// Sender identification.
        /// </summary>
        [XmlElement("ProgressivoInvio", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string SequentialNumber
        {
            get { return _sequentialNumber; }
            set { _sequentialNumber = value.NormalizeString(extendedAscii: false); }
        }

        [XmlElement("FormatoTrasmissione", Form = XmlSchemaForm.Unqualified)]
        public TransmissionFormat TransmissionFormat { get; set; }

        [XmlElement("CodiceDestinatario", Form = XmlSchemaForm.Unqualified)]
        public string DestinationCode
        {
            get { return _destinationCode; }
            set { _destinationCode = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("PECDestinatario", Form = XmlSchemaForm.Unqualified)]
        public string DestinationEmail
        {
            get { return _destinationEmail; }
            set { _destinationEmail = value.NonEmptyValueOrNull(); }
        }

        [XmlElement("ContattiTrasmittente", Form = XmlSchemaForm.Unqualified)]
        public TransmitterContact TransmitterContact { get; set; }
    }
}