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

        public static readonly Guid ClientId = new Guid(Environment.GetEnvironmentVariable("german_client_Id") ?? "1f813cc3-d78d-400c-81a8-fef412970a97");
        public static readonly Guid TssId = new Guid(Environment.GetEnvironmentVariable("german_tss_id") ?? "17572356-909a-4938-8c1e-7d192f7e2652");
        public static readonly ApiKey ApiKey = ApiKey.Create(Environment.GetEnvironmentVariable("german_api_key") ?? "test_ezskuqy6q9wp88q3qh4d0c2ze_library-test").Success.Get();
        public static readonly ApiSecret ApiSecret = ApiSecret.Create(Environment.GetEnvironmentVariable("german_api_secret") ?? "alDXQVEdbt9Luy8LxYvV5Wydd5uEHwCSNheJNQA0shG").Success.Get();
        public static readonly string AdminPin = Environment.GetEnvironmentVariable("german_admin_pin") ?? "9582571821";
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