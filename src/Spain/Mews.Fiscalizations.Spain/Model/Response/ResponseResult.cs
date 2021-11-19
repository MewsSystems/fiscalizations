using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Response
{
    public static class ResponseResult
    {
        public static ResponseResult<T> Success<T>(T result) where T : class => new ResponseResult<T>(successResult: result);

        public static ResponseResult<T> Error<T>(ErrorResult error) where T : class => new ResponseResult<T>(errorResult: error);

        public static ResponseResult<T> Error<T>(string code, string message) where T : class => new ResponseResult<T>(errorResult: new ErrorResult(code, message));
    }

    public class ResponseResult<TResult>
        where TResult : class
    {
        internal ResponseResult(TResult successResult = null, ErrorResult errorResult = null)
        {
            SuccessResult = successResult;
            ErrorResult = errorResult;
        }

        public TResult SuccessResult { get; }

        public ErrorResult ErrorResult { get; }

        public bool IsSuccess => SuccessResult.IsNotNull();
    }
}
