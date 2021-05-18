namespace Mews.Fiscalization.Uniwix.Communication
{
    public class UniwixClientConfiguration
    {
        public UniwixClientConfiguration(string key, string password)
        {
            Key = key;
            Password = password;
        }

        public string Key { get; }

        public string Password { get; }
    }
}