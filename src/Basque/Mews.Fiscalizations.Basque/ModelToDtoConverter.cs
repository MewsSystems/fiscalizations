using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Basque.Dto;
using FuncSharp;
using System.Linq;
using Mews.Fiscalizations.Core.Model;
using System;

namespace Mews.Fiscalizations.Basque
{
    internal static class ModelToDtoConverter
    {
        public static Dto.TicketBai Convert(SendInvoiceRequest sendInvoiceRequest, ServiceInfo serviceInfo)
        {
            return new Dto.TicketBai
            {
                Cabecera = new Cabecera1
                {
                    IDVersionTBAI = serviceInfo.Version
                },
                Sujetos = Convert(sendInvoiceRequest.Subject),
                Factura = Convert(sendInvoiceRequest.Invoice),
                HuellaTBAI = Convert(sendInvoiceRequest.InvoiceFooter),
                Signature = null // TODO: Convert(sendInvoiceRequest.Signature)
            };
        }

        private static Sujetos Convert(Subject subject)
        {
            return new Sujetos
            {
                Emisor = Convert(subject.Issuer),
                Destinatarios = subject.Receivers.Select(r => Convert(r)).ToArray(),
                VariosDestinatariosSpecified = true,
                VariosDestinatarios = subject.MultipleReceivers.Match(
                    t => SiNoType.S,
                    f => SiNoType.N
                ),
                EmitidaPorTercerosODestinatario = subject.IssuerType.Map(t => Convert(t)).ToNullable(),
                EmitidaPorTercerosODestinatarioSpecified = subject.IssuerType.NonEmpty
            };
        }

        private static Emisor1 Convert(Issuer issuer)
        {
            return new Emisor1
            {
                NIF = issuer.Nif.TaxpayerNumber,
                ApellidosNombreRazonSocial = issuer.Name.Value
            };
        }

        private static EmitidaPorTercerosType Convert(IssuerType type)
        {
            return type.Match(
                IssuerType.IssuedByIssuer, _ => EmitidaPorTercerosType.N,
                IssuerType.IssuedByThirdParty, _ => EmitidaPorTercerosType.T,
                IssuerType.IssuedByTransactionRecipient, _ => EmitidaPorTercerosType.D
            );
        }

        private static IDDestinatario Convert(Receiver receiver)
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
            return new IDDestinatario
            {
                ApellidosNombreRazonSocial = name.Value,
                CodigoPostal = postalCode.Value,
                Direccion = address.Value,
                Item = receiver.Match(
                    local => (object)local.TaxpayerIdentificationNumber.TaxpayerNumber,
                    foreign => new IDOtro1
                    {
                        CodigoPais = CovertCountryType21(foreign.Country),
                        CodigoPaisSpecified = true,
                        ID = foreign.Id.Value,
                        IDType = Convert1(foreign.IdType)
                    }
                )
            };
        }

        private static HuellaTBAI1 Convert(InvoiceFooter footer)
        {
            return new HuellaTBAI1
            {
                EncadenamientoFacturaAnterior = footer.OriginalInvoiceInfo.Map(i => Convert(i)).GetOrNull(),
                Software = Convert(footer.Software),
                NumSerieDispositivo = footer.DeviceSerialNumber.Map(n => n.Value).GetOrNull()
            };
        }

        private static EncadenamientoFacturaAnteriorType Convert(OriginalInvoiceInfo originalInvoiceInfo)
        {
            return new EncadenamientoFacturaAnteriorType
            {
                SerieFacturaAnterior = originalInvoiceInfo.Series.Map(s => s.Value).GetOrNull(),
                NumFacturaAnterior = originalInvoiceInfo.Number.Value,
                FechaExpedicionFacturaAnterior = Convert(originalInvoiceInfo.IssueDate),
                SignatureValueFirmaFacturaAnterior = originalInvoiceInfo.Signature.Value
            };
        }

        private static SoftwareFacturacionType1 Convert(Software software)
        {
            return new SoftwareFacturacionType1
            {
                LicenciaTBAI = software.License.Value,
                Version = software.Version.Value,
                Nombre = software.Name.Value,
                EntidadDesarrolladora = Convert(software.Developer)
            };
        }

        private static EntidadDesarrolladoraType1 Convert(SoftwareDeveloper developer)
        {
            return developer.Match(
                local => new EntidadDesarrolladoraType1
                {
                    Item = local.Nif.TaxpayerNumber
                },
                foreign => new EntidadDesarrolladoraType1
                {
                    Item = new IDOtro
                    {
                        CodigoPaisSpecified = foreign.Country.NonEmpty,
                        CodigoPais = foreign.Country.Map(c => ConvertCountryType2(c)).ToNullable(),
                        ID = foreign.Id.Value,
                        IDType = Convert(foreign.IdType)
                    }
                }
            );
        }

        private static CountryType2 ConvertCountryType2(Country country)
        {
            // TicketBai country list Dto doesn't recognize Kosovo country code, so its being reported as Serbia.
            if (country.Equals(Countries.Kosovo))
            {
                return CountryType2.RS;
            }

            var result = country.Alpha2Code.ToEnum<CountryType2>();
            return result.Get();
        }

        private static CountryType21 CovertCountryType21(Country country)
        {
            // TicketBai country list Dto doesn't recognize Kosovo country code, so its being reported as Serbia.
            if (country.Equals(Countries.Kosovo))
            {
                return CountryType21.RS;
            }

            var result = country.Alpha2Code.ToEnum<CountryType21>();
            return result.Get();
        }

        private static IDTypeType Convert(IdType type)
        {
            return type.Match(
                IdType.NifVat, _ => IDTypeType.Item02,
                IdType.Passport, _ => IDTypeType.Item03,
                IdType.OfficialIdentificationDocumentIssuedByTheCountry, _ => IDTypeType.Item04,
                IdType.ResidenceCertificate, _ => IDTypeType.Item05,
                IdType.OtherSupportingDocument, _ => IDTypeType.Item06
            );
        }

        private static IDTypeType1 Convert1(IdType type)
        {
            return type.Match(
                IdType.NifVat, _ => IDTypeType1.Item02,
                IdType.Passport, _ => IDTypeType1.Item03,
                IdType.OfficialIdentificationDocumentIssuedByTheCountry, _ => IDTypeType1.Item04,
                IdType.ResidenceCertificate, _ => IDTypeType1.Item05,
                IdType.OtherSupportingDocument, _ => IDTypeType1.Item06
            );
        }

        private static Factura Convert(Invoice invoice)
        {
            return new Factura
            {
                CabeceraFactura = Convert(invoice.Header),
                DatosFactura = Convert(invoice.InvoiceData),
                TipoDesglose = Convert(invoice.TaxBreakdown)
            };
        }

        private static DatosFacturaType Convert(InvoiceData data)
        {
            return new DatosFacturaType
            {
                FechaOperacion = data.TransactionDate.Map(d => Convert(d)).GetOrNull(),
                DescripcionFactura = data.Description.Value,
                DetallesFactura = data.Items.Select(i => Convert(i)).ToArray(),
                ImporteTotalFactura = data.TotalAmount.ToString(),
                RetencionSoportada = data.SupportWithheldAmount.Map(a => a.ToString()).GetOrNull(),
                BaseImponibleACoste = data.Tax.Map(t => t.ToString()).GetOrNull(),
                Claves = data.TaxModes.Select(t => Convert(t)).ToArray()
            };
        }

        private static IDDetalleFacturaType Convert(InvoiceItem item)
        {
            return new IDDetalleFacturaType
            {
                DescripcionDetalle = item.Description.Value,
                Cantidad = item.Quantity.ToString(),
                ImporteUnitario = item.UnitAmount.ToString(),
                Descuento = item.Discount.Map(d => d.ToString()).GetOrNull(),
                ImporteTotal = item.TotalAmount.ToString()
            };
        }

        private static IDClaveType Convert(TaxMode taxMode)
        {
            return new IDClaveType
            {
                ClaveRegimenIvaOpTrascendencia = taxMode.Match(
                    TaxMode.GeneralTaxRegimeActivity, _ => IdOperacionesTrascendenciaTributariaType.Item01,
                    TaxMode.Export, _ => IdOperacionesTrascendenciaTributariaType.Item02,
                    TaxMode.WorksOfArt, _ => IdOperacionesTrascendenciaTributariaType.Item03,
                    TaxMode.InvestmentGold, _ => IdOperacionesTrascendenciaTributariaType.Item04,
                    TaxMode.TravelAgencies, _ => IdOperacionesTrascendenciaTributariaType.Item05,
                    TaxMode.GroupsOfEntities, _ => IdOperacionesTrascendenciaTributariaType.Item06,
                    TaxMode.CashBasis, _ => IdOperacionesTrascendenciaTributariaType.Item07,
                    TaxMode.CanaryIslandsGeneralIndirectTax, _ => IdOperacionesTrascendenciaTributariaType.Item08,
                    TaxMode.TravelAgencyServicesActingAsIntermediaries, _ => IdOperacionesTrascendenciaTributariaType.Item09,
                    TaxMode.Collections, _ => IdOperacionesTrascendenciaTributariaType.Item10,
                    TaxMode.BusinessPremisesLeaseActivities1, _ => IdOperacionesTrascendenciaTributariaType.Item11,
                    TaxMode.BusinessPremisesLeaseActivities2, _ => IdOperacionesTrascendenciaTributariaType.Item12,
                    TaxMode.BusinessPremisesLeaseActivities3, _ => IdOperacionesTrascendenciaTributariaType.Item13,
                    TaxMode.InvoiceWithVATPendingAccrual1, _ => IdOperacionesTrascendenciaTributariaType.Item14,
                    TaxMode.InvoiceWithVATPendingAccrual2, _ => IdOperacionesTrascendenciaTributariaType.Item15,
                    TaxMode.SurchargeOperations, _ => IdOperacionesTrascendenciaTributariaType.Item51,
                    TaxMode.SimplifiedOperations, _ => IdOperacionesTrascendenciaTributariaType.Item52,
                    TaxMode.NotVatEntity, _ => IdOperacionesTrascendenciaTributariaType.Item53
                )
            };
        }

        private static CabeceraFacturaType1 Convert(InvoiceHeader header)
        {
            return new CabeceraFacturaType1
            {
                SerieFactura = header.Series.Map(s => s.Value).GetOrNull(),
                NumFactura = header.Number.Value,
                FechaExpedicionFactura = Convert(header.Issued.Date),
                HoraExpedicionFactura = header.Issued.ToString("HH:MM:ss"),
                FacturaSimplificada = header.IsSimplified.Map(i => i.Match(
                    t => SiNoType.S,
                    f => SiNoType.N
                )).GetOrElse(SiNoType.N),
                FacturaSimplificadaSpecified = header.IsSimplified.NonEmpty,
                FacturaEmitidaSustitucionSimplificada = header.IssuedInSubstitutionOfSimplifiedInvoice.Map(i => i.Match(
                    t => SiNoType.S,
                    f => SiNoType.N
                )).GetOrElse(SiNoType.N),
                FacturaEmitidaSustitucionSimplificadaSpecified = header.IssuedInSubstitutionOfSimplifiedInvoice.NonEmpty,
                FacturaRectificativa = header.CorrectingInvoice.Map(i => Convert(i)).GetOrNull(),
                FacturasRectificadasSustituidas = header.CorrectedInvoices.Map(invoices => invoices.Select(i => Convert(i)).ToArray()).GetOrNull()
            };
        }

        private static IDFacturaRectificadaSustituidaType Convert(CorrectedInvoice correctedInvoice)
        {
            return new IDFacturaRectificadaSustituidaType
            {
                SerieFactura = correctedInvoice.Series.Value,
                NumFactura = correctedInvoice.Number.Value,
                FechaExpedicionFactura = Convert(correctedInvoice.IssueDate)
            };
        }

        private static FacturaRectificativaType Convert(CorrectingInvoice correctiveInvoice)
        {
            return new FacturaRectificativaType
            {
                Codigo = Convert(correctiveInvoice.Code),
                Tipo = Convert(correctiveInvoice.Type),
                ImporteRectificacionSustitutiva = correctiveInvoice.Amount.Map(a => new ImporteRectificacionSustitutivaType
                {
                    BaseRectificada = a.Amount.ToString(),
                    CuotaRecargoRectificada = a.Surcharge.ToString(),
                    CuotaRectificada = a.Fee.ToString()
                }).GetOrNull()
            };
        }

        private static ClaveTipoFacturaType Convert(CorrectingInvoiceCode code)
        {
            return code.Match(
                CorrectingInvoiceCode.CorrectedInvoice, _ => ClaveTipoFacturaType.R1,
                CorrectingInvoiceCode.CorrectedInvoice2, _ => ClaveTipoFacturaType.R2,
                CorrectingInvoiceCode.CorrectedInvoice3, _ => ClaveTipoFacturaType.R3,
                CorrectingInvoiceCode.CorrectedInvoice4, _ => ClaveTipoFacturaType.R4,
                CorrectingInvoiceCode.CorrectedInvoiceInSimplifiedInvoices, _ => ClaveTipoFacturaType.R5
            );
        }

        private static ClaveTipoRectificativaType Convert(CorrectingInvoiceType type)
        {
            return type.Match(
                CorrectingInvoiceType.CorrectiveInvoiceForDifferences, _ => ClaveTipoRectificativaType.I,
                CorrectingInvoiceType.CorrectiveInvoiceForReplacement, _ => ClaveTipoRectificativaType.S
            );
        }

        private static TipoDesgloseType Convert(TaxBreakdown taxBreakdown)
        {
            return taxBreakdown.Match(
                summary => new TipoDesgloseType
                {
                    Item = new DesgloseFacturaType
                    {
                        Sujeta = Convert(summary)
                    }
                },
                operationTaxSummary => new TipoDesgloseType
                {
                    Item = new DesgloseTipoOperacionType
                    {
                        Entrega = operationTaxSummary.Delivery.Map(s => new Entrega
                        {
                            Sujeta = Convert(s)
                        }).GetOrNull(),
                        PrestacionServicios = operationTaxSummary.ServiceProvision.Map(s => new PrestacionServicios
                        {
                            Sujeta = Convert(s)
                        }).GetOrNull()
                    }
                }
            );
        }

        private static SujetaType Convert(TaxSummary summary)
        {
            return new SujetaType
            {
                Exenta = summary.TaxExempt.Map(items => items.Select(i => Convert(i)).ToArray()).GetOrNull(),
                NoExenta = summary.Taxed.Map(taxRateSummaries => new DetalleNoExentaType[]
                {
                    new DetalleNoExentaType
                    {
                        TipoNoExenta = TipoOperacionSujetaNoExentaType.S1,
                        DesgloseIVA = taxRateSummaries.Select(s => Convert(s)).ToArray()
                    }
                }).GetOrNull()
            };
        }

        private static DetalleIVAType Convert(TaxRateSummary summary)
        {
            return new DetalleIVAType
            {
                BaseImponible = Convert(summary.TaxBaseAmount),
                TipoImpositivo = Convert(summary.TaxRatePercentage),
                CuotaImpuesto = Convert(summary.TaxAmount)
            };
        }

        private static DetalleExentaType Convert(TaxExemptItem item)
        {
            return new DetalleExentaType
            {
                CausaExencion = item.Cause.Match(
                    CauseOfExemption.Article20, _ => CausaExencionType.E1,
                    CauseOfExemption.Article21, _ => CausaExencionType.E2,
                    CauseOfExemption.Article22, _ => CausaExencionType.E3,
                    CauseOfExemption.Article24, _ => CausaExencionType.E4,
                    CauseOfExemption.Article25, _ => CausaExencionType.E5,
                    CauseOfExemption.OtherGrounds, _ => CausaExencionType.E6
                ),
                BaseImponible = Convert(item.Amount)
            };
        }

        private static string Convert(Amount totalAmount)
        {
            return totalAmount.Value.ToString();
        }

        private static string Convert(Percentage percentage)
        {
            return percentage.Value.ToString();
        }

        private static string Convert(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }
    }
}