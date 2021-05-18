using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceValidationResult
    {
        public InvoiceValidationResult(string message, ValidationResultCode resultCode)
        {
            Message = message;
            ResultCode = resultCode;
        }

        public string Message { get; }

        public ValidationResultCode ResultCode { get; }

        internal static IEnumerable<InvoiceValidationResult> Map(
            IEnumerable<Dto.BusinessValidationResultType> businessValidations,
            IEnumerable<Dto.TechnicalValidationResultType> technicalValidations)
        {
            return Enumerable.Concat(
                businessValidations.OrEmptyIfNull().Select(v => new InvoiceValidationResult(
                    message: v.message,
                    resultCode: (ValidationResultCode)v.validationResultCode
                )),
                technicalValidations.OrEmptyIfNull().Select(v => new InvoiceValidationResult(
                    message: v.message,
                    resultCode: (ValidationResultCode)v.validationResultCode
                ))
            );
        }
    }
}
