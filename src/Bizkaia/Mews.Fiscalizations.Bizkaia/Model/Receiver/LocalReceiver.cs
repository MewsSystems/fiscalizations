﻿namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class LocalReceiver : ReceiverInfo
{
    public LocalReceiver(TaxpayerIdentificationNumber taxpayerIdentificationNumber, Name name, PostalCode postalCode, String1To250 address)
        : base(name, postalCode, address)
    {
        TaxpayerIdentificationNumber = Check.IsNotNull(taxpayerIdentificationNumber, nameof(taxpayerIdentificationNumber));
    }

    public TaxpayerIdentificationNumber TaxpayerIdentificationNumber { get; }

    public static Try<LocalReceiver, Error> Create(string nif, Name name, PostalCode postalCode, String1To250 address)
    {
        var taxpayerNumber = TaxpayerIdentificationNumber.Create(Countries.Spain, nif, isCountryCodePrefixAllowed: false);
        return taxpayerNumber.Map(n => new LocalReceiver(n, name, postalCode, address));
    }
}