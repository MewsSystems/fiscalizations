using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Response
{
    public static class ResponseResult
    {
        public static ResponseResult<T> Success<T>(T result) where T : class
        {
            return new ResponseResult<T>(result);
        }

        public static ResponseResult<T> Error<T>(ErrorResult error) where T : class
        {
            return new ResponseResult<T>(error);
        }

        public static ResponseResult<T> Error<T>(string code, string message) where T : class
        {
            return new ResponseResult<T>(new ErrorResult(code, message));
        }
    }

    public class ResponseResult<TResult> : Coproduct2<TResult, ErrorResult>
        where TResult : class
    {
        internal ResponseResult(TResult successResult = null) : base(successResult)
        {
        }

        internal ResponseResult(ErrorResult errorResult = null) : base(errorResult)
        {
        }

        public IOption<TResult> SuccessResult => First;

        public IOption<ErrorResult> ErrorResult => Second;

        public bool IsSuccess => IsFirst;
    }
}
