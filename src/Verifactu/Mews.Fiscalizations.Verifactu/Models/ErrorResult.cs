namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record ErrorResult(int Status, string Code, string Error, string Message);