using System.Xml;

namespace Mews.Fiscalizations.Core.Model;

public sealed class Name
{
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Try<Name, Error> Create(string value)
    {
        return StringValidations.LengthInRange(value, 0, 120).FlatMap(v =>
        {
            var validatedName = Try.Catch<string, XmlException>(_ => XmlConvert.VerifyXmlChars(v)).MapError(_ => new Error("Name contains invalid characters."));
            return validatedName.Map(n => new Name(n));
        });
    }

    public static Name CreateUnsafe(string value)
    {
        return Create(value).GetUnsafe();
    }
}