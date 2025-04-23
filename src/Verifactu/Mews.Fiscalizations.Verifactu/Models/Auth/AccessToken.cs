namespace Mews.Fiscalizations.Verifactu.Models;

public sealed class AccessToken
{
    public AccessToken(string value, FiskalyEnvironment environment, DateTime expirationUtc)
    {
        Value = value;
        Environment = environment;
        ExpirationUtc = expirationUtc;
    }

    public string Value { get; set; }

    public FiskalyEnvironment Environment { get; }

    public DateTime ExpirationUtc { get; set; }
}