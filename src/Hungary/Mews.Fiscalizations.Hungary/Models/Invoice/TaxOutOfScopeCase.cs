namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// The NAV vatOutOfScope/case value set (Online Számla 3.0 spec, chapter 3.3.2). NAV validates the code, so
/// only these values are offered.
/// </summary>
public enum TaxOutOfScopeCase
{
    /// <summary>ATK - outside the scope of the VAT Act.</summary>
    Atk,

    /// <summary>EUFAD37 - reverse charge transaction in another Member State under Section 37 of the VAT Act.</summary>
    Eufad37,

    /// <summary>EUFADE - reverse charge transaction in another Member State, not under Section 37.</summary>
    Eufade,

    /// <summary>EUE - non-reverse-charge transaction performed in another Member State.</summary>
    Eue,

    /// <summary>HO - transaction in a third country.</summary>
    Ho
}
