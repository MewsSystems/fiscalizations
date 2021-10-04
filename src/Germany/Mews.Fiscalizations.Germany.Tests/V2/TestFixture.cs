using Mews.Fiscalizations.Germany.V2;
using Mews.Fiscalizations.Germany.V2.Model;
using System;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests.V2
{
    public static class TestFixture
    {
        static TestFixture()
        {
            InitializeFiskalyData(GetFiskalyClient()).GetAwaiter().GetResult();
        }

        public static readonly Guid ClientId = new Guid(Environment.GetEnvironmentVariable("german_client_Id") ?? "INSERT_CLIENT_ID");
        public static readonly Guid TssId = new Guid(Environment.GetEnvironmentVariable("german_tss_id") ?? "INSERT_TSS_ID");
        public static readonly ApiKey ApiKey = ApiKey.Create(Environment.GetEnvironmentVariable("german_api_key") ?? "INSERT_API_KEY").Success.Get();
        public static readonly ApiSecret ApiSecret = ApiSecret.Create(Environment.GetEnvironmentVariable("german_api_secret") ?? "INSERT_API_SECRET").Success.Get();
        public static readonly string AdminPin = Environment.GetEnvironmentVariable("german_admin_pin") ?? "INSERT_ADMIN_PIN";
        public static readonly string AdminPuk = Environment.GetEnvironmentVariable("german_admin_puk") ?? "INSERT_ADMIN_PUK";

        public static FiskalyClient GetFiskalyClient()
        {
            return new FiskalyClient(ApiKey, ApiSecret);
        }

        // Fiskaly deletes the test environment data each week, so we should recreate the deleted data for tests.
        private static async Task InitializeFiskalyData(FiskalyClient fiskalyClient)
        {
            var accessToken = (await fiskalyClient.GetAccessTokenAsync()).SuccessResult;
            var tssExists = (await fiskalyClient.GetTssAsync(accessToken, TssId)).IsSuccess;
            if (!tssExists)
            {
                var tss = (await fiskalyClient.CreateTssAsync(accessToken, TssId)).SuccessResult;
                await fiskalyClient.UpdateTssAsync(accessToken, TssId, TssState.Uninitialized);
                await fiskalyClient.ChangeAdminPinAsync(accessToken, TssId, tss.AdminPuk, AdminPin);
                await fiskalyClient.AdminLoginAsync(accessToken, TssId, AdminPin);
                await fiskalyClient.UpdateTssAsync(accessToken, TssId, TssState.Initialized);
                await fiskalyClient.AdminLogoutAsync(TssId);
            }

            var clientExists = (await fiskalyClient.GetClientAsync(accessToken, ClientId, TssId)).IsSuccess;
            if (!clientExists)
            {
                await fiskalyClient.AdminLoginAsync(accessToken, TssId, AdminPin);
                await fiskalyClient.CreateClientAsync(accessToken, TssId);
                await fiskalyClient.AdminLogoutAsync(TssId);
            }
        }
    }
}