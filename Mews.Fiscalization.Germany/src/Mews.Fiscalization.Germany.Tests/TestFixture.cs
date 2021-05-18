using Mews.Fiscalization.Germany.Model;
using System;

namespace Mews.Fiscalization.Germany.Tests
{
    public static class TestFixture
    {
        public static readonly Guid ClientId = new Guid(Environment.GetEnvironmentVariable("client_Id") ?? "INSERT_CLIENT_ID");
        public static readonly Guid TssId = new Guid(Environment.GetEnvironmentVariable("tss_id") ?? "INSERT_TSS_ID");
        public static readonly ApiKey ApiKey = ApiKey.Create(Environment.GetEnvironmentVariable("api_key") ?? "INSERT_API_KEY").Success.Get();
        public static readonly ApiSecret ApiSecret = ApiSecret.Create(Environment.GetEnvironmentVariable("api_secret") ?? "INSERT_API_SECRET").Success.Get();

        public static FiskalyClient GetFiskalyClient()
        {
            return new FiskalyClient(ApiKey, ApiSecret);
        }
    }
}
