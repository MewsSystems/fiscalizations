namespace Mews.Fiscalizations.Czechia;

public enum EetMode
{
    /// <summary>
    /// The sale is registered and a confirmation code (POK) is returned.
    /// </summary>
    Operational,

    /// <summary>
    /// The message is only validated (overeni="true"). Nothing is registered and a successful check returns error code 0.
    /// </summary>
    Verification
}
