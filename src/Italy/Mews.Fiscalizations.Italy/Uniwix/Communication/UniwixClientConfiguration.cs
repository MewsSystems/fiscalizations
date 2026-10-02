namespace Mews.Fiscalizations.Italy.Uniwix.Communication;

public class UniwixClientConfiguration
{
    public static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(30);

    public UniwixClientConfiguration(string key, string password, TimeSpan? requestTimeout = null)
    {
        Key = key;
        Password = password;
        RequestTimeout = requestTimeout ?? DefaultRequestTimeout;
    }

    public string Key { get; }

    public string Password { get; }

    public TimeSpan RequestTimeout { get; }
}
