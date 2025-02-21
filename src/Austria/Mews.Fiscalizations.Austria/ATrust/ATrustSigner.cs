using Mews.Fiscalizations.Austria.Dto;
using Mews.Fiscalizations.Austria.Dto.Identifiers;
using Newtonsoft.Json;
using System.Text;

namespace Mews.Fiscalizations.Austria.ATrust;

public sealed class ATrustSigner : ISigner
{
    private readonly HttpClient _httpClient;
    private readonly EndpointUrl _endpointUrl;
    private readonly ATrustCredentials _credentials;

    public ATrustSigner(HttpClient httpClient, ATrustCredentials credentials, ATrustEnvironment environment = ATrustEnvironment.Production)
    {
        var environmentDomain = environment == ATrustEnvironment.Production ? "www" : "hs-abnahme";
        _credentials = credentials;
        _endpointUrl = new EndpointUrl($"https://{environmentDomain}.a-trust.at/asignrkonline/v2/{_credentials.User.Value}");
        _httpClient = httpClient;
    }

    public async Task<SignerOutput> SignAsync(QrData qrData)
    {
        var input = new ATrustSignerInput(_credentials.Password, qrData);
        var requestContent = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_endpointUrl.Value}/Sign/JWS", requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var result = JsonConvert.DeserializeObject<ATrustSignerResponse>(responseContent);
            return new SignerOutput(new JwsRepresentation(result.JwsRepresentation), input.QrData);
        }
        catch (JsonException jsonEx)
        {
            throw new Exception($"Failed to deserialize response content: {responseContent}", jsonEx);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while processing the response: {responseContent}", ex);
        }
    }

    public async Task<CertificateInfo> GetCertificateInfoAsync()
    {
        var response = await _httpClient.GetAsync($"{_endpointUrl.Value}/Certificate");
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CertificateInfo>(responseContent);
    }
}