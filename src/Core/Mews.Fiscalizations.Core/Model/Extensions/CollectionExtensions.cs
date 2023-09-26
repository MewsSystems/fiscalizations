using System.Diagnostics.Contracts;

namespace Mews.Fiscalizations.Core.Model;

public static class CollectionExtensions
{
    public static bool IsSequential<T>(this IEnumerable<T> source, Func<T, int> indexGetter, int startIndex)
    {
        return source.Select(indexGetter).IsSequential(startIndex);
    }

    public static bool IsSequential(this IEnumerable<int> source, int startIndex)
    {
        return source.Select((value, index) => (value, index)).All(x => x.value == startIndex + x.index);
    }

    [Pure]
    public static bool IsEmptyOrNull<T>(this IReadOnlyCollection<T> source)
    {
        return source is null || source.Count == 0;
    }

    public static Option<T> SafeFirstOption<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        return source is null
            ? Option.Empty<T>()
            : source.Where(predicate).SafeFirstOption();
    }

    public static Option<T> SafeFirstOption<T>(this IEnumerable<T> source)
    {
        switch (source)
        {
            case null:
                return Option.Empty<T>();
            case IReadOnlyList<T> list:
                return list.Count == 0
                    ? Option.Empty<T>()
                    : Option.Valued(list[0]);
            default:
            {
                using var enumerator = source.GetEnumerator();
                return enumerator.MoveNext()
                    ? Option.Valued(enumerator.Current)
                    : Option.Empty<T>();
            }
        }
    }
}