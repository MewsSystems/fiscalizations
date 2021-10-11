using System;

namespace Mews.Fiscalizations.Germany.V2.Model
{
    public sealed class AccessToken
    {
        public AccessToken(string value, FiskalyEnvironment environment, DateTime expirationUtc)
        {
            Value = value;
            Environment = environment;
            ExpirationUtc = expirationUtc;
        }

        public string Value { get; set; }

        public FiskalyEnvironment Environment { get; }

        public DateTime ExpirationUtc { get; set; }
    }
}
