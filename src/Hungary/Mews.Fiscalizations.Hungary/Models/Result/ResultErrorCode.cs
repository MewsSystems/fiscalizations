namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// The NAV errorCode values a synchronous call can return (Online Számla 3.0 spec, chapter 3.2). NAV states
/// the set is open ended and may grow, which is what <see cref="Unknown"/> is for: an unrecognised code is
/// reported with the raw code and NAV's own message rather than failing the caller with an exception.
/// </summary>
public enum ResultErrorCode
{
    Unknown = 0,
    InvalidCredentials = 1,
    MaintenanceMode = 2,
    UnauthorizedUser = 3,
    InvalidSigningKey = 4,
    InvalidRequest = 5,
    RequestIdNotUnique = 6,
    InvalidExchangeToken = 7,
    IndexNotSequential = 8,
    RequestVersionNotAllowed = 9,
    InvalidRequestVersion = 10,
    InvalidHeaderVersion = 11,
    InvalidTimestamp = 12,
    InvalidPasswordHashCrypto = 13,
    InvalidRequestSignatureHashCrypto = 14,
    InvalidPredecessorTaxNumber = 15,
    TooManyRequests = 16,
    OperationFailed = 17,
    NotAllowedException = 18,
    StatusQueryNotAllowed = 19,
    MultipleQueryResultFound = 20,
    InvalidQueryParameters = 21
}
