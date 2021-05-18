using System;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class ChainValue : ByteValue
    {
        public ChainValue(byte[] value)
            : base(value)
        {
            if (Value.Length != 8)
            {
                throw new ArgumentException("Unexpected number of bytes.");
            }
        }

        public ChainValue(string base64Value)
            : base(base64Value)
        {
        }
    }
}
