using FuncSharp;

namespace Mews.Fiscalization.Core.Model
{
    public static class ObjectValidations
    {
        public static ITry<T, Error> NotNull<T>(T value)
        {
            return value.ToTry(v => v.IsNotNull(), _ => new Error("Value cannot be null."));
        }
    }
}