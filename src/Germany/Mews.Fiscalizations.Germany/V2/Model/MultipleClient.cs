using System.Collections.Generic;

namespace Mews.Fiscalizations.Germany.V2.Model
{
    public sealed class MultipleClient
    {
        public MultipleClient(IEnumerable<Client> clients)
        {
            Clients = clients;
        }

        public IEnumerable<Client> Clients { get; }
    }
}