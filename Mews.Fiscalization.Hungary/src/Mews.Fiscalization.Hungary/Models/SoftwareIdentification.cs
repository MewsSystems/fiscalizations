using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class SoftwareIdentification
    {
        public SoftwareIdentification(string id, string name, SoftwareType type, string mainVersion, string developerName, string developerContact, string developerCountry = null, string developerTaxNumber = null)
        {
            Id = Check.IsNotNull(id, nameof(id));
            Name = Check.IsNotNull(name, nameof(name));
            Type = type;
            MainVersion = Check.IsNotNull(mainVersion, nameof(mainVersion));
            DeveloperName = Check.IsNotNull(developerName, nameof(developerName));
            DeveloperContact = Check.IsNotNull(developerContact, nameof(developerContact));
            DeveloperCountry = developerCountry;
            DeveloperTaxNumber = developerTaxNumber;
        }

        public string Id { get; }

        public string Name { get; }

        public SoftwareType Type { get; }

        public string MainVersion { get; }

        public string DeveloperName { get; }

        public string DeveloperContact { get; }

        public string DeveloperCountry { get; }

        public string DeveloperTaxNumber { get; }
    }
}