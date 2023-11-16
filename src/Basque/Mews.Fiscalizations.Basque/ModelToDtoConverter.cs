using Mews.Fiscalizations.Basque.Model;
using System.Globalization;

namespace Mews.Fiscalizations.Basque;

internal static class ModelToDtoConverter
{
    private static readonly CultureInfo CultureInfo = CultureInfo.InvariantCulture;

    public static Dto.TicketBai Convert(SendInvoiceRequest sendInvoiceRequest, ServiceInfo serviceInfo)
    {
        return new Dto.TicketBai
        {
            Cabecera = new Dto.Cabecera1
            {
                IDVersionTBAI = serviceInfo.Version
            },
            Sujetos = Convert(sendInvoiceRequest.Subject),
            Factura = Convert(sendInvoiceRequest.Invoice),
            HuellaTBAI = Convert(sendInvoiceRequest.InvoiceFooter),
            Signature = null // TODO: Convert(sendInvoiceRequest.Signature)
        };
    }

    private static Dto.Sujetos Convert(Subject subject)
    {
        return new Dto.Sujetos
        {
            Emisor = Convert(subject.Issuer),
            Destinatarios = subject.Receivers.Select(r => Convert(r)).ToArray(),
            VariosDestinatariosSpecified = true,
            VariosDestinatarios = subject.MultipleReceivers.Match(
                t => Dto.SiNoType.S,
                f => Dto.SiNoType.N
            ),
            EmitidaPorTercerosODestinatario = subject.IssuerType.Map(t => Convert(t)).ToNullable(),
            EmitidaPorTercerosODestinatarioSpecified = subject.IssuerType.NonEmpty
        };
    }

    private static Dto.Emisor1 Convert(Issuer issuer)
    {
        return new Dto.Emisor1
        {
            NIF = issuer.Nif.TaxpayerNumber,
            ApellidosNombreRazonSocial = issuer.Name.Value
        };
    }

    private static Dto.EmitidaPorTercerosType Convert(IssuerType type)
    {
        return type.Match(
            IssuerType.IssuedByIssuer, _ => Dto.EmitidaPorTercerosType.N,
            IssuerType.IssuedByThirdParty, _ => Dto.EmitidaPorTercerosType.T,
            IssuerType.IssuedByTransactionRecipient, _ => Dto.EmitidaPorTercerosType.D
        );
    }

    private static Dto.IDDestinatario Convert(Receiver receiver)
    {
        var name = receiver.Match(
            local => local.Name,
            foreign => foreign.Name
        );
        var address = receiver.Match(
            local => local.Address,
            foreign => foreign.Address
        );
        var postalCode = receiver.Match(
            local => local.PostalCode,
            foreign => foreign.PostalCode
        );
        return new Dto.IDDestinatario
        {
            ApellidosNombreRazonSocial = name.Value,
            CodigoPostal = postalCode.Value,
            Direccion = address.Value,
            Item = receiver.Match(
                local => (object)local.TaxpayerIdentificationNumber.TaxpayerNumber,
                foreign => new Dto.IDOtro1
                {
                    CodigoPais = CovertCountryType21(foreign.Country),
                    CodigoPaisSpecified = true,
                    ID = foreign.Id.Value,
                    IDType = Convert1(foreign.IdType)
                }
            )
        };
    }

    private static Dto.HuellaTBAI1 Convert(InvoiceFooter footer)
    {
        return new Dto.HuellaTBAI1
        {
            EncadenamientoFacturaAnterior = footer.OriginalInvoiceInfo.Map(i => Convert(i)).GetOrNull(),
            Software = Convert(footer.Software),
            NumSerieDispositivo = footer.DeviceSerialNumber.Map(n => n.Value).GetOrNull()
        };
    }

    private static Dto.EncadenamientoFacturaAnteriorType Convert(OriginalInvoiceInfo originalInvoiceInfo)
    {
        return new Dto.EncadenamientoFacturaAnteriorType
        {
            SerieFacturaAnterior = originalInvoiceInfo.Series.Map(s => s.Value).GetOrNull(),
            NumFacturaAnterior = originalInvoiceInfo.Number.Value,
            FechaExpedicionFacturaAnterior = Convert(originalInvoiceInfo.IssueDate),
            SignatureValueFirmaFacturaAnterior = originalInvoiceInfo.Signature.Value
        };
    }

    private static Dto.SoftwareFacturacionType1 Convert(Software software)
    {
        return new Dto.SoftwareFacturacionType1
        {
            LicenciaTBAI = software.License.Value,
            Version = software.Version.Value,
            Nombre = software.Name.Value,
            EntidadDesarrolladora = Convert(software.Developer)
        };
    }

    private static Dto.EntidadDesarrolladoraType1 Convert(SoftwareDeveloper developer)
    {
        return developer.Match(
            local => new Dto.EntidadDesarrolladoraType1
            {
                Item = local.Nif.TaxpayerNumber
            },
            foreign => new Dto.EntidadDesarrolladoraType1
            {
                Item = new Dto.IDOtro
                {
                    CodigoPaisSpecified = foreign.Country.NonEmpty,
                    CodigoPais = foreign.Country.Map(c => ConvertCountryType2(c)).ToNullable(),
                    ID = foreign.Id.Value,
                    IDType = Convert(foreign.IdType)
                }
            }
        );
    }

    private static Dto.CountryType2 ConvertCountryType2(Country country)
    {
        // TicketBai country list Dto doesn't recognize Kosovo country code, so its being reported as Serbia.
        if (country.Equals(Countries.Kosovo))
        {
            return Dto.CountryType2.RS;
        }

        var result = country.Alpha2Code.ToEnum<Dto.CountryType2>();
        return result.Get();
    }

    private static Dto.CountryType21 CovertCountryType21(Country country)
    {
        // TicketBai country list Dto doesn't recognize Kosovo country code, so its being reported as Serbia.
        if (country.Equals(Countries.Kosovo))
        {
            return Dto.CountryType21.RS;
        }

        var result = country.Alpha2Code.ToEnum<Dto.CountryType21>();
        return result.Get();
    }

    private static Dto.IDTypeType Convert(IdType type)
    {
        return type.Match(
            IdType.NifVat, _ => Dto.IDTypeType.Item02,
            IdType.Passport, _ => Dto.IDTypeType.Item03,
            IdType.OfficialIdentificationDocumentIssuedByTheCountry, _ => Dto.IDTypeType.Item04,
            IdType.ResidenceCertificate, _ => Dto.IDTypeType.Item05,
            IdType.OtherSupportingDocument, _ => Dto.IDTypeType.Item06
        );
    }

    private static Dto.IDTypeType1 Convert1(IdType type)
    {
        return type.Match(
            IdType.NifVat, _ => Dto.IDTypeType1.Item02,
            IdType.Passport, _ => Dto.IDTypeType1.Item03,
            IdType.OfficialIdentificationDocumentIssuedByTheCountry, _ => Dto.IDTypeType1.Item04,
            IdType.ResidenceCertificate, _ => Dto.IDTypeType1.Item05,
            IdType.OtherSupportingDocument, _ => Dto.IDTypeType1.Item06
        );
    }

    private static Dto.Factura Convert(Invoice invoice)
    {
        return new Dto.Factura
        {
            CabeceraFactura = Convert(invoice.Header),
            DatosFactura = Convert(invoice.InvoiceData),
            TipoDesglose = Convert(invoice.TaxBreakdown)
        };
    }

    private static Dto.DatosFacturaType Convert(InvoiceData data)
    {
        return new Dto.DatosFacturaType
        {
            FechaOperacion = data.TransactionDate.Map(d => Convert(d)).GetOrNull(),
            DescripcionFactura = data.Description.Value,
            DetallesFactura = data.Items.Select(i => Convert(i)).ToArray(),
            ImporteTotalFactura = data.TotalAmount.ToString(CultureInfo),
            RetencionSoportada = data.SupportWithheldAmount.Map(a => a.ToString(CultureInfo)).GetOrNull(),
            BaseImponibleACoste = data.Tax.Map(t => t.ToString(CultureInfo)).GetOrNull(),
            Claves = data.TaxModes.Select(t => Convert(t)).ToArray()
        };
    }

    private static Dto.IDDetalleFacturaType Convert(InvoiceItem item)
    {
        return new Dto.IDDetalleFacturaType
        {
            DescripcionDetalle = item.Description.Value,
            Cantidad = item.Quantity.ToString(CultureInfo),
            ImporteUnitario = item.UnitAmount.ToString(CultureInfo),
            Descuento = item.Discount.Map(d => d.ToString(CultureInfo)).GetOrNull(),
            ImporteTotal = item.TotalAmount.ToString(CultureInfo)
        };
    }

    private static Dto.IDClaveType Convert(TaxMode taxMode)
    {
        return new Dto.IDClaveType
        {
            ClaveRegimenIvaOpTrascendencia = taxMode.Match(
                TaxMode.GeneralTaxRegimeActivity, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item01,
                TaxMode.Export, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item02,
                TaxMode.WorksOfArt, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item03,
                TaxMode.InvestmentGold, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item04,
                TaxMode.TravelAgencies, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item05,
                TaxMode.GroupsOfEntities, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item06,
                TaxMode.CashBasis, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item07,
                TaxMode.CanaryIslandsGeneralIndirectTax, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item08,
                TaxMode.TravelAgencyServicesActingAsIntermediaries, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item09,
                TaxMode.Collections, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item10,
                TaxMode.BusinessPremisesLeaseActivities1, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item11,
                TaxMode.BusinessPremisesLeaseActivities2, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item12,
                TaxMode.BusinessPremisesLeaseActivities3, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item13,
                TaxMode.InvoiceWithVATPendingAccrual1, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item14,
                TaxMode.InvoiceWithVATPendingAccrual2, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item15,
                TaxMode.SurchargeOperations, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item51,
                TaxMode.SimplifiedOperations, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item52,
                TaxMode.NotVatEntity, _ => Dto.IdOperacionesTrascendenciaTributariaType.Item53
            )
        };
    }

    private static Dto.CabeceraFacturaType1 Convert(InvoiceHeader header)
    {
        return new Dto.CabeceraFacturaType1
        {
            SerieFactura = header.Series.Map(s => s.Value).GetOrNull(),
            NumFactura = header.Number.Value,
            FechaExpedicionFactura = Convert(header.Issued.Date),
            HoraExpedicionFactura = header.Issued.ToString("HH:MM:ss"),
            FacturaSimplificada = header.IsSimplified.Map(i => i.Match(
                t => Dto.SiNoType.S,
                f => Dto.SiNoType.N
            )).GetOrElse(Dto.SiNoType.N),
            FacturaSimplificadaSpecified = header.IsSimplified.NonEmpty,
            FacturaEmitidaSustitucionSimplificada = header.IssuedInSubstitutionOfSimplifiedInvoice.Map(i => i.Match(
                t => Dto.SiNoType.S,
                f => Dto.SiNoType.N
            )).GetOrElse(Dto.SiNoType.N),
            FacturaEmitidaSustitucionSimplificadaSpecified = header.IssuedInSubstitutionOfSimplifiedInvoice.NonEmpty,
            FacturaRectificativa = header.CorrectingInvoice.Map(i => Convert(i)).GetOrNull(),
            FacturasRectificadasSustituidas = header.CorrectedInvoices.Map(invoices => invoices.Select(i => Convert(i)).ToArray()).GetOrEmpty()
        };
    }

    private static Dto.IDFacturaRectificadaSustituidaType Convert(CorrectedInvoice correctedInvoice)
    {
        return new Dto.IDFacturaRectificadaSustituidaType
        {
            SerieFactura = correctedInvoice.Series.Value,
            NumFactura = correctedInvoice.Number.Value,
            FechaExpedicionFactura = Convert(correctedInvoice.IssueDate)
        };
    }

    private static Dto.FacturaRectificativaType Convert(CorrectingInvoice correctiveInvoice)
    {
        return new Dto.FacturaRectificativaType
        {
            Codigo = Convert(correctiveInvoice.Code),
            Tipo = Convert(correctiveInvoice.Type),
            ImporteRectificacionSustitutiva = correctiveInvoice.Amount.Map(a => new Dto.ImporteRectificacionSustitutivaType
            {
                BaseRectificada = a.Amount.ToString(CultureInfo),
                CuotaRecargoRectificada = a.Surcharge.ToString(CultureInfo),
                CuotaRectificada = a.Fee.ToString(CultureInfo)
            }).GetOrNull()
        };
    }

    private static Dto.ClaveTipoFacturaType Convert(CorrectingInvoiceCode code)
    {
        return code.Match(
            CorrectingInvoiceCode.CorrectedInvoice, _ => Dto.ClaveTipoFacturaType.R1,
            CorrectingInvoiceCode.CorrectedInvoice2, _ => Dto.ClaveTipoFacturaType.R2,
            CorrectingInvoiceCode.CorrectedInvoice3, _ => Dto.ClaveTipoFacturaType.R3,
            CorrectingInvoiceCode.CorrectedInvoice4, _ => Dto.ClaveTipoFacturaType.R4,
            CorrectingInvoiceCode.CorrectedInvoiceInSimplifiedInvoices, _ => Dto.ClaveTipoFacturaType.R5
        );
    }

    private static Dto.ClaveTipoRectificativaType Convert(CorrectingInvoiceType type)
    {
        return type.Match(
            CorrectingInvoiceType.CorrectiveInvoiceForDifferences, _ => Dto.ClaveTipoRectificativaType.I,
            CorrectingInvoiceType.CorrectiveInvoiceForReplacement, _ => Dto.ClaveTipoRectificativaType.S
        );
    }

    private static Dto.TipoDesgloseType Convert(TaxBreakdown taxBreakdown)
    {
        return taxBreakdown.Match(
            summary => new Dto.TipoDesgloseType
            {
                Item = new Dto.DesgloseFacturaType
                {
                    Sujeta = Convert(summary)
                }
            },
            operationTaxSummary => new Dto.TipoDesgloseType
            {
                Item = new Dto.DesgloseTipoOperacionType
                {
                    Entrega = operationTaxSummary.Delivery.Map(s => new Dto.Entrega
                    {
                        Sujeta = Convert(s)
                    }).GetOrNull(),
                    PrestacionServicios = operationTaxSummary.ServiceProvision.Map(s => new Dto.PrestacionServicios
                    {
                        Sujeta = Convert(s)
                    }).GetOrNull()
                }
            }
        );
    }

    private static Dto.SujetaType Convert(TaxSummary summary)
    {
        return new Dto.SujetaType
        {
            Exenta = summary.TaxExempt.Map(items => items.Select(i => Convert(i)).ToArray()).GetOrEmpty(),
            NoExenta = summary.Taxed.Map(taxRateSummaries => new Dto.DetalleNoExentaType[]
            {
                new Dto.DetalleNoExentaType
                {
                    TipoNoExenta = Dto.TipoOperacionSujetaNoExentaType.S1,
                    DesgloseIVA = taxRateSummaries.Select(s => Convert(s)).ToArray()
                }
            }).GetOrEmpty()
        };
    }

    private static Dto.DetalleIVAType Convert(TaxRateSummary summary)
    {
        return new Dto.DetalleIVAType
        {
            BaseImponible = Convert(summary.TaxBaseAmount),
            TipoImpositivo = Convert(summary.TaxRatePercentage),
            CuotaImpuesto = Convert(summary.TaxAmount)
        };
    }

    private static Dto.DetalleExentaType Convert(TaxExemptItem item)
    {
        return new Dto.DetalleExentaType
        {
            CausaExencion = item.Cause.Match(
                CauseOfExemption.Article20, _ => Dto.CausaExencionType.E1,
                CauseOfExemption.Article21, _ => Dto.CausaExencionType.E2,
                CauseOfExemption.Article22, _ => Dto.CausaExencionType.E3,
                CauseOfExemption.Article24, _ => Dto.CausaExencionType.E4,
                CauseOfExemption.Article25, _ => Dto.CausaExencionType.E5,
                CauseOfExemption.OtherGrounds, _ => Dto.CausaExencionType.E6
            ),
            BaseImponible = Convert(item.Amount)
        };
    }

    private static string Convert(Amount totalAmount)
    {
        return totalAmount.Value.ToString(CultureInfo);
    }

    private static string Convert(Percentage percentage)
    {
        return percentage.Value.ToString(CultureInfo);
    }

    private static string Convert(DateTime date)
    {
        return date.ToString("dd-MM-yyyy");
    }
}