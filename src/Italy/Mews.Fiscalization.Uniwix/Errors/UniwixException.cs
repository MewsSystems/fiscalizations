using System;

namespace Mews.Fiscalization.Uniwix.Errors
{
    public class UniwixException : Exception
    {
        public int Code { get; }

        public string Reason { get; }

        public UniwixException(int code, string reason)
            : base($"Uniwix error, code: {code}, reason: {reason}.")
        {
            Code = code;
            Reason = reason;
        }
    }
}
