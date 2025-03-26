using System.Security.Cryptography.X509Certificates;
using FuncSharp;

namespace Mews.Fiscalizations.Sweden;

public sealed record InfrasecConfiguration(
    Environment Environment,
    X509Certificate2 TransactionCertificate,
    X509Certificate2 EnrollmentCertificate,
    IEnumerable<X509Certificate> TransactionSigningCertificates,
    IEnumerable<X509Certificate> EnrollmentSigningCertificates,
    NonEmptyString UserAgent
);