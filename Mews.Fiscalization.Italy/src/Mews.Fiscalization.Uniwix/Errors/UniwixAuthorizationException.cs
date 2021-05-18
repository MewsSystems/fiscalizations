using System;

namespace Mews.Fiscalization.Uniwix.Errors
{
    public class UniwixAuthorizationException : Exception
    {
        public UniwixAuthorizationException()
            : base ("Uniwix authorization failed.")
        {
        }
    }
}