using FuncSharp;
using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public static class ObjectValidations
    {
        public static ITry<T, IEnumerable<Error>> NotNull<T>(T value)
        {
            return value.ToTry(v => v.IsNotNull(), _ => new Error("Value cannot be null.").ToEnumerable());
        }
    }
}