namespace Mews.Fiscalizations.Hungary.Models;

public class ResponseResult<TResult, TCode>
    where TResult : class
    where TCode : struct
{
    public ResponseResult(
        string requestXml,
        string responseXml,
        TResult successResult = null,
        ErrorResult<ResultErrorCode> generalErrorMessage = null,
        ErrorResult<TCode> operationErrorResult = null)
    {
        RequestXml = requestXml;
        ResponseXml = responseXml;
        SuccessResult = successResult;
        GeneralErrorResult = generalErrorMessage;
        OperationalErrorResult = operationErrorResult;
    }

    public string RequestXml { get; }

    public string ResponseXml { get; }

    public TResult SuccessResult { get; }

    public ErrorResult<ResultErrorCode> GeneralErrorResult { get; }

    public ErrorResult<TCode> OperationalErrorResult { get; }
}