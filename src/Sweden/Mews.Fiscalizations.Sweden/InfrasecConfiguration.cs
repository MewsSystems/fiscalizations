using System.Security.Cryptography.X509Certificates;
using FuncSharp;

namespace Mews.Fiscalizations.Sweden;

public sealed record InfrasecConfiguration(
    Environment Environment,
    X509Certificate2 Certificate,
    X509Certificate2 SigningCertificate,
    NonEmptyString UserAgent
);