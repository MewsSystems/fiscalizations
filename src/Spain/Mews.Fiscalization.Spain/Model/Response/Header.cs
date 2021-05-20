namespace Mews.Fiscalization.Spain.Model.Response
{
    public sealed class Header
    {
        public Header(LocalCompany company, CommunicationType communicationType)
        {
            Company = company;
            CommunicationType = communicationType;
        }

        public LocalCompany Company { get; }

        public CommunicationType CommunicationType { get; }
    }
}
