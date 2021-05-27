using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Hungary.Tests
{
    [TestFixture]
    public sealed class InvoiceTests
    {
        [Test]
        public async Task SendCustomerInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var receiver = new Receiver(new Customer());
            var responseResult = await SendInvoices(navClient, receiver);
            TestFixture.AssertResponse(responseResult);

            Thread.Sleep(2000);

            var transactionId = responseResult.SuccessResult;
            var transactionStatus = await navClient.GetTransactionStatusAsync(transactionId);
            TestFixture.AssertResponse(transactionStatus);
            AssertInvoiceStatuses(transactionStatus.SuccessResult.InvoiceStatuses);
        }

        [Test]
        public async Task SendLocalCompanyInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var localCompany = LocalCompany.Create(
                taxpayerId: TaxpayerIdentificationNumber.Create(Countries.Hungary, "99999999").Success.Get(),
                name: Name.Create("Hungarian test company ltd.").Success.Get(),
                address: CreateAddress(Countries.Hungary)
            );
            var receiver = new Receiver(new Company(localCompany.Success.Get()));
            var responseResult = await SendInvoices(navClient, receiver);
            TestFixture.AssertResponse(responseResult);

            Thread.Sleep(2000);

            var transactionId = responseResult.SuccessResult;
            var transactionStatus = await navClient.GetTransactionStatusAsync(transactionId);
            TestFixture.AssertResponse(transactionStatus);
            AssertInvoiceStatuses(transactionStatus.SuccessResult.InvoiceStatuses);
        }

        [Test]
        [TestCase("CZ", "CZ12345678")]
        [TestCase("US", "UsTaxId")]
        [TestCase("CZ", null)]
        [TestCase("US", null)]
        public async Task SendForeignCompanyInvoiceSucceeds(string countryCode, string taxId)
        {
            var navClient = TestFixture.GetNavClient();
            var country = Countries.GetByCode(countryCode).Get();
            var taxpayerNumber = taxId.ToNonEmptyOption().Map(i => TaxpayerIdentificationNumber.Create(country, i).Success.Get());
            var foreignCompany = ForeignCompany.Create(
                name: Name.Create("Foreign test company ltd.").Success.Get(),
                address: CreateAddress(country),
                taxpayerId: taxpayerNumber.GetOrNull()
            );
            var receiver = new Receiver(new Company(foreignCompany.Success.Get()));
            var responseResult = await SendInvoices(navClient, receiver);
            TestFixture.AssertResponse(responseResult);

            Thread.Sleep(2000);

            var transactionId = responseResult.SuccessResult;
            var transactionStatus = await navClient.GetTransactionStatusAsync(transactionId);
            TestFixture.AssertResponse(transactionStatus);
            AssertInvoiceStatuses(transactionStatus.SuccessResult.InvoiceStatuses);
        }

        [Test, Order(1)]
        public async Task SendCorrectionCustomerInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var receiver = new Receiver(new Customer());
            var invoice = CreateInvoice(receiver);
            var sendInvoiceResponse = await navClient.SendInvoicesAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { invoice }, startIndex: 1).Get()
            );

            Thread.Sleep(2000);

            var sendInvoiceTransactionStatus = await navClient.GetTransactionStatusAsync(sendInvoiceResponse.SuccessResult);
            TestFixture.AssertResponse(sendInvoiceTransactionStatus);
            AssertInvoiceStatuses(sendInvoiceTransactionStatus.SuccessResult.InvoiceStatuses);

            var sendModificationInvoiceResponse = await navClient.SendModificationDocumentsAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateModificationInvoice(invoice.Number, receiver) }, startIndex: 1).Get()
            );

            Thread.Sleep(2000);

            var sendModificationInvoiceTransactionStatus = await navClient.GetTransactionStatusAsync(sendModificationInvoiceResponse.SuccessResult);
            TestFixture.AssertResponse(sendModificationInvoiceTransactionStatus);
            AssertInvoiceStatuses(sendModificationInvoiceTransactionStatus.SuccessResult.InvoiceStatuses);
        }

        [Test, Order(1)]
        public async Task SendCorrectionLocalCompanyInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var localCompany = LocalCompany.Create(
                taxpayerId: TaxpayerIdentificationNumber.Create(Countries.Hungary, "99999999").Success.Get(),
                name: Name.Create("Hungarian test company ltd.").Success.Get(),
                address: CreateAddress(Countries.Hungary)
            );
            var receiver = new Receiver(new Company(localCompany.Success.Get()));
            var invoice = CreateInvoice(receiver);
            var sendInvoiceResponse = await navClient.SendInvoicesAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { invoice }, startIndex: 1).Get()
            );

            Thread.Sleep(2000);

            var sendInvoiceTransactionStatus = await navClient.GetTransactionStatusAsync(sendInvoiceResponse.SuccessResult);
            TestFixture.AssertResponse(sendInvoiceTransactionStatus);
            AssertInvoiceStatuses(sendInvoiceTransactionStatus.SuccessResult.InvoiceStatuses);

            var sendModificationInvoiceResponse = await navClient.SendModificationDocumentsAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateModificationInvoice(invoice.Number, receiver) }, startIndex: 1).Get()
            );

            Thread.Sleep(2000);

            var sendModificationInvoiceTransactionStatus = await navClient.GetTransactionStatusAsync(sendModificationInvoiceResponse.SuccessResult);
            TestFixture.AssertResponse(sendModificationInvoiceTransactionStatus);
            AssertInvoiceStatuses(sendModificationInvoiceTransactionStatus.SuccessResult.InvoiceStatuses);
        }

        [Test, Order(1)]
        [TestCase("CZ", "CZ12345678")]
        [TestCase("US", "UsTaxId")]
        [TestCase("CZ", null)]
        [TestCase("US", null)]
        public async Task SendCorrectionForeignCompanyInvoiceSucceeds(string countryCode, string taxId)
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var country = Countries.GetByCode(countryCode).Get();
            var taxpayerNumber = taxId.ToNonEmptyOption().Map(i => TaxpayerIdentificationNumber.Create(country, i).Success.Get());
            var foreignCompany = ForeignCompany.Create(
                name: Name.Create("Foreign test company ltd.").Success.Get(),
                address: CreateAddress(country),
                taxpayerId: taxpayerNumber.GetOrNull()
            );
            var receiver = new Receiver(new Company(foreignCompany.Success.Get()));
            var invoice = CreateInvoice(receiver);
            var sendInvoiceResponse = await navClient.SendInvoicesAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { invoice }, startIndex: 1).Get()
            );

            Thread.Sleep(2000);

            var sendInvoiceTransactionStatus = await navClient.GetTransactionStatusAsync(sendInvoiceResponse.SuccessResult);
            TestFixture.AssertResponse(sendInvoiceTransactionStatus);
            AssertInvoiceStatuses(sendInvoiceTransactionStatus.SuccessResult.InvoiceStatuses);

            var sendModificationInvoiceResponse = await navClient.SendModificationDocumentsAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateModificationInvoice(invoice.Number, receiver) }, startIndex: 1).Get()
            );

            Thread.Sleep(2000);

            var sendModificationInvoiceTransactionStatus = await navClient.GetTransactionStatusAsync(sendModificationInvoiceResponse.SuccessResult);
            TestFixture.AssertResponse(sendModificationInvoiceTransactionStatus);
            AssertInvoiceStatuses(sendModificationInvoiceTransactionStatus.SuccessResult.InvoiceStatuses);
        }

        private async Task<ResponseResult<string, ResultErrorCode>> SendInvoices(NavClient client, Receiver receiver)
        {
            var exchangeToken = await client.GetExchangeTokenAsync();
            return await client.SendInvoicesAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateInvoice(receiver) }, startIndex: 1).Get()
            );
        }

        private Invoice CreateInvoice(Receiver receiver)
        {
            var item1Amount = new Amount(net: new AmountValue(1694.92m), gross: new AmountValue(2000), tax: new AmountValue(305.08m));
            var item2Amount = new Amount(new AmountValue(2362.20m), new AmountValue(3000), new AmountValue(637.8m));
            var item3Amount = new Amount(new AmountValue(952.38m), new AmountValue(1000), new AmountValue(47.62m));
            var unitAmount1 = new ItemAmounts(item1Amount, item1Amount, 0.18m);
            var unitAmount2 = new ItemAmounts(item2Amount, item2Amount, 0.27m);
            var unitAmount3 = new ItemAmounts(item3Amount, item3Amount, 0.05m);
            var items = new[]
            {
                new InvoiceItem(
                    consumptionDate: DateTime.UtcNow.Date,
                    totalAmounts: new ItemAmounts(item1Amount, item1Amount, 0.18m),
                    description: Description.Create("Item 1 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 1,
                    unitAmounts: unitAmount1,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: DateTime.UtcNow.Date,
                    totalAmounts: new ItemAmounts(item2Amount, item2Amount, 0.27m),
                    description: Description.Create("Item 2 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 1,
                    unitAmounts: unitAmount2,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: DateTime.UtcNow.Date,
                    totalAmounts: new ItemAmounts(item3Amount, item3Amount, 0.05m),
                    description: Description.Create("Item 3 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 1,
                    unitAmounts: unitAmount3,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                )
            };

            var number = new Random().Next(1, 10000);
            return new Invoice(
                number: InvoiceNumber.Create($"INVOICE-{number}").Success.Get(),
                issueDate: DateTime.UtcNow.Date,
                supplierInfo: CreateSupplierInfo(),
                receiver: receiver,
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                paymentDate: DateTime.UtcNow.Date,
                currencyCode: CurrencyCode.Create("EUR").Success.Get()
            );
        }

        private ModificationInvoice CreateModificationInvoice(InvoiceNumber originalDocumentNumber, Receiver receiver)
        {
            var item1Amount = new Amount(net: new AmountValue(-1694.92m), gross: new AmountValue(-2000), tax: new AmountValue(-305.08m));
            var item2Amount = new Amount(new AmountValue(-2362.20m), new AmountValue(-3000), new AmountValue(-637.8m));
            var item3Amount = new Amount(new AmountValue(-952.38m), new AmountValue(-1000), new AmountValue(-47.62m));
            var unitAmount1 = new ItemAmounts(item1Amount, item1Amount, 0.18m);
            var unitAmount2 = new ItemAmounts(item2Amount, item2Amount, 0.27m);
            var unitAmount3 = new ItemAmounts(item3Amount, item3Amount, 0.05m);
            var items = new[]
            {
                new InvoiceItem(
                    consumptionDate: DateTime.UtcNow.Date,
                    totalAmounts: new ItemAmounts(item1Amount, item1Amount, 0.18m),
                    description: Description.Create("Item 1 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -1,
                    unitAmounts: unitAmount1,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: DateTime.UtcNow.Date,
                    totalAmounts: new ItemAmounts(item2Amount, item2Amount, 0.27m),
                    description: Description.Create("Item 2 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -1,
                    unitAmounts: unitAmount2,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: DateTime.UtcNow.Date,
                    totalAmounts: new ItemAmounts(item3Amount, item3Amount, 0.05m),
                    description: Description.Create("Item 3 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -1,
                    unitAmounts: unitAmount3,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                )
            };

            var number = new Random().Next(1, 10000);
            return new ModificationInvoice(
                number: InvoiceNumber.Create($"INVOICE-{number}-REBATE").Success.Get(),
                supplierInfo: CreateSupplierInfo(),
                receiver: receiver,
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                currencyCode: CurrencyCode.Create("EUR").Success.Get(),
                issueDate: DateTime.UtcNow.Date,
                paymentDate: DateTime.UtcNow.Date,
                itemIndexOffset: 3,
                modificationIndex: 1,
                modifyWithoutMaster: true,
                originalDocumentNumber: originalDocumentNumber
            );
        }

        private SupplierInfo CreateSupplierInfo()
        {
            return new SupplierInfo(
                taxpayerId: TestFixture.TaxPayerId,
                vatCode: VatCode.Create("2").Success.Get(),
                name: Name.Create("BUDAPESTI MSZAKI S GAZDASGTUDOMNYI EGYETEM").Success.Get(),
                address: CreateAddress(Countries.Hungary)
            );
        }

        private SimpleAddress CreateAddress(Country country)
        {
            return new SimpleAddress(
                city: City.Create("Budapest").Success.Get(),
                country: country,
                additionalAddressDetail: AdditionalAddressDetail.Create("Additional address detail").Success.Get(),
                postalCode: PostalCode.Create("1111").Success.Get()
            );
        }

        private void AssertInvoiceStatuses(IEnumerable<Indexed<InvoiceStatus>> invoiceStatuses)
        {
            foreach (var status in invoiceStatuses)
            {
                var value = status.Value;
                Assert.AreEqual(value.Status, InvoiceState.Done);
                Assert.IsEmpty(value.ValidationResults);
            }
        }
    }
}
