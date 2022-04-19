using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class Header
    {
        public Header(LocalCounterParty issuer, CommunicationType communicationType)
        {
            LocalCounterParty = Check.IsNotNull(issuer, nameof(issuer));
            CommunicationType = communicationType;
        }

        public LocalCounterParty LocalCounterParty { get; }

        public CommunicationType CommunicationType { get; }
    }
}
