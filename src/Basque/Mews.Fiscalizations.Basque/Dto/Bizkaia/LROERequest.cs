﻿using System.Xml.Serialization;

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

    private Modelo240Enum modeloField;

    private CapituloModelo240Enum capituloField;

    private SubcapituloModelo240Enum subcapituloField;

    private bool subcapituloFieldSpecified;

    private OperacionEnum operacionField;

    private IDVersionEnum versionField;

    private int ejercicioField;

    private NIFPersonaType obligadoTributarioField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Modelo240Enum Modelo
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
    public CapituloModelo240Enum Capitulo
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
    public SubcapituloModelo240Enum Subcapitulo
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
    [XmlIgnore()]
    public bool SubcapituloSpecified
    {
        get
        {
            return subcapituloFieldSpecified;
        }
        set
        {
            subcapituloFieldSpecified = value;
        }
    }

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public OperacionEnum Operacion
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
    public IDVersionEnum Version
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
    public int Ejercicio
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
    public NIFPersonaType ObligadoTributario
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum Modelo240Enum
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("240")]
    Item240,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum CapituloModelo240Enum
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5")]
    Item5,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6")]
    Item6,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum SubcapituloModelo240Enum
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1.1")]
    Item11,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1.2")]
    Item12,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4.1")]
    Item41,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4.2")]
    Item42,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5.1")]
    Item51,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("5.2")]
    Item52,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6.1")]
    Item61,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6.2")]
    Item62,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("6.3")]
    Item63,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum OperacionEnum
{

    /// <remarks/>
    A00,

    /// <remarks/>
    A01,

    /// <remarks/>
    M00,

    /// <remarks/>
    M01,

    /// <remarks/>
    AN0,

    /// <remarks/>
    C00,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum IDVersionEnum
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1.0")]
    Item10,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class NIFPersonaType
{

    private string nIFField;

    private string apellidosNombreRazonSocialField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public partial class EntidadSucedidaType
{

    private string nombreRazonField;

    private string nIFField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public partial class DetalleInmuebleType
{

    private SituacionInmuebleEnum situacionInmuebleField;

    private string referenciaCatastralField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SituacionInmuebleEnum SituacionInmueble
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum SituacionInmuebleEnum
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("1")]
    Item1,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("2")]
    Item2,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("3")]
    Item3,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("4")]
    Item4,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class OtraInformacionTrascendenciaTributariaType
{
    private string nIFRepresentanteDeclaradoField;

    private DetalleInmuebleType[] inmueblesField;

    private string importeTransmisionInmueblesSujetoAIVAField;

    private string numRegistroAcuerdoFacturacionField;

    private string referenciaExternaField;

    private SiNoEnum cuponField;

    private bool cuponFieldSpecified;

    private EntidadSucedidaType entidadSucedidaField;

    private SiNoEnum facturacionDispAdicionalSegundaYQuintaField;

    private bool facturacionDispAdicionalSegundaYQuintaFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("DetalleInmueble", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SiNoEnum Cupon
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
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CuponSpecified
    {
        get
        {
            return cuponFieldSpecified;
        }
        set
        {
            cuponFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SiNoEnum FacturacionDispAdicionalSegundaYQuinta
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

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool FacturacionDispAdicionalSegundaYQuintaSpecified
    {
        get
        {
            return facturacionDispAdicionalSegundaYQuintaFieldSpecified;
        }
        set
        {
            facturacionDispAdicionalSegundaYQuintaFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum SiNoEnum
{

    /// <remarks/>
    S,

    /// <remarks/>
    N,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class DetalleEmitidaConSGCodificadoType
{

    private byte[] ticketBaiField;

    private OtraInformacionTrascendenciaTributariaType otraInformacionTrascendenciaTributariaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary")]
    public byte[] TicketBai
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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


