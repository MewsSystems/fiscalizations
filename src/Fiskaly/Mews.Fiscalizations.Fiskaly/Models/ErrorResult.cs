namespace Mews.Fiscalizations.Fiskaly.Models;

public sealed record ErrorResult(int Status, string Code, string Error, string Message);