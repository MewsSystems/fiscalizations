namespace Mews.Fiscalizations.Czechia;

public enum EetEnvironment
{
    Production,
    Playground
}

internal static class EetEnvironmentExtensions
{
    public static Uri GetEndpoint(this EetEnvironment environment)
    {
        return environment switch
        {
            EetEnvironment.Production => new Uri("https://trzbyeet.gov.cz/eet/services/EETServiceSOAP/v4"),
            EetEnvironment.Playground => new Uri("https://pg.trzbyeet.gov.cz/eet/services/EETServiceSOAP/v4"),
            _ => throw new ArgumentOutOfRangeException(nameof(environment), environment, null)
        };
    }
}
