namespace Mews.Fiscalizations.Germany.V1.Model
{
    public sealed class Signature
    {
        public Signature(string value, int counter, string algorithm, byte[] publicKey)
        {
            Value = value;
            Counter = counter;
            Algorithm = algorithm;
            PublicKey = publicKey;
        }

        public string Value { get; }

        public int Counter { get; }

        public string Algorithm { get; }

        public byte[] PublicKey { get; }
    }
}
