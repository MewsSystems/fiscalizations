using Mews.Fiscalization.Core.Model;
using Mews.Fiscalization.Hungary.Models;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Hungary.Tests
{
    [TestFixture]
    public class InvoiceTests
    {
        [Test]
        public async Task SendInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var invoiceTransactionId = await navClient.SendInvoicesAsync(exchangeToken.SuccessResult, Sequence.FromPreordered(new[] { GetInvoice() }, startIndex: 1).Get());

            Thread.Sleep(3000);

            var transactionStatus = await navClient.GetTransactionStatusAsync(invoiceTransactionId.SuccessResult);
            Assert.IsNotNull(transactionStatus.SuccessResult);
            Assert.IsNotNull(exchangeToken.SuccessResult);
            Assert.IsNotNull(invoiceTransactionId.SuccessResult);
            AssertError(transactionStatus);
            AssertError(exchangeToken);
            AssertError(invoiceTransactionId);
        }

        [Test]
        public async Task SendCorrectionInvoiceSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            var response = await navClient.SendModificationDocumentsAsync(
                token: exchangeToken.SuccessResult,
                invoices: Sequence.FromPreordered(new[] { GetModificationInvoice() }, startIndex: 1).Get()
            );

            Thread.Sleep(3000);

            var transactionStatus = await navClient.GetTransactionStatusAsync(response.SuccessResult);
            Assert.IsNotNull(transactionStatus.SuccessResult);
            AssertError(transactionStatus);
        }

        private Invoice GetInvoice()
        {
            var item1Amount = new Amount(new AmountValue(1), new AmountValue(1), new AmountValue(0));
            var item2Amount = new Amount(new AmountValue(20m), new AmountValue(16.81m), new AmountValue(3.19m));
            var unitAmount1 = new ItemAmounts(item1Amount, item2Amount, 0.05m);
            var items = new[]
            {
                new InvoiceItem(
                    consumptionDate: new DateTime(2020, 06, 30),
                    totalAmounts: new ItemAmounts(item1Amount, item1Amount, 0.05m),
                    description: Description.Create("Httt hzi serts (fl)").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: 15,
                    unitAmounts: unitAmount1,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
                new InvoiceItem(
                    consumptionDate: new DateTime(2020, 06, 30),
                    totalAmounts: new ItemAmounts(item2Amount, item2Amount, 0.05m),
                    description: Description.Create("Httt hzi serts (fl)").Success.Get(),
                    measurementUnit: MeasurementUnit.Night,
                    quantity: -15,
                    unitAmounts: unitAmount1,
                    exchangeRate: ExchangeRate.Create(1).Success.Get()
                ),
            };

            var address = GetAddress();
            return new Invoice(
                number: InvoiceNumber.Create("ABC-18a").Success.Get(),
                issueDate: new DateTime(2020, 06, 30),
                supplierInfo: GetSupplierInfo(),
                customerInfo: GetCustomerInfo(),
                items: Sequence.FromPreordered(items, startIndex: 1).Get(),
                paymentDate: new DateTime(2020, 06, 14),
                currencyCode: CurrencyCode.Create("EUR").Success.Get()
            );
        }

        private ModificationInvoice GetModificationInvoice()
        {
            var amounts = GetItemAmounts(amount: 100, exchangeRate: 300);
            var unitAmounts = GetItemAmounts(amount: 100, exchangeRate: 300);
            var item = new InvoiceItem(
                consumptionDate: new DateTime(2020, 08, 30),
                totalAmounts: amounts,
                description: Description.Create("NIGHT 8/30/2020").Success.Get(),
                measurementUnit: MeasurementUnit.Night,
                quantity: -1,
                unitAmounts: unitAmounts,
                exchangeRate: ExchangeRate.Create(300).Success.Get()
            );

            var amounts1 = GetItemAmounts(amount: 100, exchangeRate: 300);
            var unitAmounts1 = GetItemAmounts(amount: 100, exchangeRate: 300);
            var item1 = new InvoiceItem(
                consumptionDate: new DateTime(2020, 08, 31),
                totalAmounts: amounts1,
                description: Description.Create("NIGHT2 8/31/2020").Success.Get(),
                measurementUnit: MeasurementUnit.Night,
                quantity: 1,
                unitAmounts: unitAmounts1,
                exchangeRate: ExchangeRate.Create(300).Success.Get()
            );

            return new ModificationInvoice(
                number: InvoiceNumber.Create("ABC-18abfcefsaa").Success.Get(),
                supplierInfo: GetSupplierInfo(),
                customerInfo: GetCustomerInfo(),
                items: Sequence.FromPreordered(new[] { item, item1 }, startIndex: 1).Get(),
                currencyCode: CurrencyCode.Create("USD").Success.Get(),
                issueDate: new DateTime(2020, 08, 31),
                paymentDate: new DateTime(2020, 08, 31),
                itemIndexOffset: 4,
                modificationIndex: 4,
                modifyWithoutMaster: true,
                originalDocumentNumber: InvoiceNumber.Create("ABC-18afasadafa").Success.Get()
            );
        }

        private CustomerInfo GetCustomerInfo()
        {
            return new CustomerInfo(
                taxpayerId: TaxpayerIdentificationNumber.Create(Countries.Hungary, "14750636").Success.Get(),
                name: Name.Create("Vev Kft").Success.Get(),
                address: GetAddress()
            );
        }

        private SupplierInfo GetSupplierInfo()
        {
            return new SupplierInfo(
                taxpayerId: TaxpayerIdentificationNumber.Create(Countries.Hungary, "14750636").Success.Get(),
                vatCode: VatCode.Create("2").Success.Get(),
                name: Name.Create("BUDAPESTI MSZAKI S GAZDASGTUDOMNYI EGYETEM").Success.Get(),
                address: GetAddress()
            );
        }

        private SimpleAddress GetAddress()
        {
            return new SimpleAddress(
                city: City.Create("Budapest").Success.Get(),
                country: Countries.Hungary,
                additionalAddressDetail: AdditionalAddressDetail.Create("Test").Success.Get(),
                postalCode: PostalCode.Create("1111").Success.Get()
            );
        }

        private ItemAmounts GetItemAmounts(decimal amount, decimal exchangeRate = 1)
        {
            var amountHUF = amount * exchangeRate;
            return new ItemAmounts(
                amount: new Amount(
                    net: new AmountValue(amount),
                    gross: new AmountValue(amount),
                    tax: new AmountValue(amount)
                ),
                amountHUF: new Amount(
                    net: new AmountValue(amountHUF),
                    gross: new AmountValue(amountHUF),
                    tax: new AmountValue(amountHUF)
                )
            );
        }

        private void AssertError<TResult, TCode>(ResponseResult<TResult, TCode> result)
            where TResult : class
            where TCode : struct
        {
            Assert.IsNull(result.OperationalErrorResult);
            Assert.IsNull(result.GeneralErrorResult);
        }
    }
}
