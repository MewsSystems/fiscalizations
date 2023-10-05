﻿using System.Xml.Serialization;

namespace Mews.Fiscalizations.Basque.Dto.Bizkaia;


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaRespuesta_V1_0_1.xsd")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
    "tidas_ConSG_AltaRespuesta_V1_0_1.xsd", IsNullable = false)]
public class LROEPJ240FacturasEmitidasConSGAltaRespuesta
{

    private Cabecera240Type cabeceraField;

    private DatosPresentacionType datosPresentacionField;

    private RegistroFacturaConSGType[] registrosField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Cabecera240Type Cabecera
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DatosPresentacionType DatosPresentacion
    {
        get
        {
            return datosPresentacionField;
        }
        set
        {
            datosPresentacionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Registro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public RegistroFacturaConSGType[] Registros
    {
        get
        {
            return registrosField;
        }
        set
        {
            registrosField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class Cabecera240Type
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlIgnoreAttribute()]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class SituacionRegistroType
{

    private EstadoRegistroEnum estadoRegistroField;

    private string codigoErrorRegistroField;

    private string descripcionErrorRegistroESField;

    private string descripcionErrorRegistroEUField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public EstadoRegistroEnum EstadoRegistro
    {
        get
        {
            return estadoRegistroField;
        }
        set
        {
            estadoRegistroField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CodigoErrorRegistro
    {
        get
        {
            return codigoErrorRegistroField;
        }
        set
        {
            codigoErrorRegistroField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DescripcionErrorRegistroES
    {
        get
        {
            return descripcionErrorRegistroESField;
        }
        set
        {
            descripcionErrorRegistroESField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DescripcionErrorRegistroEU
    {
        get
        {
            return descripcionErrorRegistroEUField;
        }
        set
        {
            descripcionErrorRegistroEUField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
public enum EstadoRegistroEnum
{

    /// <remarks/>
    Correcto,

    /// <remarks/>
    AceptadoConErrores,

    /// <remarks/>
    Anulado,

    /// <remarks/>
    Incorrecto,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class IDFacturaType
{

    private string serieFacturaField;

    private string numFacturaField;

    private string fechaExpedicionFacturaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class IdentificadorFacturaConSGType
{

    private object itemField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("IDFactura", typeof(IDFacturaType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("TicketBai", typeof(byte[]), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary")]
    public object Item
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class RegistroFacturaConSGType
{

    private IdentificadorFacturaConSGType identificadorField;

    private SituacionRegistroType situacionRegistroField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public IdentificadorFacturaConSGType Identificador
    {
        get
        {
            return identificadorField;
        }
        set
        {
            identificadorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SituacionRegistroType SituacionRegistro
    {
        get
        {
            return situacionRegistroField;
        }
        set
        {
            situacionRegistroField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class DatosPresentacionType
{

    private string fechaPresentacionField;

    private string nIFPresentadorField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FechaPresentacion
    {
        get
        {
            return fechaPresentacionField;
        }
        set
        {
            fechaPresentacionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NIFPresentador
    {
        get
        {
            return nIFPresentadorField;
        }
        set
        {
            nIFPresentadorField = value;
        }
    }
}
