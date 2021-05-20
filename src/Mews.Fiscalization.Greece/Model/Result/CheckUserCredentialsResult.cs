namespace Mews.Fiscalization.Greece.Model.Result
{
    public class CheckUserCredentialsResult
    {
        public CheckUserCredentialsResult(bool? isAuthorized = null, ResultError error = null)
        {
            Error = error;

            if (Error == null && isAuthorized != null)
            {
                Success = new CheckUserCredentialsSuccess(isAuthorized.Value);
            }
        }

        public bool IsSuccess
        {
            get { return Success != null; }
        }

        public CheckUserCredentialsSuccess Success { get; }

        public ResultError Error { get; }
    }
}
