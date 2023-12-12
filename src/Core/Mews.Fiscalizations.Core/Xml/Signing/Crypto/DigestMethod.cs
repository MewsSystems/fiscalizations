using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Crypto;

public sealed class DigestMethod
{
    public static readonly DigestMethod SHA1 = new("SHA1", "http://www.w3.org/2000/09/xmldsig#sha1", "1.3.14.3.2.26");
    public static readonly DigestMethod SHA256 = new("SHA256", "http://www.w3.org/2001/04/xmlenc#sha256", "2.16.840.1.101.3.4.2.1");
    public static readonly DigestMethod SHA512 = new("SHA512", "http://www.w3.org/2001/04/xmlenc#sha512", "2.16.840.1.101.3.4.2.3");

    public string Name { get; }

    public string URI { get; }

    public string Oid { get; }

    private DigestMethod(string name, string uri, string oid)
    {
        Name = name;
        URI = uri;
        Oid = oid;
    }

    public HashAlgorithm GetHashAlgorithm()
    {
        return Name switch
        {
            "SHA1" => System.Security.Cryptography.SHA1.Create(),
            "SHA256" => System.Security.Cryptography.SHA256.Create(),
            "SHA512" => System.Security.Cryptography.SHA512.Create(),
            _ => throw new NotSupportedException("Unsupported algorithm.")
        };
    }
}