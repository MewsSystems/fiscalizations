using System;
using Mews.Fiscalization.Italy.Dto.Invoice;

namespace Mews.Fiscalization.Italy.Constants
{
    public static class NormativeReference
    {
        public const string ExcludingArticle15 = "escluse ex. art. 15";
        public const string NotSubject = "non soggette";

        public static string GetByInvoiceLineKind(TaxKind taxKind)
        {
            switch (taxKind)
            {
                case TaxKind.ExcludingArticle15:
                    return ExcludingArticle15;
                case TaxKind.NotSubject:
                    return NotSubject;
            }

            throw new InvalidOperationException("Unsupported invoice line kind.");
        }
    }
}
