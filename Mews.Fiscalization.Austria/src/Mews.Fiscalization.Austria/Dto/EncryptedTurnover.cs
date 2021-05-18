namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class EncryptedTurnover : ByteValue
    {
        public EncryptedTurnover(byte[] value)
            : base(value)
        {
        }

        public EncryptedTurnover(string base64Value)
            : base(base64Value)
        {
        }
    }
}
