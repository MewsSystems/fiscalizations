using System;

namespace Mews.Fiscalization.Austria.Dto
{
    public class ByteValue
    {
        public ByteValue(byte[] value)
        {
            Value = value;
        }

        public ByteValue(string base64Value)
        {
            Value = Convert.FromBase64String(base64Value);
        }

        public byte[] Value { get; }

        public string ToBase64String()
        {
            return Convert.ToBase64String(Value);
        }
    }
}
