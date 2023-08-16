using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public static class ObjectValidations
{
    public static Try<T, Error> NotNull<T>(T value)
        where T : class
    {
        return value.ToTry(v => v is not null, _ => new Error($"{typeof(T).Name} cannot be null."));
    }
}