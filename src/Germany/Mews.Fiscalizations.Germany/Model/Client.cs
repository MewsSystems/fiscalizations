using System;

namespace Mews.Fiscalizations.Germany.Model
{
    public sealed class Client
    {
        public Client(ClientState state, string serialNumber, DateTime created, Guid tssId, Guid id)
        {
            State = state;
            SerialNumber = serialNumber;
            Created = created;
            TssId = tssId;
            Id = id;
        }

        public ClientState State { get; }

        public string SerialNumber { get; }

        public DateTime Created { get; }

        public Guid TssId { get; }

        public Guid Id { get; }
    }
}
