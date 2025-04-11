using System.Security.Cryptography.X509Certificates;
using FuncSharp;

namespace Mews.Fiscalizations.Sweden;

public sealed record InfrasecClientConfiguration(
    Environment Environment,
    X509Certificate2 ClientCertificate,
    IEnumerable<X509Certificate2> SigningCertificates,
    NonEmptyString UserAgent
);