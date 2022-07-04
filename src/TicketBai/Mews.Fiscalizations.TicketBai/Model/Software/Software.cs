using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Software
    {
        public Software(String1To20 license, Developer developer, String1To120 name, String1To20 version)
        {
            License = Check.IsNotNull(license, nameof(license));
            Developer = Check.IsNotNull(developer, nameof(developer));
            Name = Check.IsNotNull(name, nameof(name));
            Version = Check.IsNotNull(version, nameof(version));
        }

        public String1To20 License { get; }

        public Developer Developer { get; }

        public String1To120 Name { get; }

        public String1To20 Version { get; }

        public static Software LocalSoftwareDeveloper(TaxpayerIdentificationNumber nif, String1To20 license, String1To120 name, String1To20 version)
        {
            return new Software(license, new Developer(new LocalDeveloper(nif)), name, version);
        }

        public static Software ForeignSoftwareDeveloper(IdType idType, String1To20 id, String1To20 license, String1To120 name, String1To20 version, Country country = null)
        {
            return new Software(license, new Developer(new ForeignDeveloper(idType, id, country)), name, version);
        }
    }
}