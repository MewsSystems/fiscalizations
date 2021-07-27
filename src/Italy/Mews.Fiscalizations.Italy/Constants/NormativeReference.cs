using System;
using FuncSharp;
using Mews.Fiscalizations.Uniwix.Dto.Invoice;

namespace Mews.Fiscalizations.Uniwix.Constants
{
    public static class NormativeReference
    {
        public const string ExcludingArticle15 = "escluse ex. art. 15";
        public const string NotSubject = "non soggette";

        public static string GetByInvoiceLineKind(TaxKind taxKind)
        {
            return taxKind.Match(
                TaxKind.ExcludingArticle15, _ => ExcludingArticle15,
                TaxKind.NotSubject, _ => NotSubject,
                _ => throw new InvalidOperationException("Unsupported invoice line kind.")
            );
        }
    }
}
