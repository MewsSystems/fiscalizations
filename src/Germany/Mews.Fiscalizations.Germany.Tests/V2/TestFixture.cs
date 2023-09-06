using Mews.Fiscalizations.Germany.V2.Model;

namespace Mews.Fiscalizations.Germany.Tests.V2;

public static class TestFixture
{
    public static readonly ApiKey ApiKey = ApiKey.Create(Environment.GetEnvironmentVariable("german_api_key") ?? "INSERT_API_KEY").Success.Get();
    public static readonly ApiSecret ApiSecret = ApiSecret.Create(Environment.GetEnvironmentVariable("german_api_secret") ?? "INSERT_API_SECRET").Success.Get();
    public static readonly string AdminPin = Environment.GetEnvironmentVariable("german_admin_pin") ?? "INSERT_ADMIN_PIN";
    public static FiskalyTestData FiskalyTestData;
}