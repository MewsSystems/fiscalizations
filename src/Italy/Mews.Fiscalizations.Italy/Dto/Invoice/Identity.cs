using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Italy.Dto.Invoice;

[Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
public class Identity
{
    private string _firstName;
    private string _lastName;
    private string _companyName;

    [XmlElement("Nome", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value.ToBasicLatin(); }
    }

    [XmlElement("Cognome", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value.ToBasicLatin(); }
    }

    [XmlElement("Denominazione", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
    public string CompanyName
    {
        get { return _companyName; }
        set { _companyName = value.ToBasicLatin(); }
    }


    /// <summary>
    /// Optional.
    /// </summary>
    [XmlElement("Titolo", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
    public string Title { get; set; }

    /// <summary>
    /// Optional: Economic Operator Registration and Identification.
    /// </summary>
    [XmlElement("CodEORI", Form = XmlSchemaForm.Unqualified)]
    public string EoriCode { get; set; }
}