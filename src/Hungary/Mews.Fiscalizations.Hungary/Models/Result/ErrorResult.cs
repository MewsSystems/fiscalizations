namespace Mews.Fiscalizations.Hungary.Models;

public sealed class ErrorResult<TCode>
    where TCode : struct
{
    internal ErrorResult(TCode errorCode, string message = null, string rawErrorCode = null)
    {
        Message = message;
        ErrorCode = errorCode;
        RawErrorCode = rawErrorCode;
    }

    public string Message { get; }

    public TCode ErrorCode { get; }

    /// <summary>
    /// The errorCode string exactly as NAV returned it. Kept verbatim so an operator can still act on a code
    /// this library does not know yet, when <see cref="ErrorCode"/> is <see cref="ResultErrorCode.Unknown"/>.
    /// </summary>
    public string RawErrorCode { get; }

    internal static ErrorResult<ResultErrorCode> Map(Dto.GeneralErrorResponse response)
    {
        var errorCode = response.result.errorCode;
        return new ErrorResult<ResultErrorCode>(
            errorCode: MapErrorCode(errorCode),
            message: response.result.message,
            rawErrorCode: errorCode
        );
    }

    internal static ResultErrorCode MapErrorCode(string errorCode)
    {
        return errorCode switch
        {
            "INVALID_SECURITY_USER" or "NOT_REGISTERED_CUSTOMER" or "INVALID_CUSTOMER" or "INVALID_USER_RELATION" => ResultErrorCode.InvalidCredentials,
            "MAINTENANCE_MODE" => ResultErrorCode.MaintenanceMode,
            "FORBIDDEN" => ResultErrorCode.UnauthorizedUser,
            "INVALID_REQUEST_SIGNATURE" => ResultErrorCode.InvalidSigningKey,
            "INVALID_REQUEST" => ResultErrorCode.InvalidRequest,
            "REQUEST_ID_NOT_UNIQUE" => ResultErrorCode.RequestIdNotUnique,
            "INVALID_EXCHANGE_TOKEN" => ResultErrorCode.InvalidExchangeToken,
            "INDEX_NOT_SEQUENTIAL" => ResultErrorCode.IndexNotSequential,
            "REQUEST_VERSION_NOT_ALLOWED" => ResultErrorCode.RequestVersionNotAllowed,
            "INVALID_REQUEST_VERSION" => ResultErrorCode.InvalidRequestVersion,
            "INVALID_HEADER_VERSION" => ResultErrorCode.InvalidHeaderVersion,
            "INVALID_TIMESTAMP" => ResultErrorCode.InvalidTimestamp,
            "INVALID_PASSWORD_HASH_CRYPTO" => ResultErrorCode.InvalidPasswordHashCrypto,
            "INVALID_REQUEST_SIGNATURE_HASH_CRYPTO" => ResultErrorCode.InvalidRequestSignatureHashCrypto,
            "INVALID_PREDECESSOR_TAX_NUMBER" => ResultErrorCode.InvalidPredecessorTaxNumber,
            "TOO_MANY_REQUESTS" => ResultErrorCode.TooManyRequests,
            "OPERATION_FAILED" => ResultErrorCode.OperationFailed,
            "NOT_ALLOWED_EXCEPTION" => ResultErrorCode.NotAllowedException,
            "STATUS_QUERY_NOT_ALLOWED" => ResultErrorCode.StatusQueryNotAllowed,
            "MULTIPLE_QUERY_RESULT_FOUND" => ResultErrorCode.MultipleQueryResultFound,
            // Every BAD_QUERY_PARAM_* code means the same thing to a caller: the query parameters were
            // rejected. They are collapsed rather than enumerated so a new one does not become Unknown.
            not null when errorCode.StartsWith("BAD_QUERY_PARAM_", StringComparison.Ordinal) => ResultErrorCode.InvalidQueryParameters,
            _ => ResultErrorCode.Unknown
        };
    }
}
