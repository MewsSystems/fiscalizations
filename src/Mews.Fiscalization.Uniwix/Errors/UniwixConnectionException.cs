using System;

namespace Mews.Fiscalization.Uniwix.Errors
{
    public class UniwixConnectionException : Exception
    {
        public UniwixConnectionException(Exception innerException)
            : base("Connection with Uniwix couldn't be established.", innerException)
        {
        }
    }
}