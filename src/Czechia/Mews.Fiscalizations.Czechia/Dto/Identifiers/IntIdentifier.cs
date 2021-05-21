using System;

namespace Mews.Eet.Dto.Identifiers
{
    public class IntIdentifier : Identifier<int>
    {
        protected IntIdentifier(int value, int lowerBound, int upperBound)
            : base(value)
        {
            if (value < lowerBound || value > upperBound)
            {
                throw new ArgumentException($"The value '{value}' is not within the expected range '[{lowerBound}, {upperBound}]'.");
            }
        }
    }
}
