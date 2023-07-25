using Mews.Fiscalizations.Austria.Dto;
using Mews.Fiscalizations.Austria.Dto.Identifiers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Austria.ATrust;

public sealed class ATrustSigner : ISigner
{
    public ATrustSigner(ATrustCredentials credentials, ATrustEnvironment environment = ATrustEnvironment.Production)
    {
        var environmentDomain = environment == ATrustEnvironment.Production ? "www" : "hs-abnahme";
        Credentials = credentials;
        EndpointUrl = new EndpointUrl($"https://{environmentDomain}.a-trust.at/asignrkonline/v2/{Credentials.User.Value}");
    }

    public EndpointUrl EndpointUrl { get; }

    public ATrustCredentials Credentials { get; }

    private static HttpClient HttpClient { get; }

    static ATrustSigner()
    {
        HttpClient = new HttpClient();
    }

    public async Task<SignerOutput> SignAsync(QrData qrData)
    {
        var input = new ATrustSignerInput(Credentials.Password, qrData);
        var requestContent = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"{EndpointUrl.Value}/Sign/JWS", requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ATrustSignerResponse>(responseContent);
        return new SignerOutput(new JwsRepresentation(result.JwsRepresentation), input.QrData);
    }

    public async Task<CertificateInfo> GetCertificateInfoAsync()
    {
        var response = await HttpClient.GetAsync($"{EndpointUrl.Value}/Certificate");
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CertificateInfo>(responseContent);
    }
}