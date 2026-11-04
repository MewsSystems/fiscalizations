namespace Mews.Fiscalizations.Czechia;

/// <summary>
/// The EET endpoint returned a response that is not an EET acknowledgement or error message (e.g. a SOAP fault or an HTTP error page).
/// </summary>
public sealed class EetException : Exception
{
    public EetException(string message, string response, Exception? innerException = null)
        : base(message, innerException)
    {
        Response = response;
    }

    public string Response { get; }
}
