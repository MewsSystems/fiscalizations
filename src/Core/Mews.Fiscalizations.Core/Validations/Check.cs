using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.Core.Model;

public static class Check
{
    public static T IsNotNull<T>(T value, string valueName)
    {
        return value ?? throw new ArgumentNullException(valueName);
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

    public static void In<T>(T value, IEnumerable<T> values, string name)
    {
        if (!values.Contains(value))
        {
            throw new ArgumentOutOfRangeException($"Value ({value}) is not allowed.", name);
        }
    }

    public static void Digits(decimal value, int maxdigitCount)
    {
        if (!value.HasFewerDigitsThan(maxdigitCount))
        {
            throw new ArgumentOutOfRangeException($"Value cannot have more than {maxdigitCount} digits.");
        }
    }

    public static void Precision(decimal value, int maxPrecision)
    {
        if (!value.PrecisionSmallerThanOrEqualTo(maxPrecision))
        {
            throw new ArgumentOutOfRangeException($"Precision cannot be higher than {maxPrecision}.");
        }
    }

    private static Exception GetArgumentError(string message)
    {
        return new ArgumentException(message);
    }
}