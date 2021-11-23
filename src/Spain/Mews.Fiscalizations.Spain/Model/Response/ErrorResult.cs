using System;

namespace Mews.Fiscalizations.Spain.Model.Response
{
    public sealed class ErrorResult
    {
        private ErrorResult(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }

        public static ErrorResult Create(string code, string message) => new ErrorResult(code, message);
    }
}
