using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mews.Fiscalization.Hungary.Utils
{
    internal static class Sha512
    {
        internal static string GetHash(string input)
        {
            using (var sha = SHA512.Create())
            {
                var encoding = Encoding.UTF8;
                var bytes = sha.ComputeHash(encoding.GetBytes(input));
                return String.Join("", bytes.Select(b => b.ToString("x2"))).ToUpper();
            }
        }

        internal static string GetSha3Hash(string input)
        {
            var hashAlgorithm = new Org.BouncyCastle.Crypto.Digests.Sha3Digest(512);
            var bytes = Encoding.ASCII.GetBytes(input);

            hashAlgorithm.BlockUpdate(bytes, 0, bytes.Length);

            var result = new byte[64]; // 512 / 8 = 64
            hashAlgorithm.DoFinal(result, 0);

            var hashString = BitConverter.ToString(result);
            return hashString.Replace("-", "").ToUpperInvariant();
        }
    }
}