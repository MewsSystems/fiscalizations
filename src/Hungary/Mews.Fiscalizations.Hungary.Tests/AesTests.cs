using System.Security.Cryptography;
using System.Text;
using NavAes = Mews.Fiscalizations.Hungary.Utils.Aes;

namespace Mews.Fiscalizations.Hungary.Tests;

/// <summary>
/// Pins how the exchange token is decrypted. NAV encodes the token with AES-128 under the technical user's
/// replacement key; the transform is AES/ECB/PKCS7Padding, which these tests hold in place so a
/// BouncyCastle upgrade cannot change it silently - that would surface as INVALID_EXCHANGE_TOKEN against
/// every submission.
/// </summary>
public sealed class AesTests
{
    private const string Key = "0123456789abcdef";

    [Test]
    public void Decrypt_EcbPkcs7EncryptedToken_ReturnsThePlaintext()
    {
        var token = "MEWSTOKEN1234567";

        var decrypted = NavAes.Decrypt(Key, EncryptEcb(token));

        Assert.That(Encoding.UTF8.GetString(decrypted), Is.EqualTo(token));
    }

    [Test]
    public void Decrypt_MultiBlockToken_ReturnsThePlaintext()
    {
        // Two identical blocks encrypt to identical ciphertext under ECB, so this also fails if the default
        // mode ever became a chaining one.
        var token = "MEWSTOKEN1234567MEWSTOKEN1234567";

        var decrypted = NavAes.Decrypt(Key, EncryptEcb(token));

        Assert.That(Encoding.UTF8.GetString(decrypted), Is.EqualTo(token));
    }

    [Test]
    public void Decrypt_TokenShorterThanABlock_ReturnsThePlaintext()
    {
        var token = "SHORT";

        var decrypted = NavAes.Decrypt(Key, EncryptEcb(token));

        Assert.That(Encoding.UTF8.GetString(decrypted), Is.EqualTo(token));
    }

    private static byte[] EncryptEcb(string plainText)
    {
        using var aes = System.Security.Cryptography.Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        return aes.EncryptEcb(Encoding.UTF8.GetBytes(plainText), PaddingMode.PKCS7);
    }
}
