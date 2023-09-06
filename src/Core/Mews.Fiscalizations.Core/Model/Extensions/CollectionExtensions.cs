using System.Globalization;

namespace Mews.Fiscalizations.Core.Model;

public static class CollectionExtensions
{
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
    {
        return new HashSet<T>(source, comparer);
    }

    public static bool IsSequential<T>(this IEnumerable<T> source, Func<T, int> indexGetter, int startIndex)
    {
        return source.Select(indexGetter).IsSequential(startIndex);
    }

    public static bool IsSequential(this IEnumerable<int> source, int startIndex)
    {
        return source.Select((value, index) => (value, index)).All(x => x.value == startIndex + x.index);
    }

    public static string MkString(this IEnumerable<string> values, string separator = "", string prefix = "", string suffix = "", bool skipEmpty = false)
    {
        var allItems = values.OrEmptyIfNull().Select(i => Convert.ToString(i, CultureInfo.InvariantCulture));
        var items = skipEmpty.Match(t => allItems.Where(s => s.NonEmpty()), f => allItems);
        return $"{prefix}{String.Join(separator, items)}{suffix}";
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
}