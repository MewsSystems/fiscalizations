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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ticketbai:emision")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:ticketbai:emision", IsNullable = false)]
    public partial class TicketBai
    {

        private Cabecera cabeceraField;

        private Sujetos sujetosField;

        private Factura facturaField;

        private HuellaTBAI huellaTBAIField;

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
        public HuellaTBAI HuellaTBAI
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
    public partial class Cabecera
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
    public partial class Sujetos
    {

        private SujetosEmisor emisorField;

        private SujetosIDDestinatario[] destinatariosField;

        private string variosDestinatariosField;

        private string emitidaPorTercerosODestinatarioField;

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
        [System.Xml.Serialization.XmlArrayItemAttribute("IDDestinatario", IsNullable = false)]
        public SujetosIDDestinatario[] Destinatarios
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

        /// <remarks/>
        public string VariosDestinatarios
        {
            get
            {
                return this.variosDestinatariosField;
            }
            set
            {
                this.variosDestinatariosField = value;
            }
        }

        /// <remarks/>
        public string EmitidaPorTercerosODestinatario
        {
            get
            {
                return this.emitidaPorTercerosODestinatarioField;
            }
            set
            {
                this.emitidaPorTercerosODestinatarioField = value;
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
    public partial class SujetosIDDestinatario
    {

        private string nIFField;

        private string apellidosNombreRazonSocialField;

        private string codigoPostalField;

        private string direccionField;

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

        /// <remarks/>
        public string CodigoPostal
        {
            get
            {
                return this.codigoPostalField;
            }
            set
            {
                this.codigoPostalField = value;
            }
        }

        /// <remarks/>
        public string Direccion
        {
            get
            {
                return this.direccionField;
            }
            set
            {
                this.direccionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Factura
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

        private string numFacturaField;

        private string fechaExpedicionFacturaField;

        private string horaExpedicionFacturaField;

        private string facturaSimplificadaField;

        private string facturaEmitidaSustitucionSimplificadaField;

        private FacturaCabeceraFacturaFacturaRectificativa facturaRectificativaField;

        private FacturaCabeceraFacturaIDFacturaRectificadaSustituida[] facturasRectificadasSustituidasField;

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

        /// <remarks/>
        public string HoraExpedicionFactura
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

        /// <remarks/>
        public string FacturaSimplificada
        {
            get
            {
                return this.facturaSimplificadaField;
            }
            set
            {
                this.facturaSimplificadaField = value;
            }
        }

        /// <remarks/>
        public string FacturaEmitidaSustitucionSimplificada
        {
            get
            {
                return this.facturaEmitidaSustitucionSimplificadaField;
            }
            set
            {
                this.facturaEmitidaSustitucionSimplificadaField = value;
            }
        }

        /// <remarks/>
        public FacturaCabeceraFacturaFacturaRectificativa FacturaRectificativa
        {
            get
            {
                return this.facturaRectificativaField;
            }
            set
            {
                this.facturaRectificativaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("IDFacturaRectificadaSustituida", IsNullable = false)]
        public FacturaCabeceraFacturaIDFacturaRectificadaSustituida[] FacturasRectificadasSustituidas
        {
            get
            {
                return this.facturasRectificadasSustituidasField;
            }
            set
            {
                this.facturasRectificadasSustituidasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FacturaCabeceraFacturaFacturaRectificativa
    {

        private string codigoField;

        private string tipoField;

        private FacturaCabeceraFacturaFacturaRectificativaImporteRectificacionSustitutiva importeRectificacionSustitutivaField;

        /// <remarks/>
        public string Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        public string Tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }

        /// <remarks/>
        public FacturaCabeceraFacturaFacturaRectificativaImporteRectificacionSustitutiva ImporteRectificacionSustitutiva
        {
            get
            {
                return this.importeRectificacionSustitutivaField;
            }
            set
            {
                this.importeRectificacionSustitutivaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FacturaCabeceraFacturaFacturaRectificativaImporteRectificacionSustitutiva
    {

        private string baseRectificadaField;

        private string cuotaRectificadaField;

        private string cuotaRecargoRectificadaField;

        /// <remarks/>
        public string BaseRectificada
        {
            get
            {
                return this.baseRectificadaField;
            }
            set
            {
                this.baseRectificadaField = value;
            }
        }

        /// <remarks/>
        public string CuotaRectificada
        {
            get
            {
                return this.cuotaRectificadaField;
            }
            set
            {
                this.cuotaRectificadaField = value;
            }
        }

        /// <remarks/>
        public string CuotaRecargoRectificada
        {
            get
            {
                return this.cuotaRecargoRectificadaField;
            }
            set
            {
                this.cuotaRecargoRectificadaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
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
    public partial class FacturaDatosFactura
    {

        private string fechaOperacionField;

        private string descripcionFacturaField;

        private FacturaDatosFacturaIDDetalleFactura[] detallesFacturaField;

        private string importeTotalFacturaField;

        private string retencionSoportadaField;

        private string baseImponibleACosteField;

        private FacturaDatosFacturaIDClave[] clavesField;

        /// <remarks/>
        public string FechaOperacion
        {
            get
            {
                return this.fechaOperacionField;
            }
            set
            {
                this.fechaOperacionField = value;
            }
        }

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
        [System.Xml.Serialization.XmlArrayItemAttribute("IDDetalleFactura", IsNullable = false)]
        public FacturaDatosFacturaIDDetalleFactura[] DetallesFactura
        {
            get
            {
                return this.detallesFacturaField;
            }
            set
            {
                this.detallesFacturaField = value;
            }
        }

        /// <remarks/>
        public string ImporteTotalFactura
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
        public string RetencionSoportada
        {
            get
            {
                return this.retencionSoportadaField;
            }
            set
            {
                this.retencionSoportadaField = value;
            }
        }

        /// <remarks/>
        public string BaseImponibleACoste
        {
            get
            {
                return this.baseImponibleACosteField;
            }
            set
            {
                this.baseImponibleACosteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("IDClave", IsNullable = false)]
        public FacturaDatosFacturaIDClave[] Claves
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
    public partial class FacturaDatosFacturaIDDetalleFactura
    {

        private string descripcionDetalleField;

        private string cantidadField;

        private string importeUnitarioField;

        private string descuentoField;

        private string importeTotalField;

        /// <remarks/>
        public string DescripcionDetalle
        {
            get
            {
                return this.descripcionDetalleField;
            }
            set
            {
                this.descripcionDetalleField = value;
            }
        }

        /// <remarks/>
        public string Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <remarks/>
        public string ImporteUnitario
        {
            get
            {
                return this.importeUnitarioField;
            }
            set
            {
                this.importeUnitarioField = value;
            }
        }

        /// <remarks/>
        public string Descuento
        {
            get
            {
                return this.descuentoField;
            }
            set
            {
                this.descuentoField = value;
            }
        }

        /// <remarks/>
        public string ImporteTotal
        {
            get
            {
                return this.importeTotalField;
            }
            set
            {
                this.importeTotalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FacturaDatosFacturaIDClave
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

        private FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta[] noSujetaField;

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

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleNoSujeta", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta[] NoSujeta
        {
            get
            {
                return this.noSujetaField;
            }
            set
            {
                this.noSujetaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFacturaSujeta
    {

        private FacturaTipoDesgloseDesgloseFacturaSujetaDetalleExenta[] exentaField;

        private FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExenta[] noExentaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleExenta", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaSujetaDetalleExenta[] Exenta
        {
            get
            {
                return this.exentaField;
            }
            set
            {
                this.exentaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleNoExenta", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExenta[] NoExenta
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
    public partial class FacturaTipoDesgloseDesgloseFacturaSujetaDetalleExenta
    {

        private string causaExencionField;

        private string baseImponibleField;

        /// <remarks/>
        public string CausaExencion
        {
            get
            {
                return this.causaExencionField;
            }
            set
            {
                this.causaExencionField = value;
            }
        }

        /// <remarks/>
        public string BaseImponible
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExenta
    {

        private string tipoNoExentaField;

        private FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExentaDetalleIVA[] desgloseIVAField;

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
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleIVA", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExentaDetalleIVA[] DesgloseIVA
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
    public partial class FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExentaDetalleIVA
    {

        private string baseImponibleField;

        private string tipoImpositivoField;

        private string cuotaImpuestoField;

        private string tipoRecargoEquivalenciaField;

        private string cuotaRecargoEquivalenciaField;

        private string operacionEnRecargoDeEquivalenciaORegimenSimplificadoField;

        /// <remarks/>
        public string BaseImponible
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
        public string TipoImpositivo
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
        public string CuotaImpuesto
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

        /// <remarks/>
        public string TipoRecargoEquivalencia
        {
            get
            {
                return this.tipoRecargoEquivalenciaField;
            }
            set
            {
                this.tipoRecargoEquivalenciaField = value;
            }
        }

        /// <remarks/>
        public string CuotaRecargoEquivalencia
        {
            get
            {
                return this.cuotaRecargoEquivalenciaField;
            }
            set
            {
                this.cuotaRecargoEquivalenciaField = value;
            }
        }

        /// <remarks/>
        public string OperacionEnRecargoDeEquivalenciaORegimenSimplificado
        {
            get
            {
                return this.operacionEnRecargoDeEquivalenciaORegimenSimplificadoField;
            }
            set
            {
                this.operacionEnRecargoDeEquivalenciaORegimenSimplificadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta
    {

        private string causaField;

        private string importeField;

        /// <remarks/>
        public string Causa
        {
            get
            {
                return this.causaField;
            }
            set
            {
                this.causaField = value;
            }
        }

        /// <remarks/>
        public string Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
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

        private string numFacturaAnteriorField;

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
        public string NumFacturaAnterior
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
        [System.Xml.Serialization.XmlElementAttribute("Object")]
        public SignatureObject[] Object
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

        private string idField;

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
    public partial class SignatureSignedInfoCanonicalizationMethod
    {

        private string algorithmField;

        private string valueField;

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
                return this.hMACOutputLengthField;
            }
            set
            {
                this.hMACOutputLengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
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
    public partial class SignatureSignedInfoReference
    {

        private SignatureSignedInfoReferenceTransform[] transformsField;

        private SignatureSignedInfoReferenceDigestMethod digestMethodField;

        private string digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReferenceTransform
    {

        private string[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private string[] textField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XPath", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("any_element", typeof(string), Namespace = "otherNS")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        XPath,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("otherNS:any_element")]
        any_element,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReferenceDigestMethod
    {

        private string algorithmField;

        private string valueField;

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
                return this.keyNameField;
            }
            set
            {
                this.keyNameField = value;
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
        public SignatureKeyInfoRetrievalMethod RetrievalMethod
        {
            get
            {
                return this.retrievalMethodField;
            }
            set
            {
                this.retrievalMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
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
    public partial class SignatureKeyInfoKeyValue
    {

        private SignatureKeyInfoKeyValueDSAKeyValue dSAKeyValueField;

        private string[] textField;

        /// <remarks/>
        public SignatureKeyInfoKeyValueDSAKeyValue DSAKeyValue
        {
            get
            {
                return this.dSAKeyValueField;
            }
            set
            {
                this.dSAKeyValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
                return this.pField;
            }
            set
            {
                this.pField = value;
            }
        }

        /// <remarks/>
        public string Q
        {
            get
            {
                return this.qField;
            }
            set
            {
                this.qField = value;
            }
        }

        /// <remarks/>
        public string G
        {
            get
            {
                return this.gField;
            }
            set
            {
                this.gField = value;
            }
        }

        /// <remarks/>
        public string Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        public string J
        {
            get
            {
                return this.jField;
            }
            set
            {
                this.jField = value;
            }
        }

        /// <remarks/>
        public string Seed
        {
            get
            {
                return this.seedField;
            }
            set
            {
                this.seedField = value;
            }
        }

        /// <remarks/>
        public string PgenCounter
        {
            get
            {
                return this.pgenCounterField;
            }
            set
            {
                this.pgenCounterField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureKeyInfoRetrievalMethod
    {

        private string uRIField;

        private string typeField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureObject
    {

        private string[] any_elementField;

        private string[] textField;

        private string idField;

        private string mimeTypeField;

        private string encodingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("any_element", Namespace = "urn:ticketbai:emision")]
        public string[] any_element
        {
            get
            {
                return this.any_elementField;
            }
            set
            {
                this.any_elementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Encoding
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
    }


}
