using System;

namespace Mews.Fiscalizations.Italy.Uniwix.Communication.Errors
{
    public class UniwixAuthorizationException : Exception
    {
        public UniwixAuthorizationException()
            : base("Uniwix authorization failed.")
        {
        }
    }
}