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
    Other
    // ANNULATION is intentionally excluded: Fiskaly forbids it for TSS-connected systems (§7.4)
}
