using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Mews.Fiscalizations.Hungary.Utils;

internal static class Aes
{
    /// <summary>
    /// NAV encodes the exchange token with AES-128 under the technical user's replacement key. The transform
    /// is named in full rather than left to BouncyCastle's default for "AES" (which resolves to this same
    /// ECB/PKCS7 combination today), so an upgrade cannot change the mode or padding under us - the failure
    /// mode would be every submission rejected with INVALID_EXCHANGE_TOKEN.
    /// </summary>
    private const string Transformation = "AES/ECB/PKCS7Padding";

    public static byte[] Decrypt(string key, byte[] data)
    {
        var cipher = CipherUtilities.GetCipher(Transformation);
        cipher.Init(false, new KeyParameter(ServiceInfo.Encoding.GetBytes(key)));
        return cipher.DoFinal(data);
    }
}
