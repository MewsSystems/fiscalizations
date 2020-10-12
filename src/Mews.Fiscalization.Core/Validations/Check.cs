using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mews.Fiscalization.Core.Extensions;

namespace Mews.Fiscalization.Core.Model
{
    public static class Check
    {
        public static void IsNotNull<T>(T value, string valueName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(valueName);
            }
        }

        public static void NonEmpty<T>(IEnumerable<T> collection, string message)
        {
            Condition(collection.NonEmpty(), message);
        }

        public static void Regex(Regex pattern, string value, string message)
        {
            Condition(pattern.IsMatch(value), message);
        }

        public static void Condition(bool value, string message)
        {
            if (!value)
            {
                throw GetArgumentError(message);
            }
        }

        private static Exception GetArgumentError(string message)
        {
            return new ArgumentException(message);
        }
    }
}