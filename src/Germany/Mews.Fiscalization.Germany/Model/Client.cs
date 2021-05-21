using System;

namespace Mews.Fiscalizations.Germany.Model
{
    public sealed class Client
    {
        public Client(string serialNumber, DateTime created, DateTime updated, Guid tssId, Guid id)
        {
            SerialNumber = serialNumber;
            Created = created;
            Updated = updated;
            TssId = tssId;
            Id = id;
        }

        public string SerialNumber { get; }

        public DateTime Created { get; }

        public DateTime Updated { get; }

        public Guid TssId { get; }

        public Guid Id { get; }
    }
}
