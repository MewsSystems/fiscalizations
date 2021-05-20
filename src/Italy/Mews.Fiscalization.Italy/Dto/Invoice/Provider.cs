using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class Provider
    {
        [XmlElement("DatiAnagrafici", Form = XmlSchemaForm.Unqualified)]
        public IdentificationData IdentificationData { get; set; }

        [XmlElement("Sede", Form = XmlSchemaForm.Unqualified)]
        public Address OfficeAddress { get; set; }

        /// <summary>
        /// Optional for seller/provider without permanent residence in italy.
        /// </summary>
        [XmlElement("StabileOrganizzazione", Form = XmlSchemaForm.Unqualified)]
        public Address PermanentAddress { get; set; }

        /// <summary>
        /// Required if the seller/provider is a company listed on the register of companies and as such must also indicate the registration data on all documents.
        /// </summary>
        [XmlElement("IscrizioneREA", Form = XmlSchemaForm.Unqualified)]
        public EconomicIndexRegistration EconomicIndexRegistration { get; set; }

        [XmlElement("Contatti", Form = XmlSchemaForm.Unqualified)]
        public Contact Contacts { get; set; }

        /// <summary>
        /// Optional.
        /// </summary>
        [XmlElement("RiferimentoAmministrazione", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string AdministrationReference { get; set; }
    }
}