using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class CreateTaxpayerAgreementRequest
{
    [JsonPropertyName("representative")]
    public TaxpayerAgreementRepresentativeRequest Representative { get; init; }
}

internal sealed class TaxpayerAgreementRepresentativeRequest
{
    [JsonPropertyName("full_name")]
    public string FullName { get; init; }

    [JsonPropertyName("tax_number")]
    public string TaxNumber { get; init; }

    [JsonPropertyName("address")]
    public TaxpayerAgreementRepresentativeAddress Address { get; init; }
}

internal sealed class TaxpayerAgreementRepresentativeAddress
{
    [JsonPropertyName("municipality")]
    public string Municipality { get; init; }
    
    [JsonPropertyName("city")]
    public string City { get; init; }
    
    [JsonPropertyName("street")]
    public string Street { get; init; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; init; }
    
    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("country")]
    public string Country { get; init; }
}