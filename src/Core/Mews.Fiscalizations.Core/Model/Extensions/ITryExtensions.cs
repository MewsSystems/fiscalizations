using System;
using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public static class ITryExtensions
{
    public static T GetUnsafe<T>(this Try<T, Error> value)
    {
        return value.Get(error => new ArgumentException(error.Message));
    }
}