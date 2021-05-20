using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceStatus
    {
        public InvoiceStatus(InvoiceState status, IEnumerable<InvoiceValidationResult> validationResults)
        {
            Status = status;
            ValidationResults = validationResults;
        }

        public InvoiceState Status { get; }

        public IEnumerable<InvoiceValidationResult> ValidationResults { get; }

        internal static Indexed<InvoiceStatus> Map(Dto.ProcessingResultType result)
        {
            return new Indexed<InvoiceStatus>(
                index: result.index,
                value: new InvoiceStatus(
                    status: (InvoiceState)result.invoiceStatus,
                    validationResults: InvoiceValidationResult.Map(result.businessValidationMessages, result.technicalValidationMessages).ToArray()
                )
            );
        }
    }
}
