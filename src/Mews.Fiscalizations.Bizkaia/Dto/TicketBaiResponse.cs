using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Bizkaia.Dto
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
        "tidas_ConSG_AltaRespuesta_V1_0_1.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmi" +
        "tidas_ConSG_AltaRespuesta_V1_0_1.xsd", IsNullable = false)]
    public partial class LROEPJ240FacturasEmitidasConSGAltaRespuesta
    {

        private Cabecera cabeceraField;

        private DatosPresentacion datosPresentacionField;

        private RegistrosRegistro[] registrosField;

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
        public DatosPresentacion DatosPresentacion
        {
            get
            {
                return this.datosPresentacionField;
            }
            set
            {
                this.datosPresentacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Registro", IsNullable = false)]
        public RegistrosRegistro[] Registros
        {
            get
            {
                return this.registrosField;
            }
            set
            {
                this.registrosField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Cabecera
    {

        private byte modeloField;

        private byte capituloField;

        private decimal subcapituloField;

        private string operacionField;

        private decimal versionField;

        private byte ejercicioField;

        private CabeceraObligadoTributario obligadoTributarioField;

        /// <remarks/>
        public byte Modelo
        {
            get
            {
                return this.modeloField;
            }
            set
            {
                this.modeloField = value;
            }
        }

        /// <remarks/>
        public byte Capitulo
        {
            get
            {
                return this.capituloField;
            }
            set
            {
                this.capituloField = value;
            }
        }

        /// <remarks/>
        public decimal Subcapitulo
        {
            get
            {
                return this.subcapituloField;
            }
            set
            {
                this.subcapituloField = value;
            }
        }

        /// <remarks/>
        public string Operacion
        {
            get
            {
                return this.operacionField;
            }
            set
            {
                this.operacionField = value;
            }
        }

        /// <remarks/>
        public decimal Version
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

        /// <remarks/>
        public byte Ejercicio
        {
            get
            {
                return this.ejercicioField;
            }
            set
            {
                this.ejercicioField = value;
            }
        }

        /// <remarks/>
        public CabeceraObligadoTributario ObligadoTributario
        {
            get
            {
                return this.obligadoTributarioField;
            }
            set
            {
                this.obligadoTributarioField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CabeceraObligadoTributario
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
    public partial class DatosPresentacion
    {

        private string fechaPresentacionField;

        private string nIFPresentadorField;

        /// <remarks/>
        public string FechaPresentacion
        {
            get
            {
                return this.fechaPresentacionField;
            }
            set
            {
                this.fechaPresentacionField = value;
            }
        }

        /// <remarks/>
        public string NIFPresentador
        {
            get
            {
                return this.nIFPresentadorField;
            }
            set
            {
                this.nIFPresentadorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RegistrosRegistro
    {

        private RegistrosRegistroIdentificador identificadorField;

        private RegistrosRegistroSituacionRegistro situacionRegistroField;

        /// <remarks/>
        public RegistrosRegistroIdentificador Identificador
        {
            get
            {
                return this.identificadorField;
            }
            set
            {
                this.identificadorField = value;
            }
        }

        /// <remarks/>
        public RegistrosRegistroSituacionRegistro SituacionRegistro
        {
            get
            {
                return this.situacionRegistroField;
            }
            set
            {
                this.situacionRegistroField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RegistrosRegistroIdentificador
    {

        private RegistrosRegistroIdentificadorIDFactura iDFacturaField;

        /// <remarks/>
        public RegistrosRegistroIdentificadorIDFactura IDFactura
        {
            get
            {
                return this.iDFacturaField;
            }
            set
            {
                this.iDFacturaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RegistrosRegistroIdentificadorIDFactura
    {

        private string serieFacturaField;

        private string numFacturaField;

        private string fechaExpedicionFacturaField;

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
        public string NumFactura
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RegistrosRegistroSituacionRegistro
    {

        private string estadoRegistroField;

        private string codigoErrorRegistroField;

        private string descripcionErrorRegistroESField;

        private string descripcionErrorRegistroEUField;

        /// <remarks/>
        public string EstadoRegistro
        {
            get
            {
                return this.estadoRegistroField;
            }
            set
            {
                this.estadoRegistroField = value;
            }
        }

        /// <remarks/>
        public string CodigoErrorRegistro
        {
            get
            {
                return this.codigoErrorRegistroField;
            }
            set
            {
                this.codigoErrorRegistroField = value;
            }
        }

        /// <remarks/>
        public string DescripcionErrorRegistroES
        {
            get
            {
                return this.descripcionErrorRegistroESField;
            }
            set
            {
                this.descripcionErrorRegistroESField = value;
            }
        }

        /// <remarks/>
        public string DescripcionErrorRegistroEU
        {
            get
            {
                return this.descripcionErrorRegistroEUField;
            }
            set
            {
                this.descripcionErrorRegistroEUField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Registros
    {

        private RegistrosRegistro[] registroField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Registro")]
        public RegistrosRegistro[] Registro
        {
            get
            {
                return this.registroField;
            }
            set
            {
                this.registroField = value;
            }
        }
    }


}
