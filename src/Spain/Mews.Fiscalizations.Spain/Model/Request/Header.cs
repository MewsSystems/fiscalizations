using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class Header
    {
        public Header(LocalCounterParty company, CommunicationType communicationType)
        {
            Company = Check.IsNotNull(company, nameof(company));
            CommunicationType = communicationType;
        }

        public LocalCounterParty Company { get; }

        public CommunicationType CommunicationType { get; }
    }
}
