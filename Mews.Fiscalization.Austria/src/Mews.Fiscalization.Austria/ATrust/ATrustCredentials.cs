namespace Mews.Fiscalization.Austria.ATrust
{
    public sealed class ATrustCredentials
    {
        public ATrustCredentials(ATrustUserIdentifier user, string password)
        {
            User = user;
            Password = password;
        }

        public ATrustUserIdentifier User { get; }

        public string Password { get; }
    }
}
