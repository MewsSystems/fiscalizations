using Mews.Fiscalizations.Core.Model;
using System;

namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class InvoiceId
{
    public InvoiceId(TaxpayerIdentificationNumber issuer, String1To60 number, DateTime date)
    {
        Issuer = Check.IsNotNull(issuer, nameof(issuer));
        Number = Check.IsNotNull(number, nameof(number));
        Date = date;
    }

    public TaxpayerIdentificationNumber Issuer { get; }

    public String1To60 Number { get; }

    public DateTime Date { get; }
}