using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Spain.Model.Response
{
    public sealed class ErrorResult
    {
        public ErrorResult(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }
}
