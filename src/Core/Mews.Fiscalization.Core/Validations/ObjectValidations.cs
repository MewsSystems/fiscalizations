using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class ObjectValidations
    {
        public static ITry<T, INonEmptyEnumerable<Error>> NotNull<T>(T value)
        {
            return value.ToTry(v => v.IsNotNull(), _ => Error.Create("Value cannot be null."));
        }
    }
}