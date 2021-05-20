using System;
using System.Collections;
using System.Text;

namespace Mews.Fiscalization.Hungary
{
    public static class RequestId
    {
        public static string CreateRandom()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var guid = Guid.NewGuid();
            var bits = new BitArray(guid.ToByteArray());
            var chars = new StringBuilder();
            var accumulator = 0;
            for (var i = 0; i <= bits.Length; i++)
            {
                if (i > 0 && (i % 5 == 0 || i == bits.Length))
                {
                    chars.Append(alphabet[accumulator]);
                    accumulator = 0;
                    if (i == bits.Length)
                    {
                        break;
                    }
                }
                accumulator += bits[i] ? (1 << i % 5) : 0;
            }

            return chars.ToString();
        }
    }
}