namespace Mews.Fiscalizations.Fiskaly.Tests;

public class TestFixture
{
    public static readonly string SignESApiKey = Environment.GetEnvironmentVariable("fiskaly_signes_api_key") ?? "INSERT_API_KEY";
    public static readonly string SignESApiSecret = Environment.GetEnvironmentVariable("fiskaly_signes_api_secret") ?? "INSERT_API_SECRET";
    public static readonly string ManagementApiKey = Environment.GetEnvironmentVariable("fiskaly_management_api_key") ?? "INSERT_API_KEY";
    public static readonly string ManagementApiSecret = Environment.GetEnvironmentVariable("fiskaly_management_api_secret") ?? "INSERT_API_SECRET";
}