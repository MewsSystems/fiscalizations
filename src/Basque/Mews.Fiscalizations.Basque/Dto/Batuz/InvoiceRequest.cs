using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Basque.Dto.Batuz
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ticketbai:emision")]
    [System.Xml.Serialization.XmlRoot(Namespace = "urn:ticketbai:emision", IsNullable = false)]
    public partial class TicketBai
    {

        private Cabecera cabeceraField;

        private Sujetos sujetosField;

        private Factura facturaField;

        private HuellaTBAI huellaTBAIField;

        private Signature signatureField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public Cabecera Cabecera
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
        [System.Xml.Serialization.XmlElement(Namespace = "")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Cabecera
    {

        private decimal iDVersionTBAIField;

        /// <remarks/>
        public decimal IDVersionTBAI
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
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
                return emisorField;
            }
            set
            {
                emisorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("IDDestinatario", IsNullable = false)]
        public SujetosIDDestinatario[] Destinatarios
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
        public string VariosDestinatarios
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
        public string EmitidaPorTercerosODestinatario
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
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class SujetosEmisor
    {

        private string nIFField;

        private string apellidosNombreRazonSocialField;

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

        /// <remarks/>
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return nIFField;
            }
            set
            {
                nIFField = value;
            }
        }

        /// <remarks/>
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

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
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
                return cabeceraFacturaField;
            }
            set
            {
                cabeceraFacturaField = value;
            }
        }

        /// <remarks/>
        public FacturaDatosFactura DatosFactura
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
        public FacturaTipoDesglose TipoDesglose
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
        public FacturaCabeceraFacturaFacturaRectificativa FacturaRectificativa
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
        [System.Xml.Serialization.XmlArrayItem("IDFacturaRectificadaSustituida", IsNullable = false)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return codigoField;
            }
            set
            {
                codigoField = value;
            }
        }

        /// <remarks/>
        public string Tipo
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
        public FacturaCabeceraFacturaFacturaRectificativaImporteRectificacionSustitutiva ImporteRectificacionSustitutiva
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return baseRectificadaField;
            }
            set
            {
                baseRectificadaField = value;
            }
        }

        /// <remarks/>
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return fechaOperacionField;
            }
            set
            {
                fechaOperacionField = value;
            }
        }

        /// <remarks/>
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
        [System.Xml.Serialization.XmlArrayItem("IDDetalleFactura", IsNullable = false)]
        public FacturaDatosFacturaIDDetalleFactura[] DetallesFactura
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
        [System.Xml.Serialization.XmlArrayItem("IDClave", IsNullable = false)]
        public FacturaDatosFacturaIDClave[] Claves
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return descripcionDetalleField;
            }
            set
            {
                descripcionDetalleField = value;
            }
        }

        /// <remarks/>
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class FacturaDatosFacturaIDClave
    {

        private byte claveRegimenIvaOpTrascendenciaField;

        /// <remarks/>
        public byte ClaveRegimenIvaOpTrascendencia
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class FacturaTipoDesglose
    {

        private FacturaTipoDesgloseDesgloseFactura desgloseFacturaField;

        /// <remarks/>
        public FacturaTipoDesgloseDesgloseFactura DesgloseFactura
        {
            get
            {
                return desgloseFacturaField;
            }
            set
            {
                desgloseFacturaField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFactura
    {

        private FacturaTipoDesgloseDesgloseFacturaSujeta sujetaField;

        private FacturaTipoDesgloseDesgloseFacturaDetalleNoSujeta[] noSujetaField;

        /// <remarks/>
        public FacturaTipoDesgloseDesgloseFacturaSujeta Sujeta
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
        [System.Xml.Serialization.XmlArrayItem("DetalleNoSujeta", IsNullable = false)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFacturaSujeta
    {

        private FacturaTipoDesgloseDesgloseFacturaSujetaDetalleExenta[] exentaField;

        private FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExenta[] noExentaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("DetalleExenta", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaSujetaDetalleExenta[] Exenta
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
        [System.Xml.Serialization.XmlArrayItem("DetalleNoExenta", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExenta[] NoExenta
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFacturaSujetaDetalleExenta
    {

        private string causaExencionField;

        private string baseImponibleField;

        /// <remarks/>
        public string CausaExencion
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExenta
    {

        private string tipoNoExentaField;

        private FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExentaDetalleIVA[] desgloseIVAField;

        /// <remarks/>
        public string TipoNoExenta
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
        [System.Xml.Serialization.XmlArrayItem("DetalleIVA", IsNullable = false)]
        public FacturaTipoDesgloseDesgloseFacturaSujetaDetalleNoExentaDetalleIVA[] DesgloseIVA
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return baseImponibleField;
            }
            set
            {
                baseImponibleField = value;
            }
        }

        /// <remarks/>
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
        public string OperacionEnRecargoDeEquivalenciaORegimenSimplificado
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
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
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
        [System.Xml.Serialization.XmlElement("Object")]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
        [System.Xml.Serialization.XmlElement("Reference")]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoCanonicalizationMethod
    {

        private string algorithmField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlText()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
        [System.Xml.Serialization.XmlText()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReference
    {

        private SignatureSignedInfoReferenceTransform[] transformsField;

        private SignatureSignedInfoReferenceDigestMethod digestMethodField;

        private string digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Transform", IsNullable = false)]
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
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReferenceTransform
    {

        private string[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private string[] textField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("XPath", typeof(string))]
        [System.Xml.Serialization.XmlElement("any_element", typeof(string), Namespace = "otherNS")]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
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
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
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
        [System.Xml.Serialization.XmlText()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        XPath,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("otherNS:any_element")]
        any_element,
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignedInfoReferenceDigestMethod
    {

        private string algorithmField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlText()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureSignatureValue
    {

        private string idField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlText()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
        [System.Xml.Serialization.XmlText()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
        [System.Xml.Serialization.XmlText()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureKeyInfoRetrievalMethod
    {

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureObject
    {

        private string[] any_elementField;

        private string[] textField;

        private string idField;

        private string mimeTypeField;

        private string encodingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("any_element", Namespace = "urn:ticketbai:emision")]
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
        [System.Xml.Serialization.XmlText()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlAttribute()]
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
        [System.Xml.Serialization.XmlAttribute()]
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


}
