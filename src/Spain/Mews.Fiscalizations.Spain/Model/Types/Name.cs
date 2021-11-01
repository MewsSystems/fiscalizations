using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Xml;

namespace Mews.Fiscalizations.Spain.Model
{
    public sealed class Name
    {
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<Name, Error> Create(string value)
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
}
