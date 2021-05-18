using System;

namespace Mews.Fiscalization.Spain.Model.Response
{
    public sealed class InvoiceId
    {
        public InvoiceId(string issuer, string number, DateTime date)
        {
            Issuer = issuer;
            Number = number;
            Date = date;
        }

        public string Issuer { get; }

        public string Number { get; }

        public DateTime Date { get; }
    }
}
