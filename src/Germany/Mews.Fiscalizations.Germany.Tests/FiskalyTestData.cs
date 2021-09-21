using Mews.Fiscalizations.Germany.Model;

namespace Mews.Fiscalizations.Germany.Tests
{
    public sealed class FiskalyTestData
    {
        public FiskalyTestData(string adminPin, Client client, Tss tss, AccessToken accessToken)
        {
            AdminPin = adminPin;
            Client = client;
            Tss = tss;
            AccessToken = accessToken;
        }

        public string AdminPin { get; }

        public Client Client { get; }

        public Tss Tss { get; }

        public AccessToken AccessToken { get; }
    }
}
