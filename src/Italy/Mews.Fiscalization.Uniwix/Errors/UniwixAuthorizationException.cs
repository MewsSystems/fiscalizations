using System;

namespace Mews.Fiscalizations.Uniwix.Errors
{
    public class UniwixAuthorizationException : Exception
    {
        public UniwixAuthorizationException()
            : base ("Uniwix authorization failed.")
        {
        }
    }
}