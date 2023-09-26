using System.Xml.Serialization;

namespace Mews.Fiscalizations.Bizkaia.Dto;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "urn:ticketbai:emision")]
[XmlRoot(Namespace = "urn:ticketbai:emision", IsNullable = false)]
public partial class TicketBai
{

    private Cabecera1 cabeceraField;

    private Sujetos sujetosField;

    private Factura facturaField;

    private HuellaTBAI huellaTBAIField;

    private Signature signatureField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Cabecera1 Cabecera
    {
        get
        {
            return cabeceraField;
        }
        set
        {
            cabeceraField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Sujetos Sujetos
    {
        get
        {
            return sujetosField;
        }
        set
        {
            sujetosField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Factura Factura
    {
        get
        {
            return facturaField;
        }
        set
        {
            facturaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public HuellaTBAI HuellaTBAI
    {
        get
        {
            return huellaTBAIField;
        }
        set
        {
            huellaTBAIField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public Signature Signature
    {
        get
        {
            return signatureField;
        }
        set
        {
            signatureField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "Cabecera", Namespace = "urn:ticketbai:emision")]
public class Cabecera1
{

    private IDVersionTicketBaiType1 iDVersionTBAIField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public IDVersionTicketBaiType1 IDVersionTBAI
    {
        get
        {
            return iDVersionTBAIField;
        }
        set
        {
            iDVersionTBAIField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(TypeName = "IDVersionTicketBaiType", Namespace = "urn:ticketbai:emision")]
public enum IDVersionTicketBaiType1
{

    /// <remarks/>
    [XmlEnum("1.2")]
    Item12,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class Sujetos
{

    private Emisor1 emisorField;

    private IDDestinatario[] destinatariosField;

    private SiNoType variosDestinatariosField;

    private bool variosDestinatariosFieldSpecified;

    private EmitidaPorTercerosType? emitidaPorTercerosODestinatarioField;

    private bool emitidaPorTercerosODestinatarioFieldSpecified;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Emisor1 Emisor
    {
        get
        {
            return emisorField;
        }
        set
        {
            emisorField = value;
        }
    }

    /// <remarks/>
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public IDDestinatario[] Destinatarios
    {
        get
        {
            return destinatariosField;
        }
        set
        {
            destinatariosField = value;
        }
    }

    /// <remarks/>
    public SiNoType VariosDestinatarios
    {
        get
        {
            return variosDestinatariosField;
        }
        set
        {
            variosDestinatariosField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore()]
    public bool VariosDestinatariosSpecified
    {
        get
        {
            return variosDestinatariosFieldSpecified;
        }
        set
        {
            variosDestinatariosFieldSpecified = value;
        }
    }

    /// <remarks/>
    public EmitidaPorTercerosType? EmitidaPorTercerosODestinatario
    {
        get
        {
            return emitidaPorTercerosODestinatarioField;
        }
        set
        {
            emitidaPorTercerosODestinatarioField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore()]
    public bool EmitidaPorTercerosODestinatarioSpecified
    {
        get
        {
            return emitidaPorTercerosODestinatarioFieldSpecified;
        }
        set
        {
            emitidaPorTercerosODestinatarioFieldSpecified = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlType(TypeName = "Emisor", Namespace = "urn:ticketbai:emision")]
public class Emisor1
{

    private string nIFField;

    private string apellidosNombreRazonSocialField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NIF
    {
        get
        {
            return nIFField;
        }
        set
        {
            nIFField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ApellidosNombreRazonSocial
    {
        get
        {
            return apellidosNombreRazonSocialField;
        }
        set
        {
            apellidosNombreRazonSocialField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class IDDestinatario
{

    private string nIFField;

    private string apellidosNombreRazonSocialField;

    private string codigoPostalField;

    private string direccionField;

    /// <remarks/>
    [XmlElement("IDOtro", typeof(IDOtro1), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlElement("NIF", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NIF
    {
        get
        {
            return nIFField;
        }
        set
        {
            nIFField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ApellidosNombreRazonSocial
    {
        get
        {
            return apellidosNombreRazonSocialField;
        }
        set
        {
            apellidosNombreRazonSocialField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CodigoPostal
    {
        get
        {
            return codigoPostalField;
        }
        set
        {
            codigoPostalField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Direccion
    {
        get
        {
            return direccionField;
        }
        set
        {
            direccionField = value;
        }
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlType(TypeName = "IDOtro", Namespace = "urn:ticketbai:emision")]
public class IDOtro1
{

    private CountryType21? codigoPaisField;

    private bool codigoPaisFieldSpecified;

    private IDTypeType1 iDTypeField;

    private string idField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CountryType21? CodigoPais
    {
        get
        {
            return codigoPaisField;
        }
        set
        {
            codigoPaisField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore()]
    public bool CodigoPaisSpecified
    {
        get
        {
            return codigoPaisFieldSpecified;
        }
        set
        {
            codigoPaisFieldSpecified = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public IDTypeType1 IDType
    {
        get
        {
            return iDTypeField;
        }
        set
        {
            iDTypeField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ID
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(TypeName = "CountryType2", Namespace = "urn:ticketbai:emision")]
public enum CountryType21
{

    /// <remarks/>
    AF,

    /// <remarks/>
    AL,

    /// <remarks/>
    DE,

    /// <remarks/>
    AD,

    /// <remarks/>
    AO,

    /// <remarks/>
    AI,

    /// <remarks/>
    AQ,

    /// <remarks/>
    AG,

    /// <remarks/>
    SA,

    /// <remarks/>
    DZ,

    /// <remarks/>
    AR,

    /// <remarks/>
    AM,

    /// <remarks/>
    AW,

    /// <remarks/>
    AU,

    /// <remarks/>
    AT,

    /// <remarks/>
    AZ,

    /// <remarks/>
    BS,

    /// <remarks/>
    BH,

    /// <remarks/>
    BD,

    /// <remarks/>
    BB,

    /// <remarks/>
    BE,

    /// <remarks/>
    BZ,

    /// <remarks/>
    BJ,

    /// <remarks/>
    BM,

    /// <remarks/>
    BY,

    /// <remarks/>
    BO,

    /// <remarks/>
    BA,

    /// <remarks/>
    BW,

    /// <remarks/>
    BV,

    /// <remarks/>
    BR,

    /// <remarks/>
    BN,

    /// <remarks/>
    BG,

    /// <remarks/>
    BF,

    /// <remarks/>
    BI,

    /// <remarks/>
    BT,

    /// <remarks/>
    CV,

    /// <remarks/>
    KY,

    /// <remarks/>
    KH,

    /// <remarks/>
    CM,

    /// <remarks/>
    CA,

    /// <remarks/>
    CF,

    /// <remarks/>
    CC,

    /// <remarks/>
    CO,

    /// <remarks/>
    KM,

    /// <remarks/>
    CG,

    /// <remarks/>
    CD,

    /// <remarks/>
    CK,

    /// <remarks/>
    KP,

    /// <remarks/>
    KR,

    /// <remarks/>
    CI,

    /// <remarks/>
    CR,

    /// <remarks/>
    HR,

    /// <remarks/>
    CU,

    /// <remarks/>
    TD,

    /// <remarks/>
    CZ,

    /// <remarks/>
    CL,

    /// <remarks/>
    CN,

    /// <remarks/>
    CY,

    /// <remarks/>
    CW,

    /// <remarks/>
    DK,

    /// <remarks/>
    DM,

    /// <remarks/>
    DO,

    /// <remarks/>
    EC,

    /// <remarks/>
    EG,

    /// <remarks/>
    AE,

    /// <remarks/>
    ER,

    /// <remarks/>
    SK,

    /// <remarks/>
    SI,

    /// <remarks/>
    ES,

    /// <remarks/>
    US,

    /// <remarks/>
    EE,

    /// <remarks/>
    ET,

    /// <remarks/>
    FO,

    /// <remarks/>
    PH,

    /// <remarks/>
    FI,

    /// <remarks/>
    FJ,

    /// <remarks/>
    FR,

    /// <remarks/>
    GA,

    /// <remarks/>
    GM,

    /// <remarks/>
    GE,

    /// <remarks/>
    GS,

    /// <remarks/>
    GH,

    /// <remarks/>
    GI,

    /// <remarks/>
    GD,

    /// <remarks/>
    GR,

    /// <remarks/>
    GL,

    /// <remarks/>
    GU,

    /// <remarks/>
    GT,

    /// <remarks/>
    GG,

    /// <remarks/>
    GN,

    /// <remarks/>
    GQ,

    /// <remarks/>
    GW,

    /// <remarks/>
    GY,

    /// <remarks/>
    HT,

    /// <remarks/>
    HM,

    /// <remarks/>
    HN,

    /// <remarks/>
    HK,

    /// <remarks/>
    HU,

    /// <remarks/>
    IN,

    /// <remarks/>
    ID,

    /// <remarks/>
    IR,

    /// <remarks/>
    IQ,

    /// <remarks/>
    IE,

    /// <remarks/>
    IM,

    /// <remarks/>
    IS,

    /// <remarks/>
    IL,

    /// <remarks/>
    IT,

    /// <remarks/>
    JM,

    /// <remarks/>
    JP,

    /// <remarks/>
    JE,

    /// <remarks/>
    JO,

    /// <remarks/>
    KZ,

    /// <remarks/>
    KE,

    /// <remarks/>
    KG,

    /// <remarks/>
    KI,

    /// <remarks/>
    KW,

    /// <remarks/>
    LA,

    /// <remarks/>
    LS,

    /// <remarks/>
    LV,

    /// <remarks/>
    LB,

    /// <remarks/>
    LR,

    /// <remarks/>
    LY,

    /// <remarks/>
    LI,

    /// <remarks/>
    LT,

    /// <remarks/>
    LU,

    /// <remarks/>
    XG,

    /// <remarks/>
    MO,

    /// <remarks/>
    MK,

    /// <remarks/>
    MG,

    /// <remarks/>
    MY,

    /// <remarks/>
    MW,

    /// <remarks/>
    MV,

    /// <remarks/>
    ML,

    /// <remarks/>
    MT,

    /// <remarks/>
    FK,

    /// <remarks/>
    MP,

    /// <remarks/>
    MA,

    /// <remarks/>
    MH,

    /// <remarks/>
    MU,

    /// <remarks/>
    MR,

    /// <remarks/>
    YT,

    /// <remarks/>
    UM,

    /// <remarks/>
    MX,

    /// <remarks/>
    FM,

    /// <remarks/>
    MD,

    /// <remarks/>
    MC,

    /// <remarks/>
    MN,

    /// <remarks/>
    ME,

    /// <remarks/>
    MS,

    /// <remarks/>
    MZ,

    /// <remarks/>
    MM,

    /// <remarks/>
    NA,

    /// <remarks/>
    NR,

    /// <remarks/>
    CX,

    /// <remarks/>
    NP,

    /// <remarks/>
    NI,

    /// <remarks/>
    NE,

    /// <remarks/>
    NG,

    /// <remarks/>
    NU,

    /// <remarks/>
    NF,

    /// <remarks/>
    NO,

    /// <remarks/>
    NC,

    /// <remarks/>
    NZ,

    /// <remarks/>
    IO,

    /// <remarks/>
    OM,

    /// <remarks/>
    NL,

    /// <remarks/>
    BQ,

    /// <remarks/>
    PK,

    /// <remarks/>
    PW,

    /// <remarks/>
    PA,

    /// <remarks/>
    PG,

    /// <remarks/>
    PY,

    /// <remarks/>
    PE,

    /// <remarks/>
    PN,

    /// <remarks/>
    PF,

    /// <remarks/>
    PL,

    /// <remarks/>
    PT,

    /// <remarks/>
    PR,

    /// <remarks/>
    QA,

    /// <remarks/>
    GB,

    /// <remarks/>
    RW,

    /// <remarks/>
    RO,

    /// <remarks/>
    RU,

    /// <remarks/>
    SB,

    /// <remarks/>
    SV,

    /// <remarks/>
    WS,

    /// <remarks/>
    AS,

    /// <remarks/>
    KN,

    /// <remarks/>
    SM,

    /// <remarks/>
    SX,

    /// <remarks/>
    PM,

    /// <remarks/>
    VC,

    /// <remarks/>
    SH,

    /// <remarks/>
    LC,

    /// <remarks/>
    ST,

    /// <remarks/>
    SN,

    /// <remarks/>
    RS,

    /// <remarks/>
    SC,

    /// <remarks/>
    SL,

    /// <remarks/>
    SG,

    /// <remarks/>
    SY,

    /// <remarks/>
    SO,

    /// <remarks/>
    LK,

    /// <remarks/>
    SZ,

    /// <remarks/>
    ZA,

    /// <remarks/>
    SD,

    /// <remarks/>
    SS,

    /// <remarks/>
    SE,

    /// <remarks/>
    CH,

    /// <remarks/>
    SR,

    /// <remarks/>
    TH,

    /// <remarks/>
    TW,

    /// <remarks/>
    TZ,

    /// <remarks/>
    TJ,

    /// <remarks/>
    PS,

    /// <remarks/>
    TF,

    /// <remarks/>
    TL,

    /// <remarks/>
    TG,

    /// <remarks/>
    TK,

    /// <remarks/>
    TO,

    /// <remarks/>
    TT,

    /// <remarks/>
    TN,

    /// <remarks/>
    TC,

    /// <remarks/>
    TM,

    /// <remarks/>
    TR,

    /// <remarks/>
    TV,

    /// <remarks/>
    UA,

    /// <remarks/>
    UG,

    /// <remarks/>
    UY,

    /// <remarks/>
    UZ,

    /// <remarks/>
    VU,

    /// <remarks/>
    VA,

    /// <remarks/>
    VE,

    /// <remarks/>
    VN,

    /// <remarks/>
    VG,

    /// <remarks/>
    VI,

    /// <remarks/>
    WF,

    /// <remarks/>
    YE,

    /// <remarks/>
    DJ,

    /// <remarks/>
    ZM,

    /// <remarks/>
    ZW,

    /// <remarks/>
    QU,

    /// <remarks/>
    XB,

    /// <remarks/>
    XU,

    /// <remarks/>
    XN,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(TypeName = "IDTypeType", Namespace = "urn:ticketbai:emision")]
public enum IDTypeType1
{

    /// <remarks/>
    [XmlEnum("02")]
    Item02,

    /// <remarks/>
    [XmlEnum("03")]
    Item03,

    /// <remarks/>
    [XmlEnum("04")]
    Item04,

    /// <remarks/>
    [XmlEnum("05")]
    Item05,

    /// <remarks/>
    [XmlEnum("06")]
    Item06,
}


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum SiNoType
{

    /// <remarks/>
    S,

    /// <remarks/>
    N,
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum EmitidaPorTercerosType
{

    /// <remarks/>
    N,

    /// <remarks/>
    T,

    /// <remarks/>
    D,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public class Factura
{
    private CabeceraFacturaType1 cabeceraFacturaField;

    private DatosFacturaType datosFacturaField;

    private TipoDesgloseType tipoDesgloseField;

    /// <remarks/>
    public CabeceraFacturaType1 CabeceraFactura
    {
        get
        {
            return cabeceraFacturaField;
        }
        set
        {
            cabeceraFacturaField = value;
        }
    }

    /// <remarks/>
    public DatosFacturaType DatosFactura
    {
        get
        {
            return datosFacturaField;
        }
        set
        {
            datosFacturaField = value;
        }
    }

    /// <remarks/>
    public TipoDesgloseType TipoDesglose
    {
        get
        {
            return tipoDesgloseField;
        }
        set
        {
            tipoDesgloseField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "CabeceraFactura", Namespace = "urn:ticketbai:emision")]
public partial class CabeceraFacturaType1
{

    private string serieFacturaField;

    private string numFacturaField;

    private string fechaExpedicionFacturaField;

    private string horaExpedicionFacturaField;

    private string facturaSimplificadaField;

    private string facturaEmitidaSustitucionSimplificadaField;

    private FacturaRectificativaType facturaRectificativaField;

    private FacturaCabeceraFacturaIDFacturaRectificadaSustituida[] facturasRectificadasSustituidasField;

    /// <remarks/>
    public string SerieFactura
    {
        get
        {
            return serieFacturaField;
        }
        set
        {
            serieFacturaField = value;
        }
    }

    /// <remarks/>
    public string NumFactura
    {
        get
        {
            return numFacturaField;
        }
        set
        {
            numFacturaField = value;
        }
    }

    /// <remarks/>
    public string FechaExpedicionFactura
    {
        get
        {
            return fechaExpedicionFacturaField;
        }
        set
        {
            fechaExpedicionFacturaField = value;
        }
    }

    /// <remarks/>
    public string HoraExpedicionFactura
    {
        get
        {
            return horaExpedicionFacturaField;
        }
        set
        {
            horaExpedicionFacturaField = value;
        }
    }

    /// <remarks/>
    public string FacturaSimplificada
    {
        get
        {
            return facturaSimplificadaField;
        }
        set
        {
            facturaSimplificadaField = value;
        }
    }

    /// <remarks/>
    public string FacturaEmitidaSustitucionSimplificada
    {
        get
        {
            return facturaEmitidaSustitucionSimplificadaField;
        }
        set
        {
            facturaEmitidaSustitucionSimplificadaField = value;
        }
    }

    /// <remarks/>
    public FacturaRectificativaType FacturaRectificativa
    {
        get
        {
            return facturaRectificativaField;
        }
        set
        {
            facturaRectificativaField = value;
        }
    }

    /// <remarks/>
    [XmlArrayItem("IDFacturaRectificadaSustituida", IsNullable = false)]
    public FacturaCabeceraFacturaIDFacturaRectificadaSustituida[] FacturasRectificadasSustituidas
    {
        get
        {
            return facturasRectificadasSustituidasField;
        }
        set
        {
            facturasRectificadasSustituidasField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public class FacturaRectificativaType
{
    private ClaveTipoFacturaType codigoField;

    private ClaveTipoRectificativaType tipoField;

    private ImporteRectificacionSustitutivaType importeRectificacionSustitutivaField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ClaveTipoFacturaType Codigo
    {
        get
        {
            return codigoField;
        }
        set
        {
            codigoField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ClaveTipoRectificativaType Tipo
    {
        get
        {
            return tipoField;
        }
        set
        {
            tipoField = value;
        }
    }

    /// <remarks/>
    public ImporteRectificacionSustitutivaType ImporteRectificacionSustitutiva
    {
        get
        {
            return importeRectificacionSustitutivaField;
        }
        set
        {
            importeRectificacionSustitutivaField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum ClaveTipoFacturaType
{

    /// <remarks/>
    R1,

    /// <remarks/>
    R2,

    /// <remarks/>
    R3,

    /// <remarks/>
    R4,

    /// <remarks/>
    R5,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum ClaveTipoRectificativaType
{

    /// <remarks/>
    S,

    /// <remarks/>
    I,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public class ImporteRectificacionSustitutivaType
{
    private string baseRectificadaField;

    private string cuotaRectificadaField;

    private string cuotaRecargoRectificadaField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BaseRectificada
    {
        get
        {
            return baseRectificadaField;
        }
        set
        {
            baseRectificadaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CuotaRectificada
    {
        get
        {
            return cuotaRectificadaField;
        }
        set
        {
            cuotaRectificadaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CuotaRecargoRectificada
    {
        get
        {
            return cuotaRecargoRectificadaField;
        }
        set
        {
            cuotaRecargoRectificadaField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class FacturaCabeceraFacturaIDFacturaRectificadaSustituida
{

    private string serieFacturaField;

    private string numFacturaField;

    private string fechaExpedicionFacturaField;

    /// <remarks/>
    public string SerieFactura
    {
        get
        {
            return serieFacturaField;
        }
        set
        {
            serieFacturaField = value;
        }
    }

    /// <remarks/>
    public string NumFactura
    {
        get
        {
            return numFacturaField;
        }
        set
        {
            numFacturaField = value;
        }
    }

    /// <remarks/>
    public string FechaExpedicionFactura
    {
        get
        {
            return fechaExpedicionFacturaField;
        }
        set
        {
            fechaExpedicionFacturaField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class DatosFacturaType
{

    private string fechaOperacionField;

    private string descripcionFacturaField;

    private IDDetalleFacturaType[] detallesFacturaField;

    private string importeTotalFacturaField;

    private string retencionSoportadaField;

    private string baseImponibleACosteField;

    private IDClaveType[] clavesField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FechaOperacion
    {
        get
        {
            return fechaOperacionField;
        }
        set
        {
            fechaOperacionField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DescripcionFactura
    {
        get
        {
            return descripcionFacturaField;
        }
        set
        {
            descripcionFacturaField = value;
        }
    }

    /// <remarks/>
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem("IDDetalleFactura", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public IDDetalleFacturaType[] DetallesFactura
    {
        get
        {
            return detallesFacturaField;
        }
        set
        {
            detallesFacturaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ImporteTotalFactura
    {
        get
        {
            return importeTotalFacturaField;
        }
        set
        {
            importeTotalFacturaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string RetencionSoportada
    {
        get
        {
            return retencionSoportadaField;
        }
        set
        {
            retencionSoportadaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BaseImponibleACoste
    {
        get
        {
            return baseImponibleACosteField;
        }
        set
        {
            baseImponibleACosteField = value;
        }
    }

    /// <remarks/>
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem("IDClave", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public IDClaveType[] Claves
    {
        get
        {
            return clavesField;
        }
        set
        {
            clavesField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public class IDDetalleFacturaType
{
    private string descripcionDetalleField;

    private string cantidadField;

    private string importeUnitarioField;

    private string descuentoField;

    private string importeTotalField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DescripcionDetalle
    {
        get
        {
            return descripcionDetalleField;
        }
        set
        {
            descripcionDetalleField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Cantidad
    {
        get
        {
            return cantidadField;
        }
        set
        {
            cantidadField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ImporteUnitario
    {
        get
        {
            return importeUnitarioField;
        }
        set
        {
            importeUnitarioField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Descuento
    {
        get
        {
            return descuentoField;
        }
        set
        {
            descuentoField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ImporteTotal
    {
        get
        {
            return importeTotalField;
        }
        set
        {
            importeTotalField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class IDClaveType
{

    private IdOperacionesTrascendenciaTributariaType claveRegimenIvaOpTrascendenciaField;

    /// <remarks/>
    public IdOperacionesTrascendenciaTributariaType ClaveRegimenIvaOpTrascendencia
    {
        get
        {
            return claveRegimenIvaOpTrascendenciaField;
        }
        set
        {
            claveRegimenIvaOpTrascendenciaField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum IdOperacionesTrascendenciaTributariaType
{

    /// <remarks/>
    [XmlEnum("01")]
    Item01,

    /// <remarks/>
    [XmlEnum("02")]
    Item02,

    /// <remarks/>
    [XmlEnum("03")]
    Item03,

    /// <remarks/>
    [XmlEnum("04")]
    Item04,

    /// <remarks/>
    [XmlEnum("05")]
    Item05,

    /// <remarks/>
    [XmlEnum("06")]
    Item06,

    /// <remarks/>
    [XmlEnum("07")]
    Item07,

    /// <remarks/>
    [XmlEnum("08")]
    Item08,

    /// <remarks/>
    [XmlEnum("09")]
    Item09,

    /// <remarks/>
    [XmlEnum("10")]
    Item10,

    /// <remarks/>
    [XmlEnum("11")]
    Item11,

    /// <remarks/>
    [XmlEnum("12")]
    Item12,

    /// <remarks/>
    [XmlEnum("13")]
    Item13,

    /// <remarks/>
    [XmlEnum("14")]
    Item14,

    /// <remarks/>
    [XmlEnum("15")]
    Item15,

    /// <remarks/>
    [XmlEnum("51")]
    Item51,

    /// <remarks/>
    [XmlEnum("52")]
    Item52,

    /// <remarks/>
    [XmlEnum("53")]
    Item53,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class TipoDesgloseType
{

    private DesgloseFacturaType itemField;

    public DesgloseFacturaType DesgloseFactura
    {
        get
        {
            return itemField;
        }
        set
        {
            itemField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class DesgloseFacturaType
{

    private SujetaType sujetaField;

    private FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta[] noSujetaField;

    /// <remarks/>
    public SujetaType Sujeta
    {
        get
        {
            return sujetaField;
        }
        set
        {
            sujetaField = value;
        }
    }

    /// <remarks/>
    [XmlArrayItem("DetalleNoSujeta", IsNullable = false)]
    public FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta[] NoSujeta
    {
        get
        {
            return noSujetaField;
        }
        set
        {
            noSujetaField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class SujetaType
{

    private DetalleExentaType[] exentaField;

    private DetalleNoExentaType[] noExentaField;

    /// <remarks/>
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem("DetalleExenta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public DetalleExentaType[] Exenta
    {
        get
        {
            return exentaField;
        }
        set
        {
            exentaField = value;
        }
    }

    /// <remarks/>
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem("DetalleNoExenta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public DetalleNoExentaType[] NoExenta
    {
        get
        {
            return noExentaField;
        }
        set
        {
            noExentaField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public class DetalleExentaType
{

    private CausaExencionType causaExencionField;

    private string baseImponibleField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CausaExencionType CausaExencion
    {
        get
        {
            return causaExencionField;
        }
        set
        {
            causaExencionField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BaseImponible
    {
        get
        {
            return baseImponibleField;
        }
        set
        {
            baseImponibleField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum CausaExencionType
{

    /// <remarks/>
    E1,

    /// <remarks/>
    E2,

    /// <remarks/>
    E3,

    /// <remarks/>
    E4,

    /// <remarks/>
    E5,

    /// <remarks/>
    E6,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public partial class DetalleNoExentaType
{

    private TipoOperacionSujetaNoExentaType tipoNoExentaField;

    private DetalleIVAType[] desgloseIVAField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TipoOperacionSujetaNoExentaType TipoNoExenta
    {
        get
        {
            return tipoNoExentaField;
        }
        set
        {
            tipoNoExentaField = value;
        }
    }

    /// <remarks/>
    [XmlArrayItem("DetalleIVA", IsNullable = false)]
    public DetalleIVAType[] DesgloseIVA
    {
        get
        {
            return desgloseIVAField;
        }
        set
        {
            desgloseIVAField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlType(Namespace = "urn:ticketbai:emision")]
public enum TipoOperacionSujetaNoExentaType
{

    /// <remarks/>
    S1,

    /// <remarks/>
    S2,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(Namespace = "urn:ticketbai:emision")]
public class DetalleIVAType
{

    private string baseImponibleField;

    private string tipoImpositivoField;

    private string cuotaImpuestoField;

    private string tipoRecargoEquivalenciaField;

    private string cuotaRecargoEquivalenciaField;

    private SiNoType operacionEnRecargoDeEquivalenciaORegimenSimplificadoField;

    private bool operacionEnRecargoDeEquivalenciaORegimenSimplificadoFieldSpecified;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BaseImponible
    {
        get
        {
            return baseImponibleField;
        }
        set
        {
            baseImponibleField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TipoImpositivo
    {
        get
        {
            return tipoImpositivoField;
        }
        set
        {
            tipoImpositivoField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CuotaImpuesto
    {
        get
        {
            return cuotaImpuestoField;
        }
        set
        {
            cuotaImpuestoField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TipoRecargoEquivalencia
    {
        get
        {
            return tipoRecargoEquivalenciaField;
        }
        set
        {
            tipoRecargoEquivalenciaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CuotaRecargoEquivalencia
    {
        get
        {
            return cuotaRecargoEquivalenciaField;
        }
        set
        {
            cuotaRecargoEquivalenciaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SiNoType OperacionEnRecargoDeEquivalenciaORegimenSimplificado
    {
        get
        {
            return operacionEnRecargoDeEquivalenciaORegimenSimplificadoField;
        }
        set
        {
            operacionEnRecargoDeEquivalenciaORegimenSimplificadoField = value;
        }
    }

    /// <remarks/>
    [XmlIgnore()]
    public bool OperacionEnRecargoDeEquivalenciaORegimenSimplificadoSpecified
    {
        get
        {
            return operacionEnRecargoDeEquivalenciaORegimenSimplificadoFieldSpecified;
        }
        set
        {
            operacionEnRecargoDeEquivalenciaORegimenSimplificadoFieldSpecified = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta
{

    private string causaField;

    private string importeField;

    /// <remarks/>
    public string Causa
    {
        get
        {
            return causaField;
        }
        set
        {
            causaField = value;
        }
    }

    /// <remarks/>
    public string Importe
    {
        get
        {
            return importeField;
        }
        set
        {
            importeField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class HuellaTBAI
{

    private HuellaTBAIEncadenamientoFacturaAnterior encadenamientoFacturaAnteriorField;

    private HuellaTBAISoftware softwareField;

    private string numSerieDispositivoField;

    /// <remarks/>
    public HuellaTBAIEncadenamientoFacturaAnterior EncadenamientoFacturaAnterior
    {
        get
        {
            return encadenamientoFacturaAnteriorField;
        }
        set
        {
            encadenamientoFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public HuellaTBAISoftware Software
    {
        get
        {
            return softwareField;
        }
        set
        {
            softwareField = value;
        }
    }

    /// <remarks/>
    public string NumSerieDispositivo
    {
        get
        {
            return numSerieDispositivoField;
        }
        set
        {
            numSerieDispositivoField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class HuellaTBAIEncadenamientoFacturaAnterior
{

    private string serieFacturaAnteriorField;

    private string numFacturaAnteriorField;

    private string fechaExpedicionFacturaAnteriorField;

    private string signatureValueFirmaFacturaAnteriorField;

    /// <remarks/>
    public string SerieFacturaAnterior
    {
        get
        {
            return serieFacturaAnteriorField;
        }
        set
        {
            serieFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public string NumFacturaAnterior
    {
        get
        {
            return numFacturaAnteriorField;
        }
        set
        {
            numFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public string FechaExpedicionFacturaAnterior
    {
        get
        {
            return fechaExpedicionFacturaAnteriorField;
        }
        set
        {
            fechaExpedicionFacturaAnteriorField = value;
        }
    }

    /// <remarks/>
    public string SignatureValueFirmaFacturaAnterior
    {
        get
        {
            return signatureValueFirmaFacturaAnteriorField;
        }
        set
        {
            signatureValueFirmaFacturaAnteriorField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
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
            return licenciaTBAIField;
        }
        set
        {
            licenciaTBAIField = value;
        }
    }

    /// <remarks/>
    public HuellaTBAISoftwareEntidadDesarrolladora EntidadDesarrolladora
    {
        get
        {
            return entidadDesarrolladoraField;
        }
        set
        {
            entidadDesarrolladoraField = value;
        }
    }

    /// <remarks/>
    public string Nombre
    {
        get
        {
            return nombreField;
        }
        set
        {
            nombreField = value;
        }
    }

    /// <remarks/>
    public string Version
    {
        get
        {
            return versionField;
        }
        set
        {
            versionField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class HuellaTBAISoftwareEntidadDesarrolladora
{

    private string nIFField;

    /// <remarks/>
    public string NIF
    {
        get
        {
            return nIFField;
        }
        set
        {
            nIFField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
[XmlRoot(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
public partial class Signature
{

    private SignatureSignedInfo signedInfoField;

    private SignatureSignatureValue signatureValueField;

    private SignatureKeyInfo keyInfoField;

    private SignatureObject[] objectField;

    private string idField;

    /// <remarks/>
    public SignatureSignedInfo SignedInfo
    {
        get
        {
            return signedInfoField;
        }
        set
        {
            signedInfoField = value;
        }
    }

    /// <remarks/>
    public SignatureSignatureValue SignatureValue
    {
        get
        {
            return signatureValueField;
        }
        set
        {
            signatureValueField = value;
        }
    }

    /// <remarks/>
    public SignatureKeyInfo KeyInfo
    {
        get
        {
            return keyInfoField;
        }
        set
        {
            keyInfoField = value;
        }
    }

    /// <remarks/>
    [XmlElement("Object")]
    public SignatureObject[] Object
    {
        get
        {
            return objectField;
        }
        set
        {
            objectField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfo
{

    private SignatureSignedInfoCanonicalizationMethod canonicalizationMethodField;

    private SignatureSignedInfoSignatureMethod signatureMethodField;

    private SignatureSignedInfoReference[] referenceField;

    private string idField;

    /// <remarks/>
    public SignatureSignedInfoCanonicalizationMethod CanonicalizationMethod
    {
        get
        {
            return canonicalizationMethodField;
        }
        set
        {
            canonicalizationMethodField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoSignatureMethod SignatureMethod
    {
        get
        {
            return signatureMethodField;
        }
        set
        {
            signatureMethodField = value;
        }
    }

    /// <remarks/>
    [XmlElement("Reference")]
    public SignatureSignedInfoReference[] Reference
    {
        get
        {
            return referenceField;
        }
        set
        {
            referenceField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoCanonicalizationMethod
{

    private string algorithmField;

    private string valueField;

    /// <remarks/>
    [XmlAttribute()]
    public string Algorithm
    {
        get
        {
            return algorithmField;
        }
        set
        {
            algorithmField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoSignatureMethod
{

    private byte hMACOutputLengthField;

    private string[] textField;

    private string algorithmField;

    /// <remarks/>
    public byte HMACOutputLength
    {
        get
        {
            return hMACOutputLengthField;
        }
        set
        {
            hMACOutputLengthField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string[] Text
    {
        get
        {
            return textField;
        }
        set
        {
            textField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Algorithm
    {
        get
        {
            return algorithmField;
        }
        set
        {
            algorithmField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReference
{

    private SignatureSignedInfoReferenceTransform[] transformsField;

    private SignatureSignedInfoReferenceDigestMethod digestMethodField;

    private string digestValueField;

    private string idField;

    private string uRIField;

    private string typeField;

    /// <remarks/>
    [XmlArrayItem("Transform", IsNullable = false)]
    public SignatureSignedInfoReferenceTransform[] Transforms
    {
        get
        {
            return transformsField;
        }
        set
        {
            transformsField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoReferenceDigestMethod DigestMethod
    {
        get
        {
            return digestMethodField;
        }
        set
        {
            digestMethodField = value;
        }
    }

    /// <remarks/>
    public string DigestValue
    {
        get
        {
            return digestValueField;
        }
        set
        {
            digestValueField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string URI
    {
        get
        {
            return uRIField;
        }
        set
        {
            uRIField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReferenceTransform
{

    private string[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    private string[] textField;

    private string algorithmField;

    /// <remarks/>
    [XmlElement("XPath", typeof(string))]
    [XmlElement("any_element", typeof(string), Namespace = "otherNS")]
    [XmlChoiceIdentifier("ItemsElementName")]
    public string[] Items
    {
        get
        {
            return itemsField;
        }
        set
        {
            itemsField = value;
        }
    }

    /// <remarks/>
    [XmlElement("ItemsElementName")]
    [XmlIgnore()]
    public ItemsChoiceType[] ItemsElementName
    {
        get
        {
            return itemsElementNameField;
        }
        set
        {
            itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string[] Text
    {
        get
        {
            return textField;
        }
        set
        {
            textField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Algorithm
    {
        get
        {
            return algorithmField;
        }
        set
        {
            algorithmField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
public enum ItemsChoiceType
{

    /// <remarks/>
    XPath,

    /// <remarks/>
    [XmlEnum("otherNS:any_element")]
    any_element,
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReferenceDigestMethod
{

    private string algorithmField;

    private string valueField;

    /// <remarks/>
    [XmlAttribute()]
    public string Algorithm
    {
        get
        {
            return algorithmField;
        }
        set
        {
            algorithmField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignatureValue
{

    private string idField;

    private string valueField;

    /// <remarks/>
    [XmlAttribute()]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string Value
    {
        get
        {
            return valueField;
        }
        set
        {
            valueField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfo
{

    private string keyNameField;

    private SignatureKeyInfoKeyValue keyValueField;

    private SignatureKeyInfoRetrievalMethod retrievalMethodField;

    private string[] textField;

    private string idField;

    /// <remarks/>
    public string KeyName
    {
        get
        {
            return keyNameField;
        }
        set
        {
            keyNameField = value;
        }
    }

    /// <remarks/>
    public SignatureKeyInfoKeyValue KeyValue
    {
        get
        {
            return keyValueField;
        }
        set
        {
            keyValueField = value;
        }
    }

    /// <remarks/>
    public SignatureKeyInfoRetrievalMethod RetrievalMethod
    {
        get
        {
            return retrievalMethodField;
        }
        set
        {
            retrievalMethodField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string[] Text
    {
        get
        {
            return textField;
        }
        set
        {
            textField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoKeyValue
{

    private SignatureKeyInfoKeyValueDSAKeyValue dSAKeyValueField;

    private string[] textField;

    /// <remarks/>
    public SignatureKeyInfoKeyValueDSAKeyValue DSAKeyValue
    {
        get
        {
            return dSAKeyValueField;
        }
        set
        {
            dSAKeyValueField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string[] Text
    {
        get
        {
            return textField;
        }
        set
        {
            textField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoKeyValueDSAKeyValue
{

    private string pField;

    private string qField;

    private string gField;

    private string yField;

    private string jField;

    private string seedField;

    private string pgenCounterField;

    /// <remarks/>
    public string P
    {
        get
        {
            return pField;
        }
        set
        {
            pField = value;
        }
    }

    /// <remarks/>
    public string Q
    {
        get
        {
            return qField;
        }
        set
        {
            qField = value;
        }
    }

    /// <remarks/>
    public string G
    {
        get
        {
            return gField;
        }
        set
        {
            gField = value;
        }
    }

    /// <remarks/>
    public string Y
    {
        get
        {
            return yField;
        }
        set
        {
            yField = value;
        }
    }

    /// <remarks/>
    public string J
    {
        get
        {
            return jField;
        }
        set
        {
            jField = value;
        }
    }

    /// <remarks/>
    public string Seed
    {
        get
        {
            return seedField;
        }
        set
        {
            seedField = value;
        }
    }

    /// <remarks/>
    public string PgenCounter
    {
        get
        {
            return pgenCounterField;
        }
        set
        {
            pgenCounterField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoRetrievalMethod
{

    private string uRIField;

    private string typeField;

    /// <remarks/>
    [XmlAttribute()]
    public string URI
    {
        get
        {
            return uRIField;
        }
        set
        {
            uRIField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Type
    {
        get
        {
            return typeField;
        }
        set
        {
            typeField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureObject
{

    private string[] any_elementField;

    private string[] textField;

    private string idField;

    private string mimeTypeField;

    private string encodingField;

    /// <remarks/>
    [XmlElement("any_element", Namespace = "urn:ticketbai:emision")]
    public string[] any_element
    {
        get
        {
            return any_elementField;
        }
        set
        {
            any_elementField = value;
        }
    }

    /// <remarks/>
    [XmlText()]
    public string[] Text
    {
        get
        {
            return textField;
        }
        set
        {
            textField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Id
    {
        get
        {
            return idField;
        }
        set
        {
            idField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string MimeType
    {
        get
        {
            return mimeTypeField;
        }
        set
        {
            mimeTypeField = value;
        }
    }

    /// <remarks/>
    [XmlAttribute()]
    public string Encoding
    {
        get
        {
            return encodingField;
        }
        set
        {
            encodingField = value;
        }
    }
}
