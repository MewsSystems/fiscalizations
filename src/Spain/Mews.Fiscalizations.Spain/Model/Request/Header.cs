using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class Header
    {
        public Header(Issuer issuer, CommunicationType communicationType)
        {
            Issuer = Check.IsNotNull(issuer, nameof(issuer));
            CommunicationType = communicationType;
        }

        public Issuer Issuer { get; }

        public CommunicationType CommunicationType { get; }
    }
}
