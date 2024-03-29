﻿namespace Mews.Fiscalizations.Basque.Dto.Bizkaia;

using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.CodeDom.Compiler;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
                                                                    "tidas_ConSG_AltaRespuesta_V1_0_1.xsd")]
[XmlRoot(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
                                              "tidas_ConSG_AltaRespuesta_V1_0_1.xsd", IsNullable = false)]
public class LROEPJ240FacturasEmitidasConSGAltaRespuesta
{

    private Cabecera240Type cabeceraField;

    private DatosPresentacionType datosPresentacionField;

    private RegistroFacturaConSGType[] registrosField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlArrayItem("Registro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
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
    [XmlIgnore]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class SituacionRegistroType
{

    private EstadoRegistroEnum estadoRegistroField;

    private string codigoErrorRegistroField;

    private string descripcionErrorRegistroESField;

    private string descripcionErrorRegistroEUField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd")]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class IDFacturaType
{

    private string serieFacturaField;

    private string numFacturaField;

    private string fechaExpedicionFacturaField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class IdentificadorFacturaConSGType
{

    private object itemField;

    /// <remarks/>
    [XmlElement("IDFactura", typeof(IDFacturaType), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [XmlElement("TicketBai", typeof(byte[]), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary")]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class RegistroFacturaConSGType
{

    private IdentificadorFacturaConSGType identificadorField;

    private SituacionRegistroType situacionRegistroField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[GeneratedCode("xsd", "4.8.3928.0")]
[Serializable]
[System.Diagnostics.DebuggerStepThroughAttribute]
[DesignerCategory("code")]
[XmlType(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd")]
public class DatosPresentacionType
{

    private string fechaPresentacionField;

    private string nIFPresentadorField;

    /// <remarks/>
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
    [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
