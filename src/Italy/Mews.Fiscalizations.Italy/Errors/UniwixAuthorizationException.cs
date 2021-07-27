using System;

namespace Mews.Fiscalizations.Italy.Errors
{
    public class UniwixAuthorizationException : Exception
    {
        public UniwixAuthorizationException()
            : base("Uniwix authorization failed.")
        {
        }
    }
}