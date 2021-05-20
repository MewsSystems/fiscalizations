using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class InvoicePartyInfo
    {
        private InvoicePartyInfo(NonNegativeInt? branch = null, TaxpayerIdentificationNumber taxIdentifier = null, string name = null, Address address = null)
        {
            Branch = branch ?? NonNegativeInt.Zero();
            TaxIdentifier = taxIdentifier.ToOption();
            Name = name.ToOption();
            Address = address.ToOption();
        }

        public NonNegativeInt Branch { get; }

        public IOption<TaxpayerIdentificationNumber> TaxIdentifier { get; }

        public IOption<string> Name { get; }

        public IOption<Address> Address { get; }

        public static ITry<InvoicePartyInfo, INonEmptyEnumerable<Error>> Create(NonNegativeInt? branch = null, TaxpayerIdentificationNumber taxpayerNumber = null, string name = null, Address address = null)
        {
            return ObjectValidations.NotNull(branch).Map(b => new InvoicePartyInfo(b ?? NonNegativeInt.Zero(), taxpayerNumber, name, address));
        }
    }
}
