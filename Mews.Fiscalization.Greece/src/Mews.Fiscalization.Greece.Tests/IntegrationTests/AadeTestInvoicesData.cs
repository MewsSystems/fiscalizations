using Mews.Fiscalization.Greece.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Core.Model;
using FuncSharp;

namespace Mews.Fiscalization.Greece.Tests.IntegrationTests
{
    internal static class AadeTestInvoicesData
    {
        private static readonly string UserVatNumber = Environment.GetEnvironmentVariable("user_vat_number") ?? "INSERT_USER_VAT_NUMBER";

        public static IEnumerable<object[]> GetInvoices()
        {
            var invoices = new List<object>
            {
                RetailSalesReceiptForCustomer(),
                SalesInvoiceForCompany(),
                InvoiceForForeignCompany(),
                InvoiceForForeignCompany(),
                SimplifiedInvoiceForCustomer(),
                CreditInvoiceNonAssociated(),
                CreditInvoiceNonAssociatedForForeignCompany(),
                CreditInvoiceNonAssociatedForForeignCompany()
            };
            return invoices.Select(i => new[] { i });
        }

        internal static InvoiceInfo CreateInvoiceInfo(
            string invoiceSerialNumber = "50020",
            string invoiceSeries = "0",
            string currencyCode = "EUR",
            string invoiceIdentifier = null,
            decimal? exchangeRate = null)
        {
            return InvoiceInfo.Create(
                header: new InvoiceHeader(
                    invoiceSeries: String1To50.CreateUnsafe(invoiceSeries),
                    invoiceSerialNumber: String1To50.CreateUnsafe(invoiceSerialNumber),
                    invoiceIssueDate: DateTime.Now,
                    invoiceIdentifier: invoiceIdentifier,
                    currencyCode: CurrencyCode.Create(currencyCode).Success.Get(),
                    exchangeRate: exchangeRate.IsNotNull().Match(t => ExchangeRate.Create(exchangeRate.Value).Success.Get(), f => null)
                ),
                issuer: CreateInvoiceParty(Countries.Greece, UserVatNumber)
            ).Success.Get();
        }

        internal static RevenueInfo CreateRevenueInfo(TaxType taxType, RevenueType revenueType, VatExemptionType? vatExemptionType = null)
        {
            return RevenueInfo.Create(taxType, revenueType, vatExemptionType).Success.Get();
        }

        internal static InvoiceParty CreateInvoiceParty(Country country, string taxNumber, string name = null, Address address = null)
        {
            return InvoiceParty.Create(
                info: InvoicePartyInfo.Create(NonNegativeInt.Zero(), TaxpayerIdentificationNumber.Create(country, taxNumber).Success.Get(), name, address).Success.Get(),
                country: country
            ).Success.Get();
        }

        private static ISequenceStartingWithOne<Invoice> RetailSalesReceiptForCustomer()
        {
            return SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(RetailSalesReceipt.Create(
                    info: CreateInvoiceInfo(),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(53.65m).Success.Get(), NonNegativeAmount.Create(12.88m).Success.Get(), CreateRevenueInfo(TaxType.Vat6, RevenueType.Products)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(53.65m).Success.Get(), NonNegativeAmount.Create(12.88m).Success.Get(), CreateRevenueInfo(TaxType.Vat6, RevenueType.Services)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(53.65m).Success.Get(), NonNegativeAmount.Create(12.88m).Success.Get(), CreateRevenueInfo(TaxType.Vat6, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NonNegativePayment.Create(NonNegativeAmount.Create(133.06m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(66.53m).Success.Get(), PaymentType.Cash).Success.Get()
                    )
                ).Success.Get())
            ));
        }

        private static ISequenceStartingWithOne<Invoice> SalesInvoiceForCompany()
        {
            return SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(SalesInvoice.Create(
                    info: CreateInvoiceInfo(),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(88.50m).Success.Get(), NonNegativeAmount.Create(11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Products)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(88.50m).Success.Get(), NonNegativeAmount.Create(11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Services)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(88.50m).Success.Get(), NonNegativeAmount.Create(11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.OnCredit).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    ),
                    counterpart: CreateInvoiceParty(Countries.Greece, "090701900")
                ).Success.Get())
            ));
        }

        private static ISequenceStartingWithOne<Invoice> InvoiceForForeignCompany()
        {
            return SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(SalesInvoice.Create(
                    info: CreateInvoiceInfo(),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(100m).Success.Get(), NonNegativeAmount.Create(0m).Success.Get(), CreateRevenueInfo(TaxType.Vat0, RevenueType.Products, VatExemptionType.VatIncludedArticle43)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(100m).Success.Get(), NonNegativeAmount.Create(0m).Success.Get(), CreateRevenueInfo(TaxType.Vat0, RevenueType.Services, VatExemptionType.VatIncludedArticle43)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(100m).Success.Get(), NonNegativeAmount.Create(0m).Success.Get(), CreateRevenueInfo(TaxType.Vat0, RevenueType.Other, VatExemptionType.VatIncludedArticle43)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.OnCredit).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    ),
                    counterpart: CreateInvoiceParty(Countries.Greece, "090701900", address: new Address(postalCode: NonEmptyString.CreateUnsafe("12"), city: NonEmptyString.CreateUnsafe("City")))
                ).Success.Get())
            ));
        }

        private static ISequenceStartingWithOne<Invoice> SimplifiedInvoiceForCustomer()
        {
            return SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(SimplifiedInvoice.Create(
                    info: CreateInvoiceInfo(),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(20.50m).Success.Get(), NonNegativeAmount.Create(10.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Products)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(20.50m).Success.Get(), NonNegativeAmount.Create(10.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Services)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(20.50m).Success.Get(), NonNegativeAmount.Create(10.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NonNegativePayment.Create(NonNegativeAmount.Create(31m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(31m).Success.Get(), PaymentType.OnCredit).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(31m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    )
                ).Success.Get())
            ));
        }

        private static ISequenceStartingWithOne<Invoice> CreditInvoiceNonAssociated()
        {
            return SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(CreditInvoice.Create(
                    info: CreateInvoiceInfo(),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NegativeRevenue.Create(NegativeAmount.Create(-88.50m).Success.Get(), NonPositiveAmount.Create(-11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Products)).Success.Get(),
                        NegativeRevenue.Create(NegativeAmount.Create(-88.50m).Success.Get(), NonPositiveAmount.Create(-11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Services)).Success.Get(),
                        NegativeRevenue.Create(NegativeAmount.Create(-88.50m).Success.Get(), NonPositiveAmount.Create(-11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NegativePayment.Create(NegativeAmount.Create(-100m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NegativePayment.Create(NegativeAmount.Create(-100m).Success.Get(), PaymentType.OnCredit).Success.Get(),
                        NegativePayment.Create(NegativeAmount.Create(-100m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    ),
                    counterPart: CreateInvoiceParty(Countries.Greece, "090701900", address: new Address(postalCode: NonEmptyString.CreateUnsafe("12"), city: NonEmptyString.CreateUnsafe("City")))
                ).Success.Get())
            ));
        }

        private static ISequenceStartingWithOne<Invoice> CreditInvoiceNonAssociatedForForeignCompany()
        {
            return SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(CreditInvoice.Create(
                    info: CreateInvoiceInfo(),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NegativeRevenue.Create(NegativeAmount.Create(-88.50m).Success.Get(), NonPositiveAmount.Create(-11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Products)).Success.Get(),
                        NegativeRevenue.Create(NegativeAmount.Create(-88.50m).Success.Get(), NonPositiveAmount.Create(-11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Services)).Success.Get(),
                        NegativeRevenue.Create(NegativeAmount.Create(-88.50m).Success.Get(), NonPositiveAmount.Create(-11.50m).Success.Get(), CreateRevenueInfo(TaxType.Vat13, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NegativePayment.Create(NegativeAmount.Create(-100m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NegativePayment.Create(NegativeAmount.Create(-100m).Success.Get(), PaymentType.OnCredit).Success.Get(),
                        NegativePayment.Create(NegativeAmount.Create(-100m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    ),
                    counterPart: CreateInvoiceParty(Countries.Greece, "090701900", address: new Address(postalCode: NonEmptyString.CreateUnsafe("12"), city: NonEmptyString.CreateUnsafe("City")))
                ).Success.Get())
            ));
        }
    }
}
