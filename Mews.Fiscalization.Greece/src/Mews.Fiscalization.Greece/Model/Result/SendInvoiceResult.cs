using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceResult
    {
        public SendInvoiceResult(string invoiceIdentifier, long invoiceRegistrationNumber, bool invoiceRegistrationNumberSpecified, IEnumerable<ResultError> errors)
        {
            Errors = errors;

            if (errors == null || errors.Count() == 0)
            {
                Success = new SendInvoiceSuccess(invoiceIdentifier, invoiceRegistrationNumber, invoiceRegistrationNumberSpecified);
            }
        }

        public bool IsSuccess
        {
            get { return Success != null; }
        }

        public SendInvoiceSuccess Success { get; }

        public IEnumerable<ResultError> Errors { get; }
    }
}
