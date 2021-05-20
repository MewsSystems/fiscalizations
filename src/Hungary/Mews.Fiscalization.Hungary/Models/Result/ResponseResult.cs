namespace Mews.Fiscalization.Hungary.Models
{
    public class ResponseResult<TResult, TCode>
        where TResult : class
        where TCode : struct
    {
        public ResponseResult(TResult successResult = null, ErrorResult<ResultErrorCode> generalErrorMessage = null, ErrorResult<TCode> operationErrorResult = null)
        {
            SuccessResult = successResult;
            GeneralErrorResult = generalErrorMessage;
            OperationalErrorResult = operationErrorResult;
        }

        public TResult SuccessResult { get; }

        public ErrorResult<ResultErrorCode> GeneralErrorResult { get; }

        public ErrorResult<TCode> OperationalErrorResult { get; }
    }
}
