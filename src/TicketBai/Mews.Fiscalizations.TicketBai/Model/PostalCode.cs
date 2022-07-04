using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Text.RegularExpressions;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class PostalCode
    {
        private PostalCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ITry<PostalCode, Error> Create(string value)
        {
            return StringValidations.RegexMatch(value, new Regex("^[0-9]{1,5}$")).Map(v => new PostalCode(v));
        }

        public static PostalCode CreateUnsafe(string value)
        {
            return Create(value).GetUnsafe();
        }
    }
}