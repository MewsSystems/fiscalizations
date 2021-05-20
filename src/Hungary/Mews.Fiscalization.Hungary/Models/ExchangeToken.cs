using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class ExchangeToken
    {
        public ExchangeToken(byte[] value, DateTime validFrom, DateTime validTo)
        {
            Value = value;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public byte[] Value { get; }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }

        internal static ExchangeToken Map(Dto.TokenExchangeResponse response)
        {
            return new ExchangeToken(
                value: response.encodedExchangeToken,
                validFrom: response.tokenValidityFrom,
                validTo: response.tokenValidityTo
            );
        }
    }
}
