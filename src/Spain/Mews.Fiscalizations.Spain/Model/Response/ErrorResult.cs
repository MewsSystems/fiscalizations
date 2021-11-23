using System;

namespace Mews.Fiscalizations.Spain.Model.Response
{
    public sealed class ErrorResult : Exception
    {
        private ErrorResult(string code, string message) : base(message)
        {
            Code = code;
        }

        public string Code { get; }

        public static ErrorResult Create(string code, string message) => new ErrorResult(code, message);
    }
}
