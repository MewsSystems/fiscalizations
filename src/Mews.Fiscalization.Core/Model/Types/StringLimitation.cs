using System;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public class StringLimitation
    {
        public StringLimitation(int? minLength = null, int? maxLength = null, string pattern = null, bool allowEmptyOrWhiteSpace = true)
        {
            LengthLimitation = new RangeLimitation<int>(min: minLength, max: maxLength);
            Pattern = pattern != null ? new Regex(pattern) : null;
            AllowEmptyOrWhiteSpace = allowEmptyOrWhiteSpace;
        }

        private RangeLimitation<int> LengthLimitation { get; }

        private Regex Pattern { get; }

        private bool AllowEmptyOrWhiteSpace { get; }

        internal bool IsValid(string value)
        {
            var isContentAllowed = AllowEmptyOrWhiteSpace || !String.IsNullOrWhiteSpace(value);
            return value != null && isContentAllowed && LengthLimitation.IsValid(value.Length) && (Pattern?.IsMatch(value) ?? true);
        }

        internal void CheckValidity(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!AllowEmptyOrWhiteSpace && String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Empty value is not allowed.");
            }

            LengthLimitation.CheckValidity(value.Length, label: "length of string");
            if (!Pattern?.IsMatch(value) ?? false)
            {
                throw new ArgumentException($"The value '{value}' does not match the pattern '{Pattern}'");
            }
        }
    }
}