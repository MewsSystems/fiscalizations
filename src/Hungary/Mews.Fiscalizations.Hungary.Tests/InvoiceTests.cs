using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Hungary.Tests
{
    [TestFixture]
    public class InvoiceTests
    {
        private static readonly Random random = new Random();
        private int Number = random.Next(1, 10000);

        [Test, Order(0)]
        [TestCase("HU", "14750636", CustomerVatStatusType.Domestic)]

        [TestCase("HU", "14750636", CustomerVatStatusType.Other)]
        [TestCase("CZ", "CZ12345678", CustomerVatStatusType.Other)]
        [TestCase("US", "UsTaxId", CustomerVatStatusType.Other)]

        [TestCase("HU", "10630433", CustomerVatStatusType.PrivatePerson)]
        [TestCase("CZ", "CZ12345678", CustomerVatStatusType.PrivatePerson)]
        [TestCase("US", "UsTaxId", CustomerVatStatusType.PrivatePerson)]

        [TestCase("HU", null, CustomerVatStatusType.Other)]
        [TestCase("CZ", null, CustomerVatStatusType.Other)]
        [TestCase("US", null, CustomerVatStatusType.Other)]

        [TestCase("HU", null, CustomerVatStatusType.PrivatePerson)]
        [TestCase("CZ", null, CustomerVatStatusType.PrivatePerson)]
        [TestCase("US", null, CustomerVatStatusType.PrivatePerson)]
        public async Task SendInvoiceSucceeds(string countryCode, string taxId, CustomerVatStatusType type)
        {
            Number = random.Next(1, 10000);

            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var responseResult = await navClient.SendInvoicesAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateInvoice(countryCode, taxId, type) }, startIndex: 1).Get()
            );

            TestFixture.AssertResponse(responseResult);

            Thread.Sleep(2000);

            var transactionId = responseResult.SuccessResult;
            var transactionStatus = await navClient.GetTransactionStatusAsync(transactionId);

            TestFixture.AssertResponse(transactionStatus);

            var value = transactionStatus.SuccessResult.InvoiceStatuses.First().Value;
            Assert.AreEqual(value.Status, InvoiceState.Done);
            // Disabled till we have a tax id that works (other than the supplier tax id).
            //Assert.IsEmpty(value.ValidationResults);
        }

        [Test, Order(1)]
        public async Task SendCorrectionInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var response = await navClient.SendModificationDocumentsAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { CreateModificationInvoice() }, startIndex: 1).Get()
            );

            Thread.Sleep(3000);

            var transactionStatus = await navClient.GetTransactionStatusAsync(response.SuccessResult);

            TestFixture.AssertResponse(transactionStatus);

            var value = transactionStatus.SuccessResult.InvoiceStatuses.First().Value;
            Assert.AreEqual(value.Status, InvoiceState.Done);

            // Disabled till we have a tax id that works (other than the supplier tax id).
            //Assert.IsEmpty(value.ValidationResults);
        }

        private Invoice CreateInvoice(string countryCode, string taxId, CustomerVatStatusType type)
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

            return new Invoice(
                number: InvoiceNumber.Create($"INVOICE-{Number}").Success.Get(),
                issueDate: DateTime.UtcNow.Date,
                supplierInfo: CreateSupplierInfo(),
                customerInfo: CreateCustomerInfo(countryCode, taxId, type),
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                paymentDate: DateTime.UtcNow.Date,
                currencyCode: CurrencyCode.Create("EUR").Success.Get()
            );
        }

        private ModificationInvoice CreateModificationInvoice()
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

            return new ModificationInvoice(
                number: InvoiceNumber.Create($"INVOICE-{Number}-REBATE").Success.Get(),
                supplierInfo: CreateSupplierInfo(),
                customerInfo: CreateCustomerInfo("HU", "10630433", CustomerVatStatusType.Domestic),
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                currencyCode: CurrencyCode.Create("EUR").Success.Get(),
                issueDate: DateTime.UtcNow.Date,
                paymentDate: DateTime.UtcNow.Date,
                itemIndexOffset: 3,
                modificationIndex: 1,
                modifyWithoutMaster: true,
                originalDocumentNumber: InvoiceNumber.Create($"INVOICE-{Number}").Success.Get()
            );
        }

        private CustomerInfo CreateCustomerInfo(string countryCode, string taxId, CustomerVatStatusType type)
        {
            if (type == CustomerVatStatusType.PrivatePerson)
            {
                return new CustomerInfo(type: type);
            }
            var country = Countries.GetByCode(countryCode).Get();
            var taxpayerId = taxId.ToNonEmptyOption().Map(i => TaxpayerIdentificationNumber.Create(country, i).Success.Get());
            return new CustomerInfo(
                taxpayerId: taxpayerId.GetOrNull(),
                name: Name.Create("Vev Kft").Success.Get(),
                address: CreateAddress(country),
                type: type
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
    }
}
