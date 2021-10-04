using FuncSharp;
using Mews.Fiscalizations.Germany.V2;
using Mews.Fiscalizations.Germany.V2.Model;
using System;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests.V2
{
    public static class TestFixture
    {
        public static readonly ApiKey ApiKey = ApiKey.Create(Environment.GetEnvironmentVariable("german_api_key") ?? "INSERT_API_KEY").Success.Get();
        public static readonly ApiSecret ApiSecret = ApiSecret.Create(Environment.GetEnvironmentVariable("german_api_secret") ?? "INSERT_API_SECRET").Success.Get();
        public static readonly string AdminPin = Environment.GetEnvironmentVariable("german_admin_pin") ?? "INSERT_ADMIN_PIN";

        // Fiskaly deletes the test environment data each week, so we should recreate the deleted data for tests.
        internal static async Task<FiskalyTestData> GetFiskalyTestData()
        {
            var fiskalyClient = new FiskalyClient(ApiKey, ApiSecret);
            var accessToken = (await fiskalyClient.GetAccessTokenAsync()).SuccessResult;
            var allTsses = (await fiskalyClient.GetAllTSSsAsync(accessToken)).SuccessResult;

            var firstInitalizedTss = allTsses.TssList.FirstOption(t => t.State == TssState.Initialized);
            return await firstInitalizedTss.Match(
                async t =>
                {
                    var allClients = (await fiskalyClient.GetAllTssClientsAsync(accessToken, t.Id)).SuccessResult;
                    var firstRegisteredClient = allClients.Clients.FirstOption(c => c.State == ClientState.Registered);
                    var client = await firstRegisteredClient.Match(
                        c => Task.FromResult(c),
                        async _ =>
                        {
                            await fiskalyClient.AdminLoginAsync(accessToken, t.Id, AdminPin);
                            return (await fiskalyClient.CreateClientAsync(accessToken, t.Id)).SuccessResult;
                        }
                    );
                    return new FiskalyTestData(fiskalyClient, t, client);
                },
                async _ =>
                {
                    var tss = (await fiskalyClient.CreateTssAsync(accessToken)).SuccessResult;
                    await fiskalyClient.UpdateTssAsync(accessToken, tss.Id, TssState.Uninitialized);
                    await fiskalyClient.ChangeAdminPinAsync(accessToken, tss.Id, tss.AdminPuk, AdminPin);
                    await fiskalyClient.AdminLoginAsync(accessToken, tss.Id, AdminPin);
                    await fiskalyClient.UpdateTssAsync(accessToken, tss.Id, TssState.Initialized);
                    
                    var client = (await fiskalyClient.CreateClientAsync(accessToken, tss.Id)).SuccessResult;
                    await fiskalyClient.AdminLogoutAsync(tss.Id);

                    return new FiskalyTestData(fiskalyClient, tss, client);
                }
            );
        }
    }
}