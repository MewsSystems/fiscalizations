using System;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Austria.Dto.Identifiers
{
    public class StringIdentifier : Identifier<string>
    {
        protected StringIdentifier(string value, Regex pattern)
            : base(value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            if (!pattern.IsMatch(value))
            {
                throw new ArgumentException($"The value '{value}' does not match the pattern '{pattern}'");
            }
        }

        protected static bool IsValid(string value, Regex pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            return value != null && pattern.IsMatch(value);
        }
    }
}
