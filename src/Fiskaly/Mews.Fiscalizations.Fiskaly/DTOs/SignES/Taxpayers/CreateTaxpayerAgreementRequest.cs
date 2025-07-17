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
    public Address Address { get; init; }
}