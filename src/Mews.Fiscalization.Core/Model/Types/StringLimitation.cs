using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public class StringLimitation : ILimitation<string>
    {
        public StringLimitation(int? minLength = null, int? maxLength = null, string pattern = null, bool allowEmptyOrWhiteSpace = true, IEnumerable<string> allowedValues = null)
        {
            LengthLimitation = new RangeLimitation<int>(min: minLength, max: maxLength);
            Pattern = pattern != null ? new Regex(pattern) : null;
            AllowEmptyOrWhiteSpace = allowEmptyOrWhiteSpace;
            AllowedValues = allowedValues;
        }

        private RangeLimitation<int> LengthLimitation { get; }

        private Regex Pattern { get; }

        private bool AllowEmptyOrWhiteSpace { get; }

        private IEnumerable<string> AllowedValues { get; }

        public bool IsValid(string value)
        {
            var isWhiteSpaceAllowed = AllowEmptyOrWhiteSpace || !String.IsNullOrWhiteSpace(value);
            var isValueAllowed = AllowedValues?.Contains(value) ?? true;
            return value != null && isValueAllowed && isWhiteSpaceAllowed && LengthLimitation.IsValid(value.Length) && (Pattern?.IsMatch(value) ?? true);
        }

        public void CheckValidity(string value)
        {
            Check.IsNotNull(value, nameof(value));

            Check.Condition(AllowedValues?.Contains(value) ?? true, "Value is not inside the collection of allowed values.");

            var isEmptyAndNotAllowed = !AllowEmptyOrWhiteSpace && String.IsNullOrWhiteSpace(value);
            Check.Condition(!isEmptyAndNotAllowed, "Empty value is not allowed.");

            LengthLimitation.CheckValidity(value.Length, label: "length of string");

            if (Pattern.IsNotNull())
            {
                Check.Regex(Pattern, value, $"The value '{value}' does not match the pattern '{Pattern}'");
            }
        }
    }
}