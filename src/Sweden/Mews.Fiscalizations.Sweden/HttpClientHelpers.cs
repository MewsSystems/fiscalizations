using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Sweden;

internal static class HttpClientHelpers
{
    public static HttpClient CreateHttpClient(
        X509Certificate2 clientCertificate,
        List<X509Certificate2> signingCertificates,
        string userAgent)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (_, cert, _, errors) => ValidateServerCertificate(cert!, signingCertificates, errors);

        var certCollection = new X509Certificate2Collection();
        certCollection.Add(clientCertificate);
        certCollection.AddRange(signingCertificates.ToArray());
        handler.ClientCertificates.AddRange(certCollection);

        var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

        return httpClient;
    }
    private static bool ValidateServerCertificate(X509Certificate certificate, IEnumerable<X509Certificate> signingCertificates, SslPolicyErrors sslPolicyErrors)
    {
        Console.WriteLine($"Validating server certificate, errors: {sslPolicyErrors}");
        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }
        if (sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors))
        {
            foreach (var cert in signingCertificates)
            {
                var chain = new X509Chain
                {
                    ChainPolicy = new X509ChainPolicy
                    {
                        RevocationMode = X509RevocationMode.NoCheck,
                        ExtraStore = { cert },
                        VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
                    }
                };
                return chain.Build((X509Certificate2)certificate);
            }
        }
        return false;
    }
}