using FuncSharp;
using Mews.Fiscalizations.Core.Model;

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

        public CustomerVatStatusType CustomerVatStatusType
        {
            get
            {
                return Match(
                    domestic => domestic.CustomerVatStatusType,
                    privatePerson => privatePerson.CustomerVatStatusType,
                    other => other.CustomerVatStatusType
                );
            }
        }

        public IOption<Name> Name
        {
            get
            {
                return Match(
                    domestic => domestic.Name.ToOption(),
                    privatePerson => Option.Empty<Name>(),
                    other => other.Name.ToOption()
                );
            }
        }

        public IOption<SimpleAddress> Address
        {
            get
            {
                return Match(
                    domestic => domestic.Address.ToOption(),
                    privatePerson => Option.Empty<SimpleAddress>(),
                    other => other.Address.ToOption()
                );
            }
        }

        public IOption<TaxpayerIdentificationNumber> TaxpayerId
        {
            get
            {
                return Match(
                    domestic => domestic.TaxpayerId.ToOption(),
                    privatePerson => Option.Empty<TaxpayerIdentificationNumber>(),
                    other => other.TaxpayerId
                );
            }
        }
    }
}
