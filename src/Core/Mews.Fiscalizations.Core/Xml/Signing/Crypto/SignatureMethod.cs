namespace Mews.Fiscalizations.Core.Xml.Signing.Crypto;

public sealed class SignatureMethod
{
    public static readonly SignatureMethod RSAwithSHA1 = new("RSAwithSHA1", "http://www.w3.org/2000/09/xmldsig#rsa-sha1");
    public static readonly SignatureMethod RSAwithSHA256 = new("RSAwithSHA256", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
    public static readonly SignatureMethod RSAwithSHA512 = new("RSAwithSHA512", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512");

    public string Name { get; }

    public string URI { get; }

    private SignatureMethod(string name, string uri)
    {
        Name = name;
        URI = uri;
    }
}