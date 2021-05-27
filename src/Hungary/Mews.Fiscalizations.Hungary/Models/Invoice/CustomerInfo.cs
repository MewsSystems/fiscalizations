using FuncSharp;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class CustomerInfo : Coproduct3<DomesticCustomerInfo, PrivatePersonCustomerInfo, OtherCustomerInfo>
    {
        public CustomerInfo(DomesticCustomerInfo domesticCustomerInfo)
            : base(domesticCustomerInfo)
        {
        }

        public CustomerInfo(PrivatePersonCustomerInfo privatePersonCustomerInfo)
            : base(privatePersonCustomerInfo)
        {
        }

        public CustomerInfo(OtherCustomerInfo otherCustomerInfo)
            : base(otherCustomerInfo)
        {
        }
    }
}
