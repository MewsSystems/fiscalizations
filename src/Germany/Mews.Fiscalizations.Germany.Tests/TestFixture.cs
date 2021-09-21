using Mews.Fiscalizations.Germany.Model;
using System;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests
{
    public static class TestFixture
    {
        public static readonly ApiKey ApiKey = ApiKey.Create(Environment.GetEnvironmentVariable("german_api_key") ?? "INSERT_API_KEY").Success.Get();
        public static readonly ApiSecret ApiSecret = ApiSecret.Create(Environment.GetEnvironmentVariable("german_api_secret") ?? "INSERT_API_SECRET").Success.Get();

        public static FiskalyClient GetFiskalyClient()
        {
            return new FiskalyClient(ApiKey, ApiSecret);
        }

        public static async Task<FiskalyTestData> CreateTestData()
        {
            var fiskalyClient = GetFiskalyClient();
            var accessToken = (await fiskalyClient.GetAccessTokenAsync()).SuccessResult;
            var createdTss = (await fiskalyClient.CreateTssAsync(accessToken)).SuccessResult;

            var createdTssId = createdTss.Id;
            await fiskalyClient.UpdateTssAsync(accessToken, createdTssId, TssState.Uninitialized);

            var adminPin = "1234567890";
            await fiskalyClient.ChangeAdminPinAsync(accessToken, createdTssId, createdTss.AdminPuk, adminPin);
            await fiskalyClient.AdminLoginAsync(accessToken, createdTssId, adminPin);
            var initializedTss = (await fiskalyClient.UpdateTssAsync(accessToken, createdTssId, TssState.Initialized)).SuccessResult;

            var client = (await fiskalyClient.CreateClientAsync(accessToken, createdTssId)).SuccessResult;

            return await  Task.FromResult(new FiskalyTestData(
                adminPin: adminPin,
                client: client,
                tss: initializedTss,
                accessToken: accessToken
            ));
        }

        public static async Task CleanTestData(FiskalyTestData data)
        {
            var fiskalyClient = GetFiskalyClient();
            await fiskalyClient.UpdateTssAsync(data.AccessToken, data.Tss.Id, TssState.Disabled);
            await fiskalyClient.UpdateClientAsync(data.AccessToken, data.Tss.Id, data.Client.Id, ClientState.Deregistered);
            await fiskalyClient.AdminLogoutAsync(data.Tss.Id);
        }
    }
}
