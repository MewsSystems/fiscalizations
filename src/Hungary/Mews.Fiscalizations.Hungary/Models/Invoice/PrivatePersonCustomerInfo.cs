namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class PrivatePersonCustomerInfo
    {
        public PrivatePersonCustomerInfo()
        {
        }

        public CustomerVatStatusType CustomerVatStatusType
        {
            get { return CustomerVatStatusType.PrivatePerson; }
        }
    }
}
