using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class IdentificationData
    {
        [XmlElement("IdFiscaleIVA", Form = XmlSchemaForm.Unqualified)]
        public SenderId VatTaxId { get; set; }

        /// <summary>
        /// Seller's / provider's tax code composed of 11 numbers for companies or 16 numbers for natural persons.
        /// </summary>
        [XmlElement("CodiceFiscale", Form = XmlSchemaForm.Unqualified)]
        public string TaxCode { get; set; }

        [XmlElement("Anagrafica", Form = XmlSchemaForm.Unqualified)]
        public Identity Identity { get; set; }

        /// <summary>
        /// Optional: Allows for entering information on any professional roll or association to which the seller/provider belongs.
        /// </summary>
        [XmlElement("AlboProfessionale", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string ProfessionalAssociation { get; set; }

        /// <summary>
        /// Optional: Allows for entering information on any professional roll or association to which the seller/provider belongs, specifically the province of the same.
        /// </summary>
        [XmlElement("ProvinciaAlbo", Form = XmlSchemaForm.Unqualified)]
        public string AssociationProvince { get; set; }

        /// <summary>
        /// Optional: Allows for entering information on any professional roll or association to which the seller/provider belongs, specifically his/her registration number.
        /// </summary>
        [XmlElement("NumeroIscrizioneAlbo", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string AssociationNumber { get; set; }

        /// <summary>
        /// Optional: Allows for entering information on any professional roll or association to which the seller/provider belongs, specifically the date of his/her registration. 
        /// </summary>
        [XmlElement("DataIscrizioneAlbo", Form = XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime AssociationRegistrationDate { get; set; }

        [XmlIgnore]
        public bool AssociationRegistrationDateSpecified { get; set; }

        /// <summary>
        /// Must contain one of the codes given in the associated list; the code identifies, according to the business sector or the income situation, the tax system under which the seller/provider operates.
        /// </summary>
        [XmlElement("RegimeFiscale", Form = XmlSchemaForm.Unqualified)]
        public FiscalRegime FiscalRegime { get; set; }
    }
}