namespace Mews.Fiscalizations.Core.Model;

public sealed class Indexed<T>
{
    public Indexed(int index, T value)
    {
        Index = index;
        Value = value;
    }

    public int Index { get; }

    public T Value { get; }
}