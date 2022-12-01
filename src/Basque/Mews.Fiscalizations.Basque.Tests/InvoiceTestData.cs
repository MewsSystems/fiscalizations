using FuncSharp;
using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Linq;

namespace Mews.Fiscalizations.Basque.Tests
{
    internal sealed class InvoiceTestData
    {
        internal static SendInvoiceRequest CreateInvoiceRequest(Issuer issuer, Software software, bool localReceivers, bool negativeInvoice, OriginalInvoiceInfo originalInvoiceInfo = null)
        {
            return new SendInvoiceRequest(
                subject: CreateSubject(issuer, localReceivers),
                invoice: CreateInvoice(localReceivers, negativeInvoice),
                invoiceFooter: new InvoiceFooter(software, originalInvoiceInfo: originalInvoiceInfo)
            );
        }

        private static Invoice CreateInvoice(bool localReceivers, bool negativeInvoice)
        {
            return new Invoice(CreateHeader(), CreateInvoiceData(negativeInvoice), CreateTaxBreakdown(localReceivers, negativeInvoice));
        }

        private static TaxBreakdown CreateTaxBreakdown(bool localReceivers, bool negativeInvoice)
        {
            var baseValue = negativeInvoice.Match(
                t => -73.86m,
                f => 73.86m
            );
            var taxSummary = TaxSummary.Create(taxed: CreateTaxRateSummary(21m, baseValue).ToEnumerable().ToArray()).Success.Get();
            return localReceivers.Match(
                t => new TaxBreakdown(taxSummary),
                f => new TaxBreakdown(OperationTypeTaxBreakdown.Create(delivery: taxSummary).Success.Get())
            );
        }

        private static InvoiceData CreateInvoiceData(bool negativeInvoice)
        {
            return InvoiceData.Create(
                description: String1To250.CreateUnsafe("TicketBAI sample invoice test."),
                items: CreateInvoiceItems(negativeInvoice),
                totalAmount: negativeInvoice.Match(t => -89.36m, f => 89.36m),
                taxModes: TaxMode.GeneralTaxRegimeActivity.ToEnumerable(),
                transactionDate: DateTime.Now
            ).Success.Get();
        }

        private static INonEmptyEnumerable<InvoiceItem> CreateInvoiceItems(bool negativeInvoice)
        {
            return negativeInvoice.Match(
                t => NonEmptyEnumerable.Create(
                    new InvoiceItem(
                        description: String1To250.CreateUnsafe("Night 1"),
                        quantity: 1,
                        unitAmount: -23.356m,
                        discount: -2.00m,
                        totalAmount: -25.84m
                    ),
                    new InvoiceItem(
                        description: String1To250.CreateUnsafe("Night 2"),
                        quantity: 1.50m,
                        unitAmount: -18.2m,
                        totalAmount: -33.03m
                    ),
                    new InvoiceItem(
                        description: String1To250.CreateUnsafe("Parking"),
                        quantity: 18,
                        unitAmount: -1.40m,
                        totalAmount: -30.49m
                    )
                ),
                f => NonEmptyEnumerable.Create(
                    new InvoiceItem(
                        description: String1To250.CreateUnsafe("Night 1"),
                        quantity: 1,
                        unitAmount: 23.356m,
                        discount: 2.00m,
                        totalAmount: 25.84m
                    ),
                    new InvoiceItem(
                        description: String1To250.CreateUnsafe("Night 2"),
                        quantity: 1.50m,
                        unitAmount: 18.2m,
                        totalAmount: 33.03m
                    ),
                    new InvoiceItem(
                        description: String1To250.CreateUnsafe("Parking"),
                        quantity: 18,
                        unitAmount: 1.40m,
                        totalAmount: 30.49m
                    )
                )
            );
        }

        private static InvoiceHeader CreateHeader()
        {
            var randomString = String1To20.CreateUnsafe(Guid.NewGuid().ToString().Substring(0, 19));
            return new InvoiceHeader(number: randomString, issued: DateTime.Now, series: randomString);
        }

        private static Subject CreateSubject(Issuer issuer, bool localReceivers)
        {
            return Subject.Create(issuer, CreateReceivers(localReceivers), IssuerType.IssuedByThirdParty).Success.Get();
        }

        private static INonEmptyEnumerable<Receiver> CreateReceivers(bool localReceivers)
        {
            return NonEmptyEnumerable.Create(localReceivers.Match(
                t => Receiver.Local(
                    nif: "11111111H",
                    name: Name.CreateUnsafe("Mike The Local"),
                    postalCode: PostalCode.CreateUnsafe("08013"),
                    address: String1To250.CreateUnsafe("C/ de Mallorca, 401, Barcelona")
                ).Success.Get(),
                f => Receiver.Foreign(
                    idType: IdType.Passport,
                    id: String1To20.CreateUnsafe("ABCDEF123"),
                    name: Name.CreateUnsafe("John The Forienger"),
                    postalCode: PostalCode.CreateUnsafe("12345678912345678BBA"),
                    address: String1To250.CreateUnsafe("Prague, Italska 2502/555"),
                    country: Countries.CzechRepublic
                )
            ));
        }

        private static TaxRateSummary CreateTaxRateSummary(decimal vat, decimal baseValue)
        {
            return new TaxRateSummary(
                taxRatePercentage: Percentage.Create(vat).Success.Get(),
                taxBaseAmount: Amount.Create(baseValue).Success.Get(),
                taxAmount: Amount.Create(Math.Round(baseValue * vat / 100, 2)).Success.Get()
            );
        }
    }
}
