namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Software
    {
        public Software(String1To20 license, Developer developer, String1To120 name, String1To20 version)
        {
            License = license;
            Developer = developer;
            Name = name;
            Version = version;
        }

        public String1To20 License { get; }

        public Developer Developer { get; }

        public String1To120 Name { get; }

        public String1To20 Version { get; }
    }
}
