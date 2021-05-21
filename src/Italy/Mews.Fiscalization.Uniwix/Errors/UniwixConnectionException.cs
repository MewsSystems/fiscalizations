using System;

namespace Mews.Fiscalizations.Uniwix.Errors
{
    public class UniwixConnectionException : Exception
    {
        public UniwixConnectionException(Exception innerException)
            : base("Connection with Uniwix couldn't be established.", innerException)
        {
        }
    }
}