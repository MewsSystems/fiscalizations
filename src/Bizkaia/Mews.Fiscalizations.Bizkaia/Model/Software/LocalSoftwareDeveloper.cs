namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class LocalSoftwareDeveloper
{
    public LocalSoftwareDeveloper(TaxpayerIdentificationNumber nif)
    {
        Nif = Check.IsNotNull(nif, nameof(nif));
    }

    public TaxpayerIdentificationNumber Nif { get; }
}