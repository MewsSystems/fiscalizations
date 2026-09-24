namespace Mews.Fiscalizations.Hungary.Tests;

public sealed class ErrorResultTests
{
    [TestCase("INVALID_SECURITY_USER", ResultErrorCode.InvalidCredentials)]
    [TestCase("NOT_REGISTERED_CUSTOMER", ResultErrorCode.InvalidCredentials)]
    [TestCase("INVALID_CUSTOMER", ResultErrorCode.InvalidCredentials)]
    [TestCase("INVALID_USER_RELATION", ResultErrorCode.InvalidCredentials)]
    [TestCase("MAINTENANCE_MODE", ResultErrorCode.MaintenanceMode)]
    [TestCase("FORBIDDEN", ResultErrorCode.UnauthorizedUser)]
    [TestCase("INVALID_REQUEST_SIGNATURE", ResultErrorCode.InvalidSigningKey)]
    [TestCase("INVALID_REQUEST", ResultErrorCode.InvalidRequest)]
    [TestCase("REQUEST_ID_NOT_UNIQUE", ResultErrorCode.RequestIdNotUnique)]
    [TestCase("INVALID_EXCHANGE_TOKEN", ResultErrorCode.InvalidExchangeToken)]
    [TestCase("INDEX_NOT_SEQUENTIAL", ResultErrorCode.IndexNotSequential)]
    [TestCase("REQUEST_VERSION_NOT_ALLOWED", ResultErrorCode.RequestVersionNotAllowed)]
    [TestCase("INVALID_REQUEST_VERSION", ResultErrorCode.InvalidRequestVersion)]
    [TestCase("INVALID_HEADER_VERSION", ResultErrorCode.InvalidHeaderVersion)]
    [TestCase("INVALID_TIMESTAMP", ResultErrorCode.InvalidTimestamp)]
    [TestCase("INVALID_PASSWORD_HASH_CRYPTO", ResultErrorCode.InvalidPasswordHashCrypto)]
    [TestCase("INVALID_REQUEST_SIGNATURE_HASH_CRYPTO", ResultErrorCode.InvalidRequestSignatureHashCrypto)]
    [TestCase("INVALID_PREDECESSOR_TAX_NUMBER", ResultErrorCode.InvalidPredecessorTaxNumber)]
    [TestCase("TOO_MANY_REQUESTS", ResultErrorCode.TooManyRequests)]
    [TestCase("OPERATION_FAILED", ResultErrorCode.OperationFailed)]
    [TestCase("NOT_ALLOWED_EXCEPTION", ResultErrorCode.NotAllowedException)]
    [TestCase("STATUS_QUERY_NOT_ALLOWED", ResultErrorCode.StatusQueryNotAllowed)]
    [TestCase("MULTIPLE_QUERY_RESULT_FOUND", ResultErrorCode.MultipleQueryResultFound)]
    public void MapErrorCode_KnownCode_MapsToItsCode(string navCode, ResultErrorCode expected)
    {
        Assert.That(ErrorResult<ResultErrorCode>.MapErrorCode(navCode), Is.EqualTo(expected));
    }

    [TestCase("BAD_QUERY_PARAM_OVERLAP")]
    [TestCase("BAD_QUERY_PARAM_RANGE_EXCEEDED")]
    [TestCase("BAD_QUERY_PARAM_EQ_NOT_STANDALONE")]
    [TestCase("BAD_QUERY_PARAM_OPERATOR_COLLISION")]
    [TestCase("BAD_QUERY_PARAM_SUPPLIER_NOT_EXPECTED")]
    [TestCase("BAD_QUERY_PARAM_SUPPLIER_EXPECTED")]
    public void MapErrorCode_QueryParameterCode_MapsToInvalidQueryParameters(string navCode)
    {
        Assert.That(ErrorResult<ResultErrorCode>.MapErrorCode(navCode), Is.EqualTo(ResultErrorCode.InvalidQueryParameters));
    }

    [Test]
    public void MapErrorCode_UnknownCode_DoesNotThrowAndMapsToUnknown()
    {
        // NAV documents the error code set as deliberately open ended so it can be extended without breaking
        // clients. Throwing on an unrecognised code turned every future NAV addition into an opaque
        // "is not implemented" note on the fiscal record.
        Assert.That(ErrorResult<ResultErrorCode>.MapErrorCode("SOME_FUTURE_NAV_CODE"), Is.EqualTo(ResultErrorCode.Unknown));
    }

    [Test]
    public void MapErrorCode_NullCode_MapsToUnknown()
    {
        Assert.That(ErrorResult<ResultErrorCode>.MapErrorCode(null), Is.EqualTo(ResultErrorCode.Unknown));
    }

    [Test]
    public void Map_KeepsTheRawCodeAndMessageNavReturned()
    {
        var response = new Dto.GeneralErrorResponse
        {
            result = new Dto.BasicResultType
            {
                errorCode = "SOME_FUTURE_NAV_CODE",
                message = "Something NAV explained that we should show the operator."
            }
        };

        var error = ErrorResult<ResultErrorCode>.Map(response.result);

        Assert.That(error.ErrorCode, Is.EqualTo(ResultErrorCode.Unknown));
        Assert.That(error.RawErrorCode, Is.EqualTo("SOME_FUTURE_NAV_CODE"));
        Assert.That(error.Message, Is.EqualTo("Something NAV explained that we should show the operator."));
    }
}
