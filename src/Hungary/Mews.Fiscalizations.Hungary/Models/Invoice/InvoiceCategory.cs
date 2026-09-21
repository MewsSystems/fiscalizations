namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// NAV invoiceCategory. SIMPLIFIED is deliberately absent: Mews does not issue simplified invoices in
/// Hungary, and declaring one changes which value elements NAV expects on every line and in the summary.
/// </summary>
public enum InvoiceCategory
{
    /// <summary>A single supply, or several supplies sharing one delivery date (Áfa tv. 163. §).</summary>
    Normal,

    /// <summary>
    /// Gyűjtőszámla - one invoice covering several supplies with different delivery dates within a
    /// settlement period (Áfa tv. 164. §). NAV then expects a delivery date, and for a foreign currency an
    /// exchange rate, on every line.
    /// </summary>
    Aggregate
}
