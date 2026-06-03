namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public enum ProcessType
{
    Receipt,
    Transfer,
    Order,
    Cancellation,
    Training,
    BenefitInKind,
    Invoice,
    Other,
    // The spec value "AVBelegabbruch" (document abort) is intentionally omitted: Fiskaly forbids
    // it for TSS-connected systems (§7.4), so exposing it would let a caller build a payload that
    // Fiskaly rejects. Add it here only if a non-TSS integration ever needs it.
}
