using Mews.Fiscalization.Greece.Model;
using System;
using System.Linq;
using Mews.Fiscalization.Core.Model;
using TaxType = Mews.Fiscalization.Greece.Model.TaxType;
using FuncSharp;

namespace Mews.Fiscalization.Greece.Mapper
{
    public static class InvoiceDocumentMapper
    {
        private static readonly Country Greece = Countries.Greece;

        public static Dto.Xsd.InvoicesDoc GetInvoiceDoc(ISequenceStartingWithOne<Invoice> invoices)
        {
            return new Dto.Xsd.InvoicesDoc
            {
                Invoices = invoices.Values.Select(invoice => GetInvoice(invoice.Value)).ToArray()
            };
        }

        private static Dto.Xsd.Invoice GetInvoice(Invoice invoice)
        {
            return new Dto.Xsd.Invoice
            {
                InvoiceId = invoice.Info.Header.InvoiceIdentifier,
                InvoiceIssuer = GetInvoiceParty(invoice.Info.Issuer.ToOption()),
                InvoiceCounterpart = GetInvoiceParty(invoice.Counterpart),
                InvoiceSummary = GetInvoiceSummary(invoice),
                InvoiceHeader = GetInvoiceHeader(invoice),
                InvoiceDetails = invoice.RevenueItems.Values.Select(invoiceDetail => GetInvoiceDetail(invoice, invoiceDetail)).ToArray(),
                PaymentMethods = invoice.Payments?.Select(paymentMethod => new Dto.Xsd.PaymentMethod
                {
                    Amount = Math.Abs(paymentMethod.Amount),
                    PaymentMethodType = MapPaymentMethodType(paymentMethod.PaymentType)
                }).ToArray()
            };
        }

        private static Dto.Xsd.InvoiceParty GetInvoiceParty(IOption<InvoiceParty> counterpart)
        {
            var invoiceParty = counterpart.Map(p => new Dto.Xsd.InvoiceParty
            {
                Country = (Dto.Xsd.Country)Enum.Parse(typeof(Dto.Xsd.Country), p.Country.Alpha2Code, true),
                Branch = p.Info.Branch.Value,
                Name = p.Info.Name.GetOrNull(),
                VatNumber = p.Info.TaxIdentifier.Map(n => n.TaxpayerNumber).GetOrNull(),
                Address = p.Info.Address.Map(a => GetAddress(a)).GetOrNull()
            });

            return invoiceParty.GetOrNull();
        }

        private static Dto.Xsd.Address GetAddress(Address address)
        {
            if (address != null)
            {
                return new Dto.Xsd.Address
                {
                    City = address.City.Value,
                    Number = address.Number,
                    PostalCode = address.PostalCode.Value,
                    Street = address.Street
                };
            }

            return null;
        }

        private static Dto.Xsd.InvoiceHeader GetInvoiceHeader(Invoice invoice)
        {
            var header = invoice.Info.Header;
            return new Dto.Xsd.InvoiceHeader
            {
                InvoiceType = MapInvoiceType(invoice),
                IssueDate = header.InvoiceIssueDate,
                SerialNumber = header.InvoiceSerialNumber.Value,
                Series = header.InvoiceSeries.Value,
                CurrencySpecified = header.CurrencyCode.IsNotNull(),
                ExchangeRateSpecified = header.ExchangeRate.IsNotNull(),
                ExchangeRate = header.ExchangeRate?.Value ?? 0,
                CorrelatedInvoicesSpecified = invoice.CorrelatedInvoice.NonEmpty,
                CorrelatedInvoices = invoice.CorrelatedInvoice.GetOrElse((long)0),
                Currency = header.CurrencyCode.IsNotNull().Match(
                    t => (Dto.Xsd.Currency)Enum.Parse(typeof(Dto.Xsd.Currency), header.CurrencyCode.Value, true),
                    f => (Dto.Xsd.Currency?)null
                )
            };
        }

        private static Dto.Xsd.InvoiceDetail GetInvoiceDetail(Invoice invoice, Indexed<Revenue> indexedRevenueItem)
        {
            var revenueItem = indexedRevenueItem.Value;
            var info = revenueItem.Info;
            return new Dto.Xsd.InvoiceDetail
            {
                LineNumber = indexedRevenueItem.Index,
                NetValue = Math.Abs(revenueItem.NetValue),
                VatAmount = Math.Abs(revenueItem.VatValue),
                VatCategory = MapVatCategory(info.TaxType),
                IncomeClassification = new[] { GetIncomeClassification(invoice, revenueItem) },
                VatExemptionCategorySpecified = info.VatExemption.NonEmpty,
                VatExemptionCategory = info.VatExemption.Match(
                    c => MapVatExemptionCategory(c),
                    _ => (Dto.Xsd.VatExemptionCategory?)null
                )
            };
        }

        private static Dto.Xsd.InvoiceSummary GetInvoiceSummary(Invoice invoice)
        {
            var invoiceSummary = new Dto.Xsd.InvoiceSummary
            {
                TotalNetValue = Math.Abs(invoice.RevenueItems.Values.Sum(x => x.Value.NetValue)),
                TotalVatAmount = Math.Abs(invoice.RevenueItems.Values.Sum(x => x.Value.VatValue))
            };

            invoiceSummary.IncomeClassification = invoice.RevenueItems.Values.GroupBy(
                keySelector: m => m.Value.Info.RevenueType,
                resultSelector: (key, revenueItems) => new Dto.Xsd.IncomeClassification
                {
                    ClassificationCategory = MapRevenueClassification(invoice, key).Category,
                    ClassificationType = MapRevenueClassification(invoice, key).Type,
                    Amount = Math.Abs(revenueItems.Sum(i => i.Value.NetValue))
                }
            ).ToArray();


            invoiceSummary.TotalGrossValue = invoiceSummary.TotalNetValue + invoiceSummary.TotalVatAmount + invoiceSummary.TotalOtherTaxesAmount;

            return invoiceSummary;
        }

        private static Dto.Xsd.IncomeClassification GetIncomeClassification(Invoice invoice, Revenue revenue)
        {
            var revenueClassification = MapRevenueClassification(invoice, revenue.Info.RevenueType);

            return new Dto.Xsd.IncomeClassification
            {
                Amount = Math.Abs(revenue.NetValue),
                ClassificationCategory = revenueClassification.Category,
                ClassificationType = revenueClassification.Type
            };
        }

        private static Dto.Xsd.InvoiceType MapInvoiceType(Invoice invoice)
        {
            return invoice.Match(
                salesInvoice =>
                {
                    var info = invoice.Info;
                    var country = invoice.Counterpart.Get(_ => new Exception("Counterpart is mandatory on this invoice type")).Country;
                    if (country == Greece)
                    {
                        return Dto.Xsd.InvoiceType.SalesInvoice;
                    }
                    else if (country.IsFirst)
                    {
                        return Dto.Xsd.InvoiceType.SalesInvoiceIntraCommunitySupplies;
                    }
                    return Dto.Xsd.InvoiceType.SalesInvoiceThirdCountrySupplies;
                },
                simplifiedInvoice => Dto.Xsd.InvoiceType.SimplifiedInvoice,
                retailSalesReceipt => Dto.Xsd.InvoiceType.RetailSalesReceipt,
                creditInvoice =>
                {
                    if (invoice.CorrelatedInvoice.NonEmpty)
                    {
                        return Dto.Xsd.InvoiceType.CreditInvoiceAssociated;
                    }
                    return Dto.Xsd.InvoiceType.CreditInvoiceNonAssociated;
                }
            );
        }

        private static (Dto.Xsd.IncomeClassificationCategory Category, Dto.Xsd.IncomeClassificationType Type) MapRevenueClassification(
            Invoice invoice,
            RevenueType revenueType)
        {
            return revenueType.Match(
                RevenueType.Products, _ => (Dto.Xsd.IncomeClassificationCategory.ProductSaleIncome, GetGoodsAndServicesClassificationType(invoice)),
                RevenueType.Services, _ => (Dto.Xsd.IncomeClassificationCategory.ProvisionOfServicesIncome, GetGoodsAndServicesClassificationType(invoice)),
                RevenueType.Other, _ => (Dto.Xsd.IncomeClassificationCategory.OtherIncomeAndProfits, Dto.Xsd.IncomeClassificationType.OtherOrdinaryIncome)
            );
        }

        private static Dto.Xsd.IncomeClassificationType GetGoodsAndServicesClassificationType(Invoice invoice)
        {
            return invoice.Match(
                salesInvoice =>
                {
                    var country = invoice.Counterpart.Get(_ => new Exception("Counterpart is mandatory on this invoice type")).Country;
                    if (country == Greece)
                    {
                        return Dto.Xsd.IncomeClassificationType.OtherSalesOfGoodsAndServices;
                    }
                    else if (country.IsFirst)
                    {
                        return Dto.Xsd.IncomeClassificationType.IntraCommunityForeignSalesOfGoodsAndServices;
                    }
                    return Dto.Xsd.IncomeClassificationType.ThirdCountryForeignSalesOfGoodsAndServices;
                },
                simplifiedInvoice => Dto.Xsd.IncomeClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele,
                retailSalesReceipt => Dto.Xsd.IncomeClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele,
                creditInvoice => Dto.Xsd.IncomeClassificationType.OtherSalesOfGoodsAndServices
            );
        }

        private static Dto.Xsd.VatCategory MapVatCategory(TaxType taxType)
        {
            switch (taxType)
            {
                case TaxType.Vat24:
                    return Dto.Xsd.VatCategory.Vat24;
                case TaxType.Vat13:
                    return Dto.Xsd.VatCategory.Vat13;
                case TaxType.Vat6:
                    return Dto.Xsd.VatCategory.Vat6;
                case TaxType.Vat0:
                    return Dto.Xsd.VatCategory.Vat0;
                case TaxType.WithoutVat:
                    return Dto.Xsd.VatCategory.WithoutVat;
                default:
                    throw new ArgumentException($"Cannot map TaxType {taxType} to {nameof(Dto.Xsd.VatCategory)}.");
            }
        }

        private static Dto.Xsd.PaymentMethodType MapPaymentMethodType(PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.DomesticPaymentsAccountNumber:
                    return Dto.Xsd.PaymentMethodType.DomesticPaymentsAccountNumber;
                case PaymentType.ForeignMethodsAccountNumber:
                    return Dto.Xsd.PaymentMethodType.ForeignMethodsAccountNumber;
                case PaymentType.Check:
                    return Dto.Xsd.PaymentMethodType.Check;
                case PaymentType.OnCredit:
                    return Dto.Xsd.PaymentMethodType.OnCredit;
                case PaymentType.Cash:
                    return Dto.Xsd.PaymentMethodType.Cash;
                default:
                    throw new ArgumentException($"Cannot map PaymentType {paymentType} to {nameof(Dto.Xsd.PaymentMethodType)}.");
            }
        }

        private static Dto.Xsd.VatExemptionCategory MapVatExemptionCategory(VatExemptionType vatExemption)
        {
            switch (vatExemption)
            {
                case VatExemptionType.VatIncludedArticle43:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle43;
                case VatExemptionType.VatIncludedArticle44:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle44;
                case VatExemptionType.VatIncludedArticle45:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle45;
                case VatExemptionType.VatIncludedArticle46:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle46;
                case VatExemptionType.WithoutVatArticle13:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle13;
                case VatExemptionType.WithoutVatArticle14:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle14;
                case VatExemptionType.WithoutVatArticle16:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle16;
                case VatExemptionType.WithoutVatArticle19:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle19;
                case VatExemptionType.WithoutVatArticle22:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle22;
                case VatExemptionType.WithoutVatArticle24:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle24;
                case VatExemptionType.WithoutVatArticle25:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle25;
                case VatExemptionType.WithoutVatArticle26:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle26;
                case VatExemptionType.WithoutVatArticle27:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle27;
                case VatExemptionType.WithoutVatArticle271CSeagoingVessels:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle271CSeagoingVessels;
                case VatExemptionType.WithoutVatArticle27SeagoingVessels:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle27SeagoingVessels;
                case VatExemptionType.WithoutVatArticle28:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle28;
                case VatExemptionType.WithoutVatArticle3:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle3;
                case VatExemptionType.WithoutVatArticle39:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle39;
                case VatExemptionType.WithoutVatArticle39A:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle39A;
                case VatExemptionType.WithoutVatArticle40:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle40;
                case VatExemptionType.WithoutVatArticle41:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle41;
                case VatExemptionType.WithoutVatArticle47:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle47;
                case VatExemptionType.WithoutVatArticle5:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle5;
                default:
                    throw new ArgumentException($"Cannot map VatExemption {vatExemption} to Dto.Xsd.{nameof(Dto.Xsd.VatExemptionCategory)}.");
            }
        }
    }
}
