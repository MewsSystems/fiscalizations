namespace Mews.Fiscalizations.Bizkaia.Dto;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ticketbai:emision")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:ticketbai:emision", IsNullable = false)]
public partial class TicketBaiNew
{

    private Cabecera cabeceraField;

    private Sujetos sujetosField;

    private Factura facturaField;

    private HuellaTBAINEW huellaTBAIField;

    private Signature signatureField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
    public Cabecera Cabecera
    {
        get
        {
            return this.cabeceraField;
        }
        set
        {
            this.cabeceraField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
    public Sujetos Sujetos
    {
        get
        {
            return this.sujetosField;
        }
        set
        {
            this.sujetosField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
    public Factura Factura
    {
        get
        {
            return this.facturaField;
        }
        set
        {
            this.facturaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
    public HuellaTBAINEW HuellaTBAI
    {
        get
        {
            return this.huellaTBAIField;
        }
        set
        {
            this.huellaTBAIField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public Signature Signature
    {
        get
        {
            return this.signatureField;
        }
        set
        {
            this.signatureField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class CabeceraNew
{

    private decimal iDVersionTBAIField;

    /// <remarks/>
    public decimal IDVersionTBAI
    {
        get
        {
            return this.iDVersionTBAIField;
        }
        set
        {
            this.iDVersionTBAIField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class SujetosNew
{

    private SujetosEmisor emisorField;

    private SujetosDestinatarios destinatariosField;

    /// <remarks/>
    public SujetosEmisor Emisor
    {
        get
        {
            return this.emisorField;
        }
        set
        {
            this.emisorField = value;
        }
    }

    /// <remarks/>
    public SujetosDestinatarios Destinatarios
    {
        get
        {
            return this.destinatariosField;
        }
        set
        {
            this.destinatariosField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class SujetosEmisor
{

    private string nIFField;

    private string apellidosNombreRazonSocialField;

    /// <remarks/>
    public string NIF
    {
        get
        {
            return this.nIFField;
        }
        set
        {
            this.nIFField = value;
        }
    }

    /// <remarks/>
    public string ApellidosNombreRazonSocial
    {
        get
        {
            return this.apellidosNombreRazonSocialField;
        }
        set
        {
            this.apellidosNombreRazonSocialField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class SujetosDestinatarios
{

    private SujetosDestinatariosIDDestinatario iDDestinatarioField;

    /// <remarks/>
    public SujetosDestinatariosIDDestinatario IDDestinatario
    {
        get
        {
            return this.iDDestinatarioField;
        }
        set
        {
            this.iDDestinatarioField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class SujetosDestinatariosIDDestinatario
{

    private string nIFField;

    private string apellidosNombreRazonSocialField;

    /// <remarks/>
    public string NIF
    {
        get
        {
            return this.nIFField;
        }
        set
        {
            this.nIFField = value;
        }
    }

    /// <remarks/>
    public string ApellidosNombreRazonSocial
    {
        get
        {
            return this.apellidosNombreRazonSocialField;
        }
        set
        {
            this.apellidosNombreRazonSocialField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class FacturaNew
{

    private FacturaCabeceraFactura cabeceraFacturaField;

    private FacturaDatosFactura datosFacturaField;

    private FacturaTipoDesglose tipoDesgloseField;

    /// <remarks/>
    public FacturaCabeceraFactura CabeceraFactura
    {
        get
        {
            return this.cabeceraFacturaField;
        }
        set
        {
            this.cabeceraFacturaField = value;
        }
    }

    /// <remarks/>
    public FacturaDatosFactura DatosFactura
    {
        get
        {
            return this.datosFacturaField;
        }
        set
        {
            this.datosFacturaField = value;
        }
    }

    /// <remarks/>
    public FacturaTipoDesglose TipoDesglose
    {
        get
        {
            return this.tipoDesgloseField;
        }
        set
        {
            this.tipoDesgloseField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaCabeceraFactura
{

    private string serieFacturaField;

    private ushort numFacturaField;

    private string fechaExpedicionFacturaField;

    private System.DateTime horaExpedicionFacturaField;

    /// <remarks/>
    public string SerieFactura
    {
        get
        {
            return this.serieFacturaField;
        }
        set
        {
            this.serieFacturaField = value;
        }
    }

    /// <remarks/>
    public ushort NumFactura
    {
        get
        {
            return this.numFacturaField;
        }
        set
        {
            this.numFacturaField = value;
        }
    }

    /// <remarks/>
    public string FechaExpedicionFactura
    {
        get
        {
            return this.fechaExpedicionFacturaField;
        }
        set
        {
            this.fechaExpedicionFacturaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
    public System.DateTime HoraExpedicionFactura
    {
        get
        {
            return this.horaExpedicionFacturaField;
        }
        set
        {
            this.horaExpedicionFacturaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaDatosFactura
{

    private string descripcionFacturaField;

    private decimal importeTotalFacturaField;

    private FacturaDatosFacturaClaves clavesField;

    /// <remarks/>
    public string DescripcionFactura
    {
        get
        {
            return this.descripcionFacturaField;
        }
        set
        {
            this.descripcionFacturaField = value;
        }
    }

    /// <remarks/>
    public decimal ImporteTotalFactura
    {
        get
        {
            return this.importeTotalFacturaField;
        }
        set
        {
            this.importeTotalFacturaField = value;
        }
    }

    /// <remarks/>
    public FacturaDatosFacturaClaves Claves
    {
        get
        {
            return this.clavesField;
        }
        set
        {
            this.clavesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaDatosFacturaClaves
{

    private FacturaDatosFacturaClavesIDClave iDClaveField;

    /// <remarks/>
    public FacturaDatosFacturaClavesIDClave IDClave
    {
        get
        {
            return this.iDClaveField;
        }
        set
        {
            this.iDClaveField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaDatosFacturaClavesIDClave
{

    private byte claveRegimenIvaOpTrascendenciaField;

    /// <remarks/>
    public byte ClaveRegimenIvaOpTrascendencia
    {
        get
        {
            return this.claveRegimenIvaOpTrascendenciaField;
        }
        set
        {
            this.claveRegimenIvaOpTrascendenciaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesglose
{

    private FacturaTipoDesgloseDesgloseFactura desgloseFacturaField;

    /// <remarks/>
    public FacturaTipoDesgloseDesgloseFactura DesgloseFactura
    {
        get
        {
            return this.desgloseFacturaField;
        }
        set
        {
            this.desgloseFacturaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFactura
{

    private FacturaTipoDesgloseDesgloseFacturaSujeta sujetaField;

    /// <remarks/>
    public FacturaTipoDesgloseDesgloseFacturaSujeta Sujeta
    {
        get
        {
            return this.sujetaField;
        }
        set
        {
            this.sujetaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFacturaSujeta
{

    private FacturaTipoDesgloseDesgloseFacturaSujetaNoExenta noExentaField;

    /// <remarks/>
    public FacturaTipoDesgloseDesgloseFacturaSujetaNoExenta NoExenta
    {
        get
        {
            return this.noExentaField;
        }
        set
        {
            this.noExentaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFacturaSujetaNoExenta
{

    private FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExenta detalleNoExentaField;

    /// <remarks/>
    public FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExenta DetalleNoExenta
    {
        get
        {
            return this.detalleNoExentaField;
        }
        set
        {
            this.detalleNoExentaField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExenta
{

    private string tipoNoExentaField;

    private FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDesgloseIVA desgloseIVAField;

    /// <remarks/>
    public string TipoNoExenta
    {
        get
        {
            return this.tipoNoExentaField;
        }
        set
        {
            this.tipoNoExentaField = value;
        }
    }

    /// <remarks/>
    public FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDesgloseIVA DesgloseIVA
    {
        get
        {
            return this.desgloseIVAField;
        }
        set
        {
            this.desgloseIVAField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDesgloseIVA
{

    private FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDesgloseIVADetalleIVA detalleIVAField;

    /// <remarks/>
    public FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDesgloseIVADetalleIVA DetalleIVA
    {
        get
        {
            return this.detalleIVAField;
        }
        set
        {
            this.detalleIVAField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDesgloseIVADetalleIVA
{

    private decimal baseImponibleField;

    private decimal tipoImpositivoField;

    private decimal cuotaImpuestoField;

    /// <remarks/>
    public decimal BaseImponible
    {
        get
        {
            return this.baseImponibleField;
        }
        set
        {
            this.baseImponibleField = value;
        }
    }

    /// <remarks/>
    public decimal TipoImpositivo
    {
        get
        {
            return this.tipoImpositivoField;
        }
        set
        {
            this.tipoImpositivoField = value;
        }
    }

    /// <remarks/>
    public decimal CuotaImpuesto
    {
        get
        {
            return this.cuotaImpuestoField;
        }
        set
        {
            this.cuotaImpuestoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class HuellaTBAI2
{

    private HuellaTBAIEncadenamientoFacturaAnterior encadenamientoFacturaAnteriorField;

    private HuellaTBAISoftware softwareField;

    private string numSerieDispositivoField;

    /// <remarks/>
    public HuellaTBAIEncadenamientoFacturaAnterior EncadenamientoFacturaAnterior
    {
        get
        {
            return this.encadenamientoFacturaAnteriorField;
        }
        set
        {
            this.encadenamientoFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public HuellaTBAISoftware Software
    {
        get
        {
            return this.softwareField;
        }
        set
        {
            this.softwareField = value;
        }
    }

    /// <remarks/>
    public string NumSerieDispositivo
    {
        get
        {
            return this.numSerieDispositivoField;
        }
        set
        {
            this.numSerieDispositivoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class HuellaTBAIEncadenamientoFacturaAnterior
{

    private string serieFacturaAnteriorField;

    private ushort numFacturaAnteriorField;

    private string fechaExpedicionFacturaAnteriorField;

    private string signatureValueFirmaFacturaAnteriorField;

    /// <remarks/>
    public string SerieFacturaAnterior
    {
        get
        {
            return this.serieFacturaAnteriorField;
        }
        set
        {
            this.serieFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public ushort NumFacturaAnterior
    {
        get
        {
            return this.numFacturaAnteriorField;
        }
        set
        {
            this.numFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public string FechaExpedicionFacturaAnterior
    {
        get
        {
            return this.fechaExpedicionFacturaAnteriorField;
        }
        set
        {
            this.fechaExpedicionFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public string SignatureValueFirmaFacturaAnterior
    {
        get
        {
            return this.signatureValueFirmaFacturaAnteriorField;
        }
        set
        {
            this.signatureValueFirmaFacturaAnteriorField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class HuellaTBAISoftware
{

    private string licenciaTBAIField;

    private HuellaTBAISoftwareEntidadDesarrolladora entidadDesarrolladoraField;

    private string nombreField;

    private string versionField;

    /// <remarks/>
    public string LicenciaTBAI
    {
        get
        {
            return this.licenciaTBAIField;
        }
        set
        {
            this.licenciaTBAIField = value;
        }
    }

    /// <remarks/>
    public HuellaTBAISoftwareEntidadDesarrolladora EntidadDesarrolladora
    {
        get
        {
            return this.entidadDesarrolladoraField;
        }
        set
        {
            this.entidadDesarrolladoraField = value;
        }
    }

    /// <remarks/>
    public string Nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    /// <remarks/>
    public string Version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class HuellaTBAISoftwareEntidadDesarrolladora
{

    private string nIFField;

    /// <remarks/>
    public string NIF
    {
        get
        {
            return this.nIFField;
        }
        set
        {
            this.nIFField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
public partial class Signature2
{

    private SignatureSignedInfo signedInfoField;

    private SignatureSignatureValue signatureValueField;

    private SignatureKeyInfo keyInfoField;

    private SignatureObject objectField;

    private string idField;

    /// <remarks/>
    public SignatureSignedInfo SignedInfo
    {
        get
        {
            return this.signedInfoField;
        }
        set
        {
            this.signedInfoField = value;
        }
    }

    /// <remarks/>
    public SignatureSignatureValue SignatureValue
    {
        get
        {
            return this.signatureValueField;
        }
        set
        {
            this.signatureValueField = value;
        }
    }

    /// <remarks/>
    public SignatureKeyInfo KeyInfo
    {
        get
        {
            return this.keyInfoField;
        }
        set
        {
            this.keyInfoField = value;
        }
    }

    /// <remarks/>
    public SignatureObject Object
    {
        get
        {
            return this.objectField;
        }
        set
        {
            this.objectField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfo
{

    private SignatureSignedInfoCanonicalizationMethod canonicalizationMethodField;

    private SignatureSignedInfoSignatureMethod signatureMethodField;

    private SignatureSignedInfoReference[] referenceField;

    /// <remarks/>
    public SignatureSignedInfoCanonicalizationMethod CanonicalizationMethod
    {
        get
        {
            return this.canonicalizationMethodField;
        }
        set
        {
            this.canonicalizationMethodField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoSignatureMethod SignatureMethod
    {
        get
        {
            return this.signatureMethodField;
        }
        set
        {
            this.signatureMethodField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Reference")]
    public SignatureSignedInfoReference[] Reference
    {
        get
        {
            return this.referenceField;
        }
        set
        {
            this.referenceField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoCanonicalizationMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoSignatureMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReference
{

    private SignatureSignedInfoReferenceTransform[] transformsField;

    private SignatureSignedInfoReferenceDigestMethod digestMethodField;

    private string digestValueField;

    private string idField;

    private string typeField;

    private string uRIField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
    public SignatureSignedInfoReferenceTransform[] Transforms
    {
        get
        {
            return this.transformsField;
        }
        set
        {
            this.transformsField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoReferenceDigestMethod DigestMethod
    {
        get
        {
            return this.digestMethodField;
        }
        set
        {
            this.digestMethodField = value;
        }
    }

    /// <remarks/>
    public string DigestValue
    {
        get
        {
            return this.digestValueField;
        }
        set
        {
            this.digestValueField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string URI
    {
        get
        {
            return this.uRIField;
        }
        set
        {
            this.uRIField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReferenceTransform
{

    private string xPathField;

    private string algorithmField;

    /// <remarks/>
    public string XPath
    {
        get
        {
            return this.xPathField;
        }
        set
        {
            this.xPathField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReferenceDigestMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignatureValue
{

    private string idField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfo
{

    private SignatureKeyInfoX509Data x509DataField;

    private SignatureKeyInfoKeyValue keyValueField;

    private string idField;

    /// <remarks/>
    public SignatureKeyInfoX509Data X509Data
    {
        get
        {
            return this.x509DataField;
        }
        set
        {
            this.x509DataField = value;
        }
    }

    /// <remarks/>
    public SignatureKeyInfoKeyValue KeyValue
    {
        get
        {
            return this.keyValueField;
        }
        set
        {
            this.keyValueField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoX509Data
{

    private string x509CertificateField;

    /// <remarks/>
    public string X509Certificate
    {
        get
        {
            return this.x509CertificateField;
        }
        set
        {
            this.x509CertificateField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoKeyValue
{

    private SignatureKeyInfoKeyValueRSAKeyValue rSAKeyValueField;

    /// <remarks/>
    public SignatureKeyInfoKeyValueRSAKeyValue RSAKeyValue
    {
        get
        {
            return this.rSAKeyValueField;
        }
        set
        {
            this.rSAKeyValueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoKeyValueRSAKeyValue
{

    private string modulusField;

    private string exponentField;

    /// <remarks/>
    public string Modulus
    {
        get
        {
            return this.modulusField;
        }
        set
        {
            this.modulusField = value;
        }
    }

    /// <remarks/>
    public string Exponent
    {
        get
        {
            return this.exponentField;
        }
        set
        {
            this.exponentField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureObject
{

    private QualifyingProperties qualifyingPropertiesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public QualifyingProperties QualifyingProperties
    {
        get
        {
            return this.qualifyingPropertiesField;
        }
        set
        {
            this.qualifyingPropertiesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
public partial class QualifyingProperties2
{

    private QualifyingPropertiesSignedProperties signedPropertiesField;

    private string idField;

    private string targetField;

    /// <remarks/>
    public QualifyingPropertiesSignedProperties SignedProperties
    {
        get
        {
            return this.signedPropertiesField;
        }
        set
        {
            this.signedPropertiesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Target
    {
        get
        {
            return this.targetField;
        }
        set
        {
            this.targetField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedProperties
{

    private QualifyingPropertiesSignedPropertiesSignedSignatureProperties signedSignaturePropertiesField;

    private QualifyingPropertiesSignedPropertiesSignedDataObjectProperties signedDataObjectPropertiesField;

    private string idField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignatureProperties SignedSignatureProperties
    {
        get
        {
            return this.signedSignaturePropertiesField;
        }
        set
        {
            this.signedSignaturePropertiesField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedDataObjectProperties SignedDataObjectProperties
    {
        get
        {
            return this.signedDataObjectPropertiesField;
        }
        set
        {
            this.signedDataObjectPropertiesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignatureProperties
{

    private System.DateTime signingTimeField;

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificate signingCertificateField;

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifier signaturePolicyIdentifierField;

    /// <remarks/>
    public System.DateTime SigningTime
    {
        get
        {
            return this.signingTimeField;
        }
        set
        {
            this.signingTimeField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificate SigningCertificate
    {
        get
        {
            return this.signingCertificateField;
        }
        set
        {
            this.signingCertificateField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifier SignaturePolicyIdentifier
    {
        get
        {
            return this.signaturePolicyIdentifierField;
        }
        set
        {
            this.signaturePolicyIdentifierField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificate
{

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCert certField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCert Cert
    {
        get
        {
            return this.certField;
        }
        set
        {
            this.certField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCert
{

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertCertDigest certDigestField;

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertIssuerSerial issuerSerialField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertCertDigest CertDigest
    {
        get
        {
            return this.certDigestField;
        }
        set
        {
            this.certDigestField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertIssuerSerial IssuerSerial
    {
        get
        {
            return this.issuerSerialField;
        }
        set
        {
            this.issuerSerialField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertCertDigest
{

    private DigestMethod digestMethodField;

    private string digestValueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public DigestMethod DigestMethod
    {
        get
        {
            return this.digestMethodField;
        }
        set
        {
            this.digestMethodField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public string DigestValue
    {
        get
        {
            return this.digestValueField;
        }
        set
        {
            this.digestValueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
public partial class DigestMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertIssuerSerial
{

    private string x509IssuerNameField;

    private string x509SerialNumberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public string X509IssuerName
    {
        get
        {
            return this.x509IssuerNameField;
        }
        set
        {
            this.x509IssuerNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", DataType = "integer")]
    public string X509SerialNumber
    {
        get
        {
            return this.x509SerialNumberField;
        }
        set
        {
            this.x509SerialNumberField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifier
{

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyId signaturePolicyIdField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyId SignaturePolicyId
    {
        get
        {
            return this.signaturePolicyIdField;
        }
        set
        {
            this.signaturePolicyIdField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyId
{

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyId sigPolicyIdField;

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyHash sigPolicyHashField;

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiers sigPolicyQualifiersField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyId SigPolicyId
    {
        get
        {
            return this.sigPolicyIdField;
        }
        set
        {
            this.sigPolicyIdField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyHash SigPolicyHash
    {
        get
        {
            return this.sigPolicyHashField;
        }
        set
        {
            this.sigPolicyHashField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiers SigPolicyQualifiers
    {
        get
        {
            return this.sigPolicyQualifiersField;
        }
        set
        {
            this.sigPolicyQualifiersField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyId
{

    private string identifierField;

    private object descriptionField;

    /// <remarks/>
    public string Identifier
    {
        get
        {
            return this.identifierField;
        }
        set
        {
            this.identifierField = value;
        }
    }

    /// <remarks/>
    public object Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyHash
{

    private DigestMethod digestMethodField;

    private string digestValueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public DigestMethod DigestMethod
    {
        get
        {
            return this.digestMethodField;
        }
        set
        {
            this.digestMethodField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public string DigestValue
    {
        get
        {
            return this.digestValueField;
        }
        set
        {
            this.digestValueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiers
{

    private QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiersSigPolicyQualifier sigPolicyQualifierField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiersSigPolicyQualifier SigPolicyQualifier
    {
        get
        {
            return this.sigPolicyQualifierField;
        }
        set
        {
            this.sigPolicyQualifierField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiersSigPolicyQualifier
{

    private string sPURIField;

    /// <remarks/>
    public string SPURI
    {
        get
        {
            return this.sPURIField;
        }
        set
        {
            this.sPURIField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedDataObjectProperties
{

    private QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormat dataObjectFormatField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormat DataObjectFormat
    {
        get
        {
            return this.dataObjectFormatField;
        }
        set
        {
            this.dataObjectFormatField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormat
{

    private object descriptionField;

    private QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifier objectIdentifierField;

    private string mimeTypeField;

    private object encodingField;

    private string objectReferenceField;

    /// <remarks/>
    public object Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifier ObjectIdentifier
    {
        get
        {
            return this.objectIdentifierField;
        }
        set
        {
            this.objectIdentifierField = value;
        }
    }

    /// <remarks/>
    public string MimeType
    {
        get
        {
            return this.mimeTypeField;
        }
        set
        {
            this.mimeTypeField = value;
        }
    }

    /// <remarks/>
    public object Encoding
    {
        get
        {
            return this.encodingField;
        }
        set
        {
            this.encodingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ObjectReference
    {
        get
        {
            return this.objectReferenceField;
        }
        set
        {
            this.objectReferenceField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifier
{

    private QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifierIdentifier identifierField;

    private object descriptionField;

    /// <remarks/>
    public QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifierIdentifier Identifier
    {
        get
        {
            return this.identifierField;
        }
        set
        {
            this.identifierField = value;
        }
    }

    /// <remarks/>
    public object Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
public partial class QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifierIdentifier
{

    private string qualifierField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Qualifier
    {
        get
        {
            return this.qualifierField;
        }
        set
        {
            this.qualifierField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

