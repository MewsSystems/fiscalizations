﻿namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class TaxExemptItem
{
    public TaxExemptItem(Amount amount, CauseOfExemption cause)
    {
        Amount = Check.IsNotNull(amount, nameof(amount));
        Cause = cause;
    }

    public Amount Amount { get; }

    public CauseOfExemption Cause { get; }
}