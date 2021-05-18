using Mews.Fiscalization.Core.Model;
using Mews.Fiscalization.Greece.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mews.Fiscalization.Greece.Tests.IntegrationTests
{
    public class AadeClientTests
    {
        private static readonly string UserId = Environment.GetEnvironmentVariable("user_id") ?? "INSERT_USER_ID";
        private static readonly string UserSubscriptionKey = Environment.GetEnvironmentVariable("user_subscription_key") ?? "INSERT_SUBSCRIPTION_KEY";
        private static readonly string UserVatNumber = Environment.GetEnvironmentVariable("user_vat_number") ?? "INSERT_USER_VAT_NUMBER";

        [Fact]
        public async Task CheckUserCredentials()
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.CheckUserCredentialsAsync();

            // Assert
            Assert.True(response.IsSuccess);
            Assert.True(response.Success.IsAuthorized);
        }

        [Theory]
        [MemberData(nameof(AadeTestInvoicesData.GetInvoices), MemberType = typeof(AadeTestInvoicesData))]
        public async Task ValidInvoicesWork(ISequenceStartingWithOne<Invoice> invoices)
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.SendInvoicesAsync(invoices);

            // Assert
            Assert.True(response.SendInvoiceResults.IsSuccess);
            Assert.All(response.SendInvoiceResults.Success.Get().Values, result => Assert.True(result.Value.IsSuccess));
        }

        [Fact]
        public async Task ValidNegativeInvoiceWorks()
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act

            // Step 1 - regular invoice
            var country = Countries.Greece;
            var invoices = SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(SalesInvoice.Create(
                    info: AadeTestInvoicesData.CreateInvoiceInfo(invoiceSerialNumber: "50021"),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(88.50m).Success.Get(), NonNegativeAmount.Create(11.50m).Success.Get(), AadeTestInvoicesData.CreateRevenueInfo(TaxType.Vat13, RevenueType.Products)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(88.50m).Success.Get(), NonNegativeAmount.Create(11.50m).Success.Get(), AadeTestInvoicesData.CreateRevenueInfo(TaxType.Vat13, RevenueType.Services)).Success.Get(),
                        NonNegativeRevenue.Create(NonNegativeAmount.Create(88.50m).Success.Get(), NonNegativeAmount.Create(11.50m).Success.Get(), AadeTestInvoicesData.CreateRevenueInfo(TaxType.Vat13, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.OnCredit).Success.Get(),
                        NonNegativePayment.Create(NonNegativeAmount.Create(100m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    ),
                    counterpart: AadeTestInvoicesData.CreateInvoiceParty(country, "090701900")
                ).Success.Get())
            ));

            var response = await client.SendInvoicesAsync(invoices);

            Assert.True(response.SendInvoiceResults.IsSuccess);
            Assert.All(response.SendInvoiceResults.Success.Get().Values, result => Assert.True(result.Value.IsSuccess));

            // We need to wait some time to allow external system to store the mark from the first call
            await Task.Delay(1000);

            // Step 2 - negative invoice
            var correlatedInvoice = response.SendInvoiceResults.Success.Get().Values.First().Value.Success.InvoiceRegistrationNumber.Value;

            var negativeInvoice = SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                new Invoice(CreditInvoice.Create(
                    correlatedInvoice: correlatedInvoice,
                    info: AadeTestInvoicesData.CreateInvoiceInfo(invoiceSerialNumber: "50021"),
                    revenueItems: SequenceStartingWithOne.FromPreordered(NonEmptyEnumerable.Create(
                        NegativeRevenue.Create(NegativeAmount.Create(-53.65m).Success.Get(), NonPositiveAmount.Create(-12.88m).Success.Get(), AadeTestInvoicesData.CreateRevenueInfo(TaxType.Vat6, RevenueType.Products)).Success.Get(),
                        NegativeRevenue.Create(NegativeAmount.Create(-53.65m).Success.Get(), NonPositiveAmount.Create(-12.88m).Success.Get(), AadeTestInvoicesData.CreateRevenueInfo(TaxType.Vat6, RevenueType.Services)).Success.Get(),
                        NegativeRevenue.Create(NegativeAmount.Create(-53.65m).Success.Get(), NonPositiveAmount.Create(-12.88m).Success.Get(), AadeTestInvoicesData.CreateRevenueInfo(TaxType.Vat6, RevenueType.Other)).Success.Get()
                    )),
                    payments: NonEmptyEnumerable.Create(
                        NegativePayment.Create(NegativeAmount.Create(-133.06m).Success.Get(), PaymentType.Cash).Success.Get(),
                        NegativePayment.Create(NegativeAmount.Create(-66.53m).Success.Get(), PaymentType.DomesticPaymentsAccountNumber).Success.Get()
                    ),
                    counterPart: AadeTestInvoicesData.CreateInvoiceParty(country, "090701900", address: new Address(postalCode: NonEmptyString.CreateUnsafe("12"), city: NonEmptyString.CreateUnsafe("City")))
                ).Success.Get())
            ));

            var negativeInvoiceResponse = await client.SendInvoicesAsync(negativeInvoice);

            // Assert
            Assert.True(negativeInvoiceResponse.SendInvoiceResults.IsSuccess);
            Assert.All(negativeInvoiceResponse.SendInvoiceResults.Success.Get().Values, result => Assert.True(result.Value.IsSuccess));
        }
    }
}
