using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Mews.Fiscalization.Hungary.Utils
{
    internal static class Aes
    {
        public static byte[] Decrypt(string key, byte[] data)
        {
            var cipher = CipherUtilities.GetCipher("AES");
            cipher.Init(false, new KeyParameter(Encoding.UTF8.GetBytes(key)));
            return cipher.DoFinal(data);
        }
    }
}