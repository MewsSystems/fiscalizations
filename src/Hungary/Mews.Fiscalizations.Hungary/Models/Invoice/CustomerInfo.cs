using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class CustomerInfo
    {
        public CustomerInfo(CustomerVatStatusType type, TaxpayerIdentificationNumber taxpayerId = null, Name name = null, SimpleAddress address = null)
        {
            // Change to coproduct?
            if (type == CustomerVatStatusType.PrivatePerson)
            {
                Check.Condition(name.IsNull(), $"{nameof(Name)} must be empty when the customer is of type {type}");
                Check.Condition(address.IsNull(), $"{nameof(SimpleAddress)} must be empty when the custoemr is of type {type}");
                Check.Condition(taxpayerId.IsNull(), $"{nameof(TaxpayerIdentificationNumber)} must be empty when the customer is of type {type}");
            }
            if (type == CustomerVatStatusType.Domestic)
            {
                Check.IsNotNull(taxpayerId, nameof(taxpayerId)).ToOption();
                Check.IsNotNull(name, nameof(name)).ToOption();
                Check.IsNotNull(address, nameof(address)).ToOption();

                Check.Condition(taxpayerId.Country.Alpha2Code == "HU", "Domestic customers must have HU country.");
            }
            if (type == CustomerVatStatusType.Other)
            {
                Check.IsNotNull(name, nameof(name)).ToOption();
                Check.IsNotNull(address, nameof(address)).ToOption();
            }

            CustomerVatStatusType = type;
            TaxpayerId = taxpayerId.ToOption();
            Name = name.ToOption();
            Address = address.ToOption();
        }

        public CustomerVatStatusType CustomerVatStatusType { get; }

        public IOption<TaxpayerIdentificationNumber> TaxpayerId { get; }

        public IOption<Name> Name { get; }

        public IOption<SimpleAddress> Address { get; }
    }
}
