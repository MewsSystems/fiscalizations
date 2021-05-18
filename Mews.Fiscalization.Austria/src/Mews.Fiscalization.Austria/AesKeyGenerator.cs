using Org.BouncyCastle.Security;

namespace Mews.Fiscalization.Austria
{
    public sealed class AesKeyGenerator
    {
        public static byte[] GetNext()
        {
            var random = new SecureRandom();
            var bytes = new byte[32];
            random.NextBytes(bytes);
            return bytes;
        }
    }
}
