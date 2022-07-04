using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Subject
    {
        private Subject(Issuer issuer, IEnumerable<Receiver> receivers, IssuerType? issuerType = null)
        {
            Issuer = issuer;
            Receivers = receivers;
            MultipleReceivers = Receivers.Count() > 1;
            IssuerType = issuerType.ToOption();
        }

        public Issuer Issuer { get; }

        public IEnumerable<Receiver> Receivers { get; }

        public bool MultipleReceivers { get; }

        public IOption<IssuerType> IssuerType { get; }

        public static ITry<Subject, IEnumerable<Error>> Create(Issuer issuer, IEnumerable<Receiver> receivers, IssuerType? issuerType = null)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(issuer),
                receivers.ToTry(i => i.Count() >= 1 && i.Count() <= 100, _ => new Error($"{nameof(receivers)} count must be in range [1, 100].")),
                (i, r) => new Subject(i, r, issuerType)
            );
        }
    }
}