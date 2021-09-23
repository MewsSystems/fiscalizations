namespace Mews.Fiscalizations.Germany.V1.Model
{
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

        public bool IsSuccess
        {
            get
            {
                return SuccessResult != null;
            }
        }
    }
}
