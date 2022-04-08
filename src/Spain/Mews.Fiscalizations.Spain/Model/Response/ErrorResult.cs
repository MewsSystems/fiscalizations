using System;
using FuncSharp;

namespace Mews.Fiscalizations.Spain.Model.Response
{
    public sealed class ErrorResult
    {
        private ErrorResult(string code, string message)
        {
            Code = code;
            Message = message;
            ErrorType = ConvertErrorType(message);
        }

        public string Code { get; }

        public ErrorType ErrorType { get; }
        public string Message { get; }

        public static ErrorResult Create(string code, string message) => new ErrorResult(code, message);

        private ErrorType ConvertErrorType(string message)
        {
            return message.Match(
                "Codigo[401].Certificado revocado", _ => ErrorType.CertificateRevoked);
        }
    }
}
