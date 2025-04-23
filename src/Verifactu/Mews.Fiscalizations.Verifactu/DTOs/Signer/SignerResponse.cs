using System.Globalization;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class SignerResponse
{
    [JsonPropertyName("content")]
    public SignerData SignerData { get; init; }
}

internal sealed class SignerData
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SignerState State { get; init; }

    [JsonPropertyName("certificate")]
    public SignerCertificate Certificate { get; init; }
}

internal sealed class SignerCertificate
{
    [JsonPropertyName("serial_number")]
    public string SerialNumber { get; init; }

    [JsonPropertyName("expires_at")]
    public string ExpiresAtRaw { get; init; }

    [JsonIgnore]
    public DateTime ExpiresAt => DateTime.ParseExact(ExpiresAtRaw, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

    [JsonPropertyName("x509_pem")]
    public string X509Pem { get; init; }
}

internal enum SignerState
{
    ENABLED,
    DISABLED,
    DEFCTIVE
}

