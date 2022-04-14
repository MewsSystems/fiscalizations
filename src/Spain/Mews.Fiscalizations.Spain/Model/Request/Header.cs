using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class Header
    {
        public Header(LocalCounterParty localCounterParty, CommunicationType communicationType)
        {
            LocalCounterParty = Check.IsNotNull(localCounterParty, nameof(localCounterParty));
            CommunicationType = communicationType;
        }

        public LocalCounterParty LocalCounterParty { get; }

        public CommunicationType CommunicationType { get; }
    }
}
