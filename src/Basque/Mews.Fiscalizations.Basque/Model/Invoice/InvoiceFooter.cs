﻿namespace Mews.Fiscalizations.Basque.Model;

public sealed class InvoiceFooter
{
    public InvoiceFooter(Software software, OriginalInvoiceInfo originalInvoiceInfo = null, String1To30 deviceSerialNumber = null)
    {
        Software = Check.IsNotNull(software, nameof(software));
        OriginalInvoiceInfo = originalInvoiceInfo.ToOption();
        DeviceSerialNumber = deviceSerialNumber.ToOption();
    }

    public Software Software { get; }

    public Option<OriginalInvoiceInfo> OriginalInvoiceInfo { get; }

    public Option<String1To30> DeviceSerialNumber { get; }
}