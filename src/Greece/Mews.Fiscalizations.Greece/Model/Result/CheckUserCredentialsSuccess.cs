namespace Mews.Fiscalizations.Greece.Model.Result
{
    public class CheckUserCredentialsSuccess
    {
        public CheckUserCredentialsSuccess(bool isAuthorized)
        {
            IsAuthorized = isAuthorized;
        }

        public bool IsAuthorized { get; }
    }
}
