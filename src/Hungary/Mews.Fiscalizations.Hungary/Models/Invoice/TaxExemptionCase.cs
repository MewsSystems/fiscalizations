namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// The NAV vatExemption/case value set (Online Számla 3.0 spec, chapter 3.3.2). NAV validates the code, so
/// only these values are offered.
/// </summary>
public enum TaxExemptionCase
{
    /// <summary>AAM - personal tax exemption (alanyi adómentesség).</summary>
    Aam,

    /// <summary>TAM - tax exempt activity, or exempt due to its public or specific nature.</summary>
    Tam,

    /// <summary>KBAET - tax exempt intra-Community supply, without new means of transport.</summary>
    Kbaet,

    /// <summary>KBAUK - tax exempt intra-Community supply of new means of transport.</summary>
    Kbauk,

    /// <summary>EAM - tax exempt extra-Community supply (export to a third country).</summary>
    Eam,

    /// <summary>NAM - tax exempt on other grounds related to international transactions.</summary>
    Nam
}
