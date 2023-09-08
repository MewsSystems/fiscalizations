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

    /// <summary>
    /// Retuns an empty Enumerable if source is null, otherwise source.
    /// </summary>
    public static IEnumerable<TSource> OrEmptyIfNull<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null)
        {
            return Enumerable.Empty<TSource>();
        }

        return source;
    }

    public static bool NonEmptyNorNull<T>(this IEnumerable<T> source)
    {
        return source is not null && source.Any();
    }

    public static bool IsEmptyOrNull<T>(this IEnumerable<T> source)
    {
        return source is null || !source.Any();
    }

    [Pure]
    public static bool NonEmptyNorNull<T>(this IReadOnlyCollection<T> source)
    {
        return source is not null && source.Count > 0;
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
                return list.SafeFirstOption();
            default:
            {
                using var enumerator = source.GetEnumerator();
                return enumerator.MoveNext()
                    ? Option.Valued(enumerator.Current)
                    : Option.Empty<T>();
            }
        }
    }

    [Pure]
    public static Option<T> SafeFirstOption<T>(this IReadOnlyList<T> list)
    {
        return list is null || list.Count == 0
            ? Option.Empty<T>()
            : Option.Valued(list[0]);
    }
}