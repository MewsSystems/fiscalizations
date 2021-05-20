using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Xml;

namespace Mews.Fiscalization.Spain.Model
{
    public sealed class Name
    {
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Name, INonEmptyEnumerable<Error>> Create(string value)
        {
            return StringValidations.LengthInRange(value, 0, 120).FlatMap(v =>
            {
                var validatedName = Try.Create<string, XmlException>(_ => XmlConvert.VerifyXmlChars(v)).MapError(_ => Error.Create("Name contains invalid characters."));
                return validatedName.Map(n => new Name(n));
            });
        }

        public static Name CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}
