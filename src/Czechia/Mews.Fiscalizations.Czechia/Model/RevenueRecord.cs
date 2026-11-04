namespace Mews.Fiscalizations.Czechia.Model;

/// <summary>
/// A single registered sale (the Data element of the Trzba message).
/// </summary>
public sealed class RevenueRecord
{
    /// <param name="taxpayerTaxId">EIČ of the taxpayer sending the message, must match the certificate (eic_popl).</param>
    /// <param name="registrationUnitId">Registrating unit (branch) identifier (id_jednotky).</param>
    /// <param name="registryId">Point of sale identifier (id_pokl).</param>
    /// <param name="billNumber">Transaction sequence number, usually the receipt number (porad_cis).</param>
    /// <param name="created">Local date and time of the sale including its offset (dat_trzby).</param>
    /// <param name="revenue">Financial data of the sale.</param>
    /// <param name="delegatingTaxpayerTaxId">EIČ of the taxpayer who authorized the sender to register the sale on their behalf (eic_poverujiciho).</param>
    /// <param name="isOnBehalfOfMultipleTaxpayers">Whether the sale is registered on behalf of multiple taxpayers, e.g. co-owners (povereni_vice_popl).</param>
    public RevenueRecord(
        TaxIdentifier taxpayerTaxId,
        RegistrationUnitIdentifier registrationUnitId,
        RegistryIdentifier registryId,
        BillNumber billNumber,
        DateTimeOffset created,
        Revenue revenue,
        TaxIdentifier? delegatingTaxpayerTaxId = null,
        bool isOnBehalfOfMultipleTaxpayers = false)
    {
        TaxpayerTaxId = taxpayerTaxId ?? throw new ArgumentNullException(nameof(taxpayerTaxId));
        RegistrationUnitId = registrationUnitId ?? throw new ArgumentNullException(nameof(registrationUnitId));
        RegistryId = registryId ?? throw new ArgumentNullException(nameof(registryId));
        BillNumber = billNumber ?? throw new ArgumentNullException(nameof(billNumber));
        Created = created;
        Revenue = revenue ?? throw new ArgumentNullException(nameof(revenue));
        DelegatingTaxpayerTaxId = delegatingTaxpayerTaxId is null ? Option.Empty<TaxIdentifier>() : Option.Create(delegatingTaxpayerTaxId);
        IsOnBehalfOfMultipleTaxpayers = isOnBehalfOfMultipleTaxpayers;
    }

    public TaxIdentifier TaxpayerTaxId { get; }

    public RegistrationUnitIdentifier RegistrationUnitId { get; }

    public RegistryIdentifier RegistryId { get; }

    public BillNumber BillNumber { get; }

    public DateTimeOffset Created { get; }

    public Revenue Revenue { get; }

    public Option<TaxIdentifier> DelegatingTaxpayerTaxId { get; }

    public bool IsOnBehalfOfMultipleTaxpayers { get; }
}
