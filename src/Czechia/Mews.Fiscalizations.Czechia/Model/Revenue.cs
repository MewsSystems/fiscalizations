namespace Mews.Fiscalizations.Czechia.Model;

public sealed class Revenue
{
    /// <param name="total">Total amount of the sale (celk_trzba).</param>
    /// <param name="depositToBeDrawn">Total amount of payments intended for subsequent drawing or settlement, e.g. vouchers or deposits sold (urceno_cerp_zuct).</param>
    /// <param name="drawnDeposit">Total amount of payments that draw or settle earlier deposits (cerp_zuct).</param>
    public Revenue(CurrencyValue total, CurrencyValue? depositToBeDrawn = null, CurrencyValue? drawnDeposit = null)
    {
        Total = total ?? throw new ArgumentNullException(nameof(total));
        DepositToBeDrawn = depositToBeDrawn is null ? Option.Empty<CurrencyValue>() : Option.Create(depositToBeDrawn);
        DrawnDeposit = drawnDeposit is null ? Option.Empty<CurrencyValue>() : Option.Create(drawnDeposit);
    }

    public CurrencyValue Total { get; }

    public Option<CurrencyValue> DepositToBeDrawn { get; }

    public Option<CurrencyValue> DrawnDeposit { get; }
}
