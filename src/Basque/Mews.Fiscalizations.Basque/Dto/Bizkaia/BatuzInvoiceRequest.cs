using System.Xml.Serialization;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd")]
[XmlRoot(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd", IsNullable = false)]
public class LROEPJ240FacturasEmitidasConSGAltaPeticion
{
    private Cabecera2 cabeceraField;

    private FacturaEmitidaType[] facturasEmitidasField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Cabecera2 Cabecera
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
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public FacturaEmitidaType[] FacturasEmitidas
    {
        get
        {
            return facturasEmitidasField;
        }
        set
        {
            facturasEmitidasField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "Cabecera", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class Cabecera2
{

    private string modeloField;

    private string capituloField;

    private string subcapituloField;

    private string operacionField;

    private string versionField;

    private string ejercicioField;

    private ObligadoTributarioType obligadoTributarioField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Modelo
    {
        get
        {
            return modeloField;
        }
        set
        {
            modeloField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Capitulo
    {
        get
        {
            return capituloField;
        }
        set
        {
            capituloField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Subcapitulo
    {
        get
        {
            return subcapituloField;
        }
        set
        {
            subcapituloField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Operacion
    {
        get
        {
            return operacionField;
        }
        set
        {
            operacionField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Ejercicio
    {
        get
        {
            return ejercicioField;
        }
        set
        {
            ejercicioField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ObligadoTributarioType ObligadoTributario
    {
        get
        {
            return obligadoTributarioField;
        }
        set
        {
            obligadoTributarioField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "ObligadoTributario", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class ObligadoTributarioType
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
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "FacturaEmitida", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class FacturaEmitidaType
{

    private string ticketBaiField;

    private OtraInformacionTrascendenciaTributariaType otraInformacionTrascendenciaTributariaField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TicketBai
    {
        get
        {
            return ticketBaiField;
        }
        set
        {
            ticketBaiField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public OtraInformacionTrascendenciaTributariaType OtraInformacionTrascendenciaTributaria
    {
        get
        {
            return otraInformacionTrascendenciaTributariaField;
        }
        set
        {
            otraInformacionTrascendenciaTributariaField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "OtraInformacionTrascendenciaTributaria", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class OtraInformacionTrascendenciaTributariaType
{

    private string nIFRepresentanteDeclaradoField;

    private DetalleInmuebleType[] inmueblesField;

    private string importeTransmisionInmueblesSujetoAIVAField;

    private string numRegistroAcuerdoFacturacionField;

    private string referenciaExternaField;

    private string cuponField;

    private EntidadSucedidaType entidadSucedidaField;

    private string facturacionDispAdicionalSegundaYQuintaField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NIFRepresentanteDeclarado
    {
        get
        {
            return nIFRepresentanteDeclaradoField;
        }
        set
        {
            nIFRepresentanteDeclaradoField = value;
        }
    }

    /// <remarks/>
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public DetalleInmuebleType[] Inmuebles
    {
        get
        {
            return inmueblesField;
        }
        set
        {
            inmueblesField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ImporteTransmisionInmueblesSujetoAIVA
    {
        get
        {
            return importeTransmisionInmueblesSujetoAIVAField;
        }
        set
        {
            importeTransmisionInmueblesSujetoAIVAField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NumRegistroAcuerdoFacturacion
    {
        get
        {
            return numRegistroAcuerdoFacturacionField;
        }
        set
        {
            numRegistroAcuerdoFacturacionField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReferenciaExterna
    {
        get
        {
            return referenciaExternaField;
        }
        set
        {
            referenciaExternaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Cupon
    {
        get
        {
            return cuponField;
        }
        set
        {
            cuponField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public EntidadSucedidaType EntidadSucedida
    {
        get
        {
            return entidadSucedidaField;
        }
        set
        {
            entidadSucedidaField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FacturacionDispAdicionalSegundaYQuinta
    {
        get
        {
            return facturacionDispAdicionalSegundaYQuintaField;
        }
        set
        {
            facturacionDispAdicionalSegundaYQuintaField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "DetalleInmueble", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class DetalleInmuebleType
{

    private string situacionInmuebleField;

    private string referenciaCatastralField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SituacionInmueble
    {
        get
        {
            return situacionInmuebleField;
        }
        set
        {
            situacionInmuebleField = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReferenciaCatastral
    {
        get
        {
            return referenciaCatastralField;
        }
        set
        {
            referenciaCatastralField = value;
        }
    }
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "EntidadSucedida", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class EntidadSucedidaType
{

    private string nombreRazonField;

    private string nIFField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NombreRazon
    {
        get
        {
            return nombreRazonField;
        }
        set
        {
            nombreRazonField = value;
        }
    }

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
}

/// <remarks/>
[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(TypeName = "FacturasEmitidas", Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd")]
public class FacturasEmitidasType
{

    private FacturaEmitidaType[] facturaEmitidaField;

    /// <remarks/>
    [XmlElement("FacturaEmitida")]
    public FacturaEmitidaType[] FacturaEmitida
    {
        get
        {
            return facturaEmitidaField;
        }
        set
        {
            facturaEmitidaField = value;
        }
    }
}



