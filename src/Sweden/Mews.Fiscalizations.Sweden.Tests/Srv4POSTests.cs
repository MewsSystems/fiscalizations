using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Sweden.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Sweden.Tests;

[TestFixture]
public sealed class Srv4POSTests
{
    private static readonly string Username = Environment.GetEnvironmentVariable("swedish_srv4pos_username") ?? "INSERT_USERNAME";
    private static readonly string Password = Environment.GetEnvironmentVariable("swedish_srv4pos_password") ?? "INSERT_PASSWORD";
    private static readonly string ApplicationPackage = Environment.GetEnvironmentVariable("swedish_srv4pos_app_package") ?? "INSERT_APP_PACKAGE";
    private static readonly string ApiKey = Environment.GetEnvironmentVariable("swedish_srv4pos_api_key") ?? "INSERT_API_KEY";
    private static readonly string CorporateId = Environment.GetEnvironmentVariable("swedish_srv4pos_corporate_id") ?? "INSERT_CORPORATE_ID";
    private const string CashRegisterName = "CashReg001";

    [Test]
    [Retry(3)]
    public async Task CheckCashRegisterUniqueness_Succeeds_Async()
    {
        var client = new Srv4posClient();
        var corporateId = GenerateLuhnNumber();
        var response = await client.CheckCashRegisterUniquenessAsync(Countries.Sweden, corporateId, CashRegisterName);

        Assert.That(response.IsSuccess, Is.True);
        Assert.That(response.Success.Get(), Is.False);
    }

    [Test]
    [Ignore("Only for local testing - don't run in pipelines.")]
    public async Task CreateActivation_Succeeds_Async()
    {
        var request = new CreateActivationRequest(
            country: Countries.Sweden,
            corporateId: GenerateLuhnNumber(),
            cashRegisterName: CashRegisterName,
            features: ["CONTROL_UNIT"],
            controlUnitSerial: "PTEST900000000001",
            applicationNameAndVersion: "SE Test app 1.0",
            applicationPackage: ApplicationPackage,
            address: new Address(AddressLines: "Address line 1", City: "Stockholm", PostalCode: "12311", CompanyName: "Test company"),
            installationCreationInfo: new InstallationCreationInfo("SERVER", "SE Test app")
        );

        var client = new Srv4posClient();
        var activationResponse = await client.CreateActivation(Username, Password, request);

        Assert.That(activationResponse.IsSuccess, Is.True);
        Assert.That(activationResponse.Success.Get().ActivationId, Is.Not.Null);
        Assert.That(activationResponse.Success.Get().ApiKey, Is.Not.Empty);
    }

    [Test]
    [Retry(3)]
    public async Task SendData_Succeeds_Async()
    {
        var sendDataRequest = new SendDataRequest(
            grossAmount: 100,
            totalTaxByVatRate: new Dictionary<decimal, decimal> { { 0, 0 }, { 0.06m, 6 }, { 0.12m, 0 }, { 0.25m, 0 } },
            isRefund: false,
            printType: PrintType.Normal,
            saleDate: DateTime.Now,
            receiptNumber: 1
        );
        var client = new Srv4posClient();
        var sendDataResponse = await client.SendDataToControlUnitAsync(ApiKey, CorporateId, CashRegisterName, sendDataRequest);

        Assert.That(sendDataResponse.IsSuccess, Is.True);
        Assert.That(sendDataResponse.Success.Get().ResponseCode, Is.Not.Empty);
    }

    private static string GenerateLuhnNumber()
    {
        var random = new Random();
        var number = random.Next(1_000_000_000, 2_000_000_000).ToString(); // Generate a 9-digit random number

        var sum = 0;
        for (var i = 0; i < number.Length - 1; i++)
        {
            var digit = int.Parse(number[i].ToString());
            if ((number.Length - i - 1) % 2 == 1)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }
            sum += digit;
        }

        var checkDigit = (10 - (sum % 10)) % 10; // Calculate the check digit
        return $"{number[..^1]}{checkDigit}"; // Return the 10-digit Luhn number
    }
}