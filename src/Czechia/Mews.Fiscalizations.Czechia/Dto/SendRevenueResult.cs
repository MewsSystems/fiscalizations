using System;
using System.Collections.Generic;
using System.Linq;
using Mews.Eet.Dto.Wsdl;

namespace Mews.Eet.Dto
{
    public class SendRevenueResult
    {
        internal SendRevenueResult(SendRevenueXmlResponse response)
        {
            var confirmation = response.Item as ResponseSuccess;
            var rejection = response.Item as ResponseError;

            var date = confirmation != null ? response.Header.Accepted : response.Header.Rejected;

            Id = String.IsNullOrWhiteSpace(response.Header.MessageUuid) ? (Guid?)null : Guid.Parse(response.Header.MessageUuid);
            Issued = new DateTimeWithTimeZone(date.ToUniversalTime(), TimeZoneInfo.Utc);
            SecurityCode = response.Header.SecurityCode;
            Success = confirmation != null ? new SendRevenueSuccess(confirmation.FiscalCode) : null;
            Error = rejection != null ? new SendRevenueError(new Fault(
                code: rejection.Code,
                message: String.Join("\n", rejection.Text)
            )) : null;
            IsPlayground = confirmation != null ? confirmation.IsPlaygroundSpecified && confirmation.IsPlayground : rejection.IsPlaygroundSpecified && rejection.IsPlayground;
            Warnings = GetWarnings(response.Warning);
        }

        public Guid? Id { get; }

        public bool IsPlayground { get; }

        public DateTimeWithTimeZone Issued { get; }

        public string SecurityCode { get; }

        public bool IsSuccess
        {
            get { return Success != null; }
        }

        public SendRevenueSuccess Success { get; }

        public bool IsError
        {
            get { return Error != null; }
        }

        public SendRevenueError Error { get; }

        public IEnumerable<Fault> Warnings { get; }

        private static IEnumerable<Fault> GetWarnings(ResponseWarning[] warnings)
        {
            if (warnings == null)
            {
                return Enumerable.Empty<Fault>();
            }
            return warnings.Select(w => new Fault(w.Code, String.Join("\n", w.Text)));
        }
    }
}
