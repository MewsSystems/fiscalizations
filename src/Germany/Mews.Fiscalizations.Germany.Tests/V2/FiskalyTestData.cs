using Mews.Fiscalizations.Germany.V2;
using Mews.Fiscalizations.Germany.V2.Model;

namespace Mews.Fiscalizations.Germany.Tests.V2
{
    internal class FiskalyTestData
    {
        public FiskalyTestData(FiskalyClient fiskalyClient, Tss tss, Client client)
        {
            FiskalyClient = fiskalyClient;
            Tss = tss;
            Client = client;
        }

        public FiskalyClient FiskalyClient { get; }

        public Tss Tss { get; }

        public Client Client { get; }
    }
}