using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class Header
    {
        public Header(LocalCompany company, CommunicationType communicationType)
        {
            Company = Check.IsNotNull(company, nameof(company));
            CommunicationType = communicationType;
        }

        public LocalCompany Company { get; }

        public CommunicationType CommunicationType { get; }
    }
}
