using System;

namespace Mews.Fiscalizations.Italy.Uniwix.Errors
{
    public class UniwixAuthorizationException : Exception
    {
        public UniwixAuthorizationException()
            : base("Uniwix authorization failed.")
        {
        }
    }
}