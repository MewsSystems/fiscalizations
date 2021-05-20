﻿using System;
using System.Linq;
using FuncSharp;
using Mews.Fiscalization.Core.Model;
using Mews.Fiscalization.Spain.Dto.Requests;
using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;
using Mews.Fiscalization.Spain.Dto.XSD.SuministroLR;
using Mews.Fiscalization.Spain.Model;
using Mews.Fiscalization.Spain.Model.Request;

namespace Mews.Fiscalization.Spain.Converters
{
    internal class ModelToDtoConverter
    {
        public SubmitIssuedInvoicesRequest Convert(InvoicesToSubmit model)
        {
            return new SubmitIssuedInvoicesRequest
            {
                Cabecera = Convert(model.Header),
                RegistroLRFacturasEmitidas = model.Invoices.Select(i => Convert(i)).ToArray()
            };
        }

        public SubmitIssuedInvoicesRequest Convert(SimplifiedInvoicesToSubmit model)
        {
            return new SubmitIssuedInvoicesRequest
            {
                Cabecera = Convert(model.Header),
                RegistroLRFacturasEmitidas = model.Invoices.Select(i => Convert(i)).ToArray()
            };
        }

        private LRfacturasEmitidasType Convert(Invoice invoice)
        {
            return new LRfacturasEmitidasType
            {
                PeriodoLiquidacion = Convert(invoice.TaxPeriod),
                IDFactura = Convert(invoice.Id),
                FacturaExpedida = new FacturaExpedidaType
                {
                    TipoFactura = Convert(invoice.Type),
                    ClaveRegimenEspecialOTrascendencia = Convert(invoice.SchemeOrEffect),
                    ImporteTotal = invoice.TotalAmount.Serialize(),
                    DescripcionOperacion = invoice.Description.Value,
                    Contraparte = Convert(invoice.CounterPartyCompany),
                    TipoDesglose = Convert(invoice.TaxBreakdown),
                    EmitidaPorTercerosODestinatarioSpecified = true,
                    EmitidaPorTercerosODestinatario = Convert(invoice.IssuedByThirdParty)
                }
            };
        }

        private LRfacturasEmitidasType Convert(SimplifiedInvoice invoice)
        {
            return new LRfacturasEmitidasType
            {
                PeriodoLiquidacion = Convert(invoice.TaxPeriod),
                IDFactura = Convert(invoice.Id),
                FacturaExpedida = new FacturaExpedidaType
                {
                    TipoFactura = ClaveTipoFacturaType.F2,
                    ClaveRegimenEspecialOTrascendencia = Convert(invoice.SchemeOrEffect),
                    ImporteTotal = invoice.TotalAmount.Serialize(),
                    DescripcionOperacion = invoice.Description.Value,
                    TipoDesglose = Convert(invoice.TaxBreakdown),
                    EmitidaPorTercerosODestinatarioSpecified = true,
                    EmitidaPorTercerosODestinatario = Convert(invoice.IssuedByThirdParty)
                }
            };
        }

        private FacturaExpedidaTypeTipoDesglose Convert(TaxBreakdown taxBreakdown)
        {
            return taxBreakdown.Match(
                summary => new FacturaExpedidaTypeTipoDesglose
                {
                    Item = new TipoSinDesgloseType
                    {
                        Sujeta = Convert(summary)
                    }
                },
                operationTaxSummary => new FacturaExpedidaTypeTipoDesglose
                {
                    Item = new TipoConDesgloseType
                    {
                        Entrega = operationTaxSummary.Delivery.Map(s => new TipoSinDesgloseType
                        {
                            Sujeta = Convert(s)
                        }).GetOrNull(),
                        PrestacionServicios = operationTaxSummary.ServiceProvision.Map(s => new TipoSinDesglosePrestacionType
                        {
                            Sujeta = ConvertProvision(s)
                        }).GetOrNull()
                    }
                }
            );
        }

        private SujetaType Convert(TaxSummary summary)
        {
            return new SujetaType
            {
                Exenta = summary.TaxExempt.Map(items => items.Select(i => Convert(i)).ToArray()).GetOrNull(),
                NoExenta = summary.Taxed.Map(taxRateSummaries => new SujetaTypeNoExenta
                {
                    TipoNoExenta = TipoOperacionSujetaNoExentaType.S1,
                    DesgloseIVA = taxRateSummaries.Select(s => Convert(s)).ToArray()
                }).GetOrNull()
            };
        }

        private SujetaPrestacionType ConvertProvision(TaxSummary summary)
        {
            return new SujetaPrestacionType
            {
                Exenta = summary.TaxExempt.Map(items => items.Select(i => Convert(i)).ToArray()).GetOrNull(),
                NoExenta = summary.Taxed.Map(taxRateSummaries => new SujetaPrestacionTypeNoExenta
                {
                    TipoNoExenta = TipoOperacionSujetaNoExentaType.S1,
                    DesgloseIVA = taxRateSummaries.Select(s => ConvertProvision(s)).ToArray()
                }).GetOrNull()
            };
        }

        private DetalleExentaType Convert(TaxExemptItem item)
        {
            return new DetalleExentaType
            {
                CausaExencionSpecified = item.Cause.NonEmpty,
                CausaExencion = item.Cause.Match(
                    c => c.Match(
                        CauseOfExemption.Article20, _ => CausaExencionType.E1,
                        CauseOfExemption.Article21, _ => CausaExencionType.E2,
                        CauseOfExemption.Article22, _ => CausaExencionType.E3,
                        CauseOfExemption.Article24, _ => CausaExencionType.E4,
                        CauseOfExemption.Article25, _ => CausaExencionType.E5,
                        CauseOfExemption.OtherGrounds, _ => CausaExencionType.E6
                    ),
                    _ => (CausaExencionType?)null
                ),
                BaseImponible = Convert(item.Amount)
            };
        }

        private DetalleIVAEmitidaType Convert(TaxRateSummary summary)
        {
            return new DetalleIVAEmitidaType
            {
                TipoImpositivo = Convert(summary.TaxRatePercentage),
                BaseImponible = Convert(summary.TaxBaseAmount),
                CuotaRepercutida = Convert(summary.TaxAmount)
            };
        }

        private DetalleIVAEmitidaPrestacionType ConvertProvision(TaxRateSummary summary)
        {
            return new DetalleIVAEmitidaPrestacionType
            {
                TipoImpositivo = Convert(summary.TaxRatePercentage),
                BaseImponible = Convert(summary.TaxBaseAmount),
                CuotaRepercutida = Convert(summary.TaxAmount)
            };
        }

        private PersonaFisicaJuridicaType Convert(CounterPartyCompany counterParty)
        {
            return counterParty?.Match(
                local => new PersonaFisicaJuridicaType
                {
                    NombreRazon = local.Name.Value,
                    Item = local.TaxpayerIdentificationNumber.TaxpayerNumber
                },
                foreign => new PersonaFisicaJuridicaType
                {
                    NombreRazon = foreign.Name.Value,
                    Item = Convert(foreign)
                }
            );
        }

        private IDOtroType Convert(ForeignCompany foreignCompany)
        {
            return new IDOtroType
            {
                CodigoPais = Convert(foreignCompany.Country),
                CodigoPaisSpecified = true,
                ID = foreignCompany.Id.Value,
                IDType = foreignCompany.IdentificatorType.Match(
                    ResidenceCountryIdentificatorType.NifVat, _ => PersonaFisicaJuridicaIDTypeType.Item02,
                    ResidenceCountryIdentificatorType.Passport, _ => PersonaFisicaJuridicaIDTypeType.Item03,
                    ResidenceCountryIdentificatorType.OfficialIdentificationDocumentIssuedByTheCountry, _ => PersonaFisicaJuridicaIDTypeType.Item04,
                    ResidenceCountryIdentificatorType.ResidenceCertificate, _ => PersonaFisicaJuridicaIDTypeType.Item05,
                    ResidenceCountryIdentificatorType.OtherSupportingDocument, _ => PersonaFisicaJuridicaIDTypeType.Item06,
                    ResidenceCountryIdentificatorType.NotSelected, _ => PersonaFisicaJuridicaIDTypeType.Item07
                )
            };
        }

        private RegistroSiiPeriodoLiquidacion Convert(TaxPeriod taxPeriod)
        {
            return new RegistroSiiPeriodoLiquidacion
            {
                Ejercicio = Convert(taxPeriod.Year),
                Periodo = taxPeriod.Month.Match(
                    Month.January, _ => TimePeriodType.January,
                    Month.February, _ => TimePeriodType.February,
                    Month.March, _ => TimePeriodType.March,
                    Month.April, _ => TimePeriodType.April,
                    Month.May, _ => TimePeriodType.May,
                    Month.June, _ => TimePeriodType.June,
                    Month.July, _ => TimePeriodType.July,
                    Month.August, _ => TimePeriodType.August,
                    Month.September, _ => TimePeriodType.September,
                    Month.October, _ => TimePeriodType.October,
                    Month.November, _ => TimePeriodType.November,
                    Month.December, _ => TimePeriodType.December
                )
            };
        }

        private IDFacturaExpedidaType Convert(InvoiceId id)
        {
            return new IDFacturaExpedidaType
            {
                FechaExpedicionFacturaEmisor = Convert(id.Date),
                IDEmisorFactura = new IDFacturaExpedidaTypeIDEmisorFactura
                {
                    NIF = id.Issuer.TaxpayerNumber
                },
                NumSerieFacturaEmisor = id.Number.Value
            };
        }

        private CabeceraSii Convert(Header header)
        {
            return new CabeceraSii
            {
                IDVersionSii = VersionSiiType.Item11,
                Titular = Convert(header.Company)
            };
        }

        private PersonaFisicaJuridicaESType Convert(LocalCompany companyTitle)
        {
            return new PersonaFisicaJuridicaESType
            {
                NombreRazon = companyTitle.Name.Value,
                NIF = companyTitle.TaxpayerIdentificationNumber.TaxpayerNumber
            };
        }

        private ClaveTipoFacturaType Convert(InvoiceType type)
        {
            return type.Match(
                InvoiceType.Invoice, _ => ClaveTipoFacturaType.F1,
                InvoiceType.CorrectedInvoice, _ => ClaveTipoFacturaType.R1,
                InvoiceType.CorrectedInvoice2, _ => ClaveTipoFacturaType.R2,
                InvoiceType.CorrectedInvoice3, _ => ClaveTipoFacturaType.R3,
                InvoiceType.CorrectedInvoice4, _ => ClaveTipoFacturaType.R4,
                InvoiceType.CorrectedInvoiceInSimplifiedInvoices, _ => ClaveTipoFacturaType.R5,
                InvoiceType.InvoiceIssuedToReplaceSimplifiedInvoices, _ => ClaveTipoFacturaType.F3,
                InvoiceType.InvoiceSummaryEntry, _ => ClaveTipoFacturaType.F4
            );
        }

        private IdOperacionesTrascendenciaTributariaType Convert(SchemeOrEffect schemeOrEffect)
        {
            return schemeOrEffect.Match(
                SchemeOrEffect.GeneralTaxRegimeActivity, _ => IdOperacionesTrascendenciaTributariaType.Item01,
                SchemeOrEffect.Export, _ => IdOperacionesTrascendenciaTributariaType.Item02,
                SchemeOrEffect.WorksOfArt, _ => IdOperacionesTrascendenciaTributariaType.Item03,
                SchemeOrEffect.InvestmentGold, _ => IdOperacionesTrascendenciaTributariaType.Item04,
                SchemeOrEffect.TravelAgencies, _ => IdOperacionesTrascendenciaTributariaType.Item05,
                SchemeOrEffect.GroupsOfEntities, _ => IdOperacionesTrascendenciaTributariaType.Item06,
                SchemeOrEffect.CashBasis, _ => IdOperacionesTrascendenciaTributariaType.Item07,
                SchemeOrEffect.CanaryIslandsGeneralIndirectTax, _ => IdOperacionesTrascendenciaTributariaType.Item08,
                SchemeOrEffect.TravelAgencyServicesActingAsIntermediaries, _ => IdOperacionesTrascendenciaTributariaType.Item09,
                SchemeOrEffect.Collections, _ => IdOperacionesTrascendenciaTributariaType.Item10,
                SchemeOrEffect.BusinessPremisesLeaseActivities1, _ => IdOperacionesTrascendenciaTributariaType.Item11,
                SchemeOrEffect.BusinessPremisesLeaseActivities2, _ => IdOperacionesTrascendenciaTributariaType.Item12,
                SchemeOrEffect.BusinessPremisesLeaseActivities3, _ => IdOperacionesTrascendenciaTributariaType.Item13,
                SchemeOrEffect.InvoiceWithVATPendingAccrual1, _ => IdOperacionesTrascendenciaTributariaType.Item14,
                SchemeOrEffect.InvoiceWithVATPendingAccrual2, _ => IdOperacionesTrascendenciaTributariaType.Item15
            );
        }

        private EmitidaPorTercerosType Convert(bool issuedByThirdParty)
        {
            return issuedByThirdParty.Match(
                t => EmitidaPorTercerosType.S,
                f => EmitidaPorTercerosType.N
            );
        }

        private CountryType2 Convert(Country country)
        {
            var result = country.Alpha2Code.ToEnum<CountryType2>();
            return result.Get();
        }

        private string Convert(Amount totalAmount)
        {
            return totalAmount.Value.Serialize();
        }

        private string Convert(Percentage percentage)
        {
            return percentage.Value.Serialize();
        }

        private string Convert(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        private string Convert(Year year)
        {
            return year.Value.ToString();
        }
    }
}