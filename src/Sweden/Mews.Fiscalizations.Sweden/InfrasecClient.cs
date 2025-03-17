using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Sweden.Models;
using FuncSharp;

namespace Mews.Fiscalizations.Sweden;

public sealed class InfrasecClient : IInfrasecClient
{
    private readonly HttpClient _httpClient;
    private readonly string _infrasecApiUrl;
    private const string TestApiUrl = "https://tcs-verify.infrasec-api.se";
    private const string ProductionApiUrl = "https://tcs.infrasec-api.se"; // TODO: check if this is correct
    private const string TcsServerEndpoint = "/tcsserver";

    public InfrasecClient(InfrasecConfiguration configuration)
    {
        _infrasecApiUrl = configuration.Environment switch
        {
            Environment.Test => TestApiUrl,
            Environment.Production => ProductionApiUrl,
            _ => throw new ArgumentOutOfRangeException(configuration.Environment.ToString())
        };
        var handler = new HttpClientHandler
        {
            ClientCertificates = { configuration.Certificate, configuration.SigningCertificate },
            ServerCertificateCustomValidationCallback = (_, cert, _, errors) => ValidateServerCertificate(cert!, configuration.SigningCertificate, errors)
        };

        _httpClient = new HttpClient(handler);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(configuration.UserAgent);
    }

    public async Task<Try<TransactionResponse, Exception>> SendTransactionAsync(TransactionData data, NonEmptyString applicationId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToDto(applicationId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var endpoint = new Uri($"{_infrasecApiUrl}{TcsServerEndpoint}");
        var response = await _httpClient.PostAsync(endpoint, new StringContent(xml.OuterXml, Encoding.UTF8, "application/xml"), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<TransactionResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}")
            );
        }
        var tcsResponse = await Try.CatchAsync<DTOs.TcsResponse, Exception>(
            async _ => XmlSerializer.Deserialize<DTOs.TcsResponse>(await response.Content.ReadAsStringAsync(cancellationToken))
        );
        return tcsResponse.Map(r => r.FromDto());
    }

    // TODO: Add RegisterStatus and Enrollment APIs.

    private static bool ValidateServerCertificate(X509Certificate certificate, X509Certificate2 signingCertificate, SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }
        if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
        {
            var chain = new X509Chain
            {
                ChainPolicy = new X509ChainPolicy
                {
                    RevocationMode = X509RevocationMode.NoCheck,
                    ExtraStore = { signingCertificate },
                    VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
                }
            };
            return chain.Build((X509Certificate2)certificate);
        }
        return false;
    }
}