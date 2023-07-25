using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class SimpleAddress
{
    public SimpleAddress(City city, Country country, AdditionalAddressDetail additionalAddressDetail, PostalCode postalCode, Region region = null)
    {
        City = Check.IsNotNull(city, nameof(city));
        Country = Check.IsNotNull(country, nameof(country));
        AddtionalAddressDetail = Check.IsNotNull(additionalAddressDetail, nameof(additionalAddressDetail));
        PostalCode = Check.IsNotNull(postalCode, nameof(postalCode));
        Region = region.ToOption();
    }

    public City City { get; }

    public Country Country { get; }

    public AdditionalAddressDetail AddtionalAddressDetail { get; }

    public PostalCode PostalCode { get; }

    public IOption<Region> Region { get; }
}