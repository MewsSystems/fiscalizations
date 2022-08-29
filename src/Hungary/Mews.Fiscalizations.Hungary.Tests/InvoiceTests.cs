using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Mews.Fiscalizations.Hungary.Tests
{
    [TestFixture]
    public sealed class InvoiceTests
    {
        private static readonly NavClient NavClient = TestFixture.GetNavClient();

        private const int RetryCount = 3;

        [Test]
        [Retry(RetryCount)]
        public async Task SendCustomerInvoiceSucceeds()
        {
            var receiver = Receiver.Customer();
            var sendInvoicesResult = await SendInvoices(receiver);
            await AssertInvoices(sendInvoicesResult);
        }

        [Test]
        [Retry(RetryCount)]
        public async Task SendLocalCompanyInvoiceSucceeds()
        {
            var receiver = Receiver.LocalCompany(
                taxpayerId: "10630433",
                name: Models.Name.Create("Hungarian test company ltd.").Success.Get(),
                address: CreateAddress(Countries.Hungary)
            );
            var sendInvoicesResult = await SendInvoices(receiver.Success.Get());
            await AssertInvoices(sendInvoicesResult);
        }

        [Test]
        [Retry(RetryCount)]
        [TestCase("CZ", "CZ12345678")]
        [TestCase("US", "UsTaxId")]
        [TestCase("CZ", null)]
        [TestCase("US", null)]
        public async Task SendForeignCompanyInvoiceSucceeds(string countryCode, string taxId)
        {
            var country = Countries.GetByCode(countryCode).Get();
            var taxpayerNumber = taxId.ToNonEmptyOption().Map(i => TaxpayerIdentificationNumber.Create(country, i).Success.Get());
            var receiver = Receiver.ForeignCompany(
                name: Models.Name.Create("Foreign test company ltd.").Success.Get(),
                address: CreateAddress(country),
                taxpayerId: taxpayerNumber.GetOrNull()
            );
            var sendInvoicesResult = await SendInvoices(receiver.Success.Get());
            await AssertInvoices(sendInvoicesResult);
        }

        [Test, Order(1)]
        [Retry(RetryCount)]
        public async Task SendCorrectionCustomerInvoiceSucceeds()
        {
            var receiver = Receiver.Customer();
            var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
            var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
            await AssertInvoices(sendInvoicesResult);

            var sendModificationInvoicesResult = await SendModificationInvoices(receiver, originalInvoiceNumber: invoiceNumber);
            await AssertInvoices(sendModificationInvoicesResult);
        }

        [Test, Order(1)]
        [Retry(RetryCount)]
        public async Task SendCorrectionLocalCompanyInvoiceSucceeds()
        {
            var receiver = Receiver.LocalCompany(
                taxpayerId: "10630433",
                name: Models.Name.Create("Hungarian test company ltd.").Success.Get(),
                address: CreateAddress(Countries.Hungary)
            ).Success.Get();
            var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
            var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
            await AssertInvoices(sendInvoicesResult);

            var sendModificationInvoiceResponse = await SendModificationInvoices(receiver, originalInvoiceNumber: invoiceNumber);
            await AssertInvoices(sendModificationInvoiceResponse);
        }

        [Test, Order(1)]
        [Retry(RetryCount)]
        [TestCase("CZ", "CZ12345678")]
        [TestCase("US", "UsTaxId")]
        [TestCase("CZ", null)]
        [TestCase("US", null)]
        public async Task SendCorrectionForeignCompanyInvoiceSucceeds(string countryCode, string taxId)
        {
            var country = Countries.GetByCode(countryCode).Get();
            var taxpayerNumber = taxId.ToNonEmptyOption().Map(i => TaxpayerIdentificationNumber.Create(country, i).Success.Get());
            var receiver = Receiver.ForeignCompany(
                name: Models.Name.Create("Foreign test company ltd.").Success.Get(),
                address: CreateAddress(country),
                taxpayerId: taxpayerNumber.GetOrNull()
            ).Success.Get();
            var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
            var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
            await AssertInvoices(sendInvoicesResult);

            var sendModificationInvoiceResponse = await SendModificationInvoices(receiver, originalInvoiceNumber: invoiceNumber);
            await AssertInvoices(sendModificationInvoiceResponse);
        }

        private async Task<ResponseResult<string, ResultErrorCode>> SendInvoices(Receiver receiver, InvoiceNumber invoiceNumber = null)
        {
            var exchangeToken = await NavClient.GetExchangeTokenAsync();
            return await NavClient.SendInvoicesAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateInvoice(receiver, invoiceNumber) }, startIndex: 1).Get()
            );
        }

        private async Task<ResponseResult<string, ResultErrorCode>> SendModificationInvoices(Receiver receiver, InvoiceNumber originalInvoiceNumber)
        {
            var exchangeToken = await NavClient.GetExchangeTokenAsync();
            return await NavClient.SendModificationDocumentsAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateModificationInvoice(originalInvoiceNumber, receiver) }, startIndex: 1).Get()
            );
        }

        private Invoice CreateInvoice(Receiver receiver, InvoiceNumber invoiceNumber = null)
        {
            var nowUtc = DateTime.UtcNow.Date;
            var item1Amount = new Models.Amount(net: new AmountValue(1694.92m), gross: new AmountValue(2000), tax: new AmountValue(305.08m));
            var item2Amount = new Models.Amount(new AmountValue(2362.20m), new AmountValue(3000), new AmountValue(637.8m));
            var item3Amount = new Models.Amount(new AmountValue(952.38m), new AmountValue(1000), new AmountValue(47.62m));
            var unitAmount1 = new ItemAmounts(item1Amount, item1Amount, 0.18m);
            var unitAmount2 = new ItemAmounts(item2Amount, item2Amount, 0.27m);
            var unitAmount3 = new ItemAmounts(item3Amount, item3Amount, 0.05m);
            var items = new[]
            {
                new InvoiceItem(
                    consumptionDate: nowUtc,
                    totalAmounts: new ItemAmounts(item1Amount, item1Amount, 0.18m),
                    description: Description.Create("Item 1 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 1,
                    unitAmounts: unitAmount1,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: nowUtc,
                    totalAmounts: new ItemAmounts(item2Amount, item2Amount, 0.27m),
                    description: Description.Create("Item 2 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 1,
                    unitAmounts: unitAmount2,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: nowUtc,
                    totalAmounts: new ItemAmounts(item3Amount, item3Amount, 0.05m),
                    description: Description.Create("Item 3 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 1,
                    unitAmounts: unitAmount3,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                )
            };

            return new Invoice(
                number: invoiceNumber ?? InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get(),
                issueDate: nowUtc,
                supplierInfo: CreateSupplierInfo(),
                receiver: receiver,
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                paymentDate: nowUtc,
                currencyCode: CurrencyCode.Create("EUR").Success.Get(),
                paymentMethod: PaymentMethod.Card
            );
        }

        private ModificationInvoice CreateModificationInvoice(InvoiceNumber originalDocumentNumber, Receiver receiver)
        {
            var nowUtc = DateTime.UtcNow.Date;
            var item1Amount = new Models.Amount(net: new AmountValue(-1694.92m), gross: new AmountValue(-2000), tax: new AmountValue(-305.08m));
            var item2Amount = new Models.Amount(new AmountValue(-2362.20m), new AmountValue(-3000), new AmountValue(-637.8m));
            var item3Amount = new Models.Amount(new AmountValue(-952.38m), new AmountValue(-1000), new AmountValue(-47.62m));
            var unitAmount1 = new ItemAmounts(item1Amount, item1Amount, 0.18m);
            var unitAmount2 = new ItemAmounts(item2Amount, item2Amount, 0.27m);
            var unitAmount3 = new ItemAmounts(item3Amount, item3Amount, 0.05m);
            var items = new[]
            {
                new InvoiceItem(
                    consumptionDate: nowUtc,
                    totalAmounts: new ItemAmounts(item1Amount, item1Amount, 0.18m),
                    description: Description.Create("Item 1 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -1,
                    unitAmounts: unitAmount1,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: nowUtc,
                    totalAmounts: new ItemAmounts(item2Amount, item2Amount, 0.27m),
                    description: Description.Create("Item 2 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -1,
                    unitAmounts: unitAmount2,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: nowUtc,
                    totalAmounts: new ItemAmounts(item3Amount, item3Amount, 0.05m),
                    description: Description.Create("Item 3 description").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -1,
                    unitAmounts: unitAmount3,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                )
            };

            return new ModificationInvoice(
                number: InvoiceNumber.Create($"REBATE-{Guid.NewGuid()}").Success.Get(),
                supplierInfo: CreateSupplierInfo(),
                receiver: receiver,
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                currencyCode: CurrencyCode.Create("EUR").Success.Get(),
                issueDate: nowUtc,
                paymentDate: nowUtc,
                itemIndexOffset: 3,
                modificationIndex: 1,
                modifyWithoutMaster: false,
                originalDocumentNumber: originalDocumentNumber,
                paymentMethod: PaymentMethod.Cash
            );
        }

        private SupplierInfo CreateSupplierInfo()
        {
            return new SupplierInfo(
                taxpayerId: TestFixture.TaxPayerId,
                vatCode: VatCode.Create("2").Success.Get(),
                name: Models.Name.Create("Supplier company").Success.Get(),
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

        private async Task AssertInvoices(ResponseResult<string, ResultErrorCode> sendInvoicesResults)
        {
            TestFixture.AssertResponse(sendInvoicesResults);

            Thread.Sleep(2000);

            var transactionId = sendInvoicesResults.SuccessResult;
            var transactionStatus = await NavClient.GetTransactionStatusAsync(transactionId);
            TestFixture.AssertResponse(transactionStatus);

            var invoiceStatuses = transactionStatus.SuccessResult.InvoiceStatuses;
            foreach (var status in invoiceStatuses)
            {
                var value = status.Value;
                Assert.AreEqual(value.Status, InvoiceState.Done);
            }

            var errorValidationResults = invoiceStatuses.SelectMany(s => s.Value.ValidationResults).Where(r => r.ResultCode.Equals(ValidationResultCode.Error));
            Assert.IsEmpty(errorValidationResults, "Response contains validation errors.", new
            {
                Message = String.Join(", ", errorValidationResults.Select(r => r.Message)),
                Code = String.Join(", ", errorValidationResults.Select(r => r.ResultCode.ToString()))
            });
        }
    }
}
