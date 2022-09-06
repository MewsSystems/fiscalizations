using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class SoftwareIdentification
    {
        public SoftwareIdentification(
            string id,
            string name,
            SoftwareType type,
            string mainVersion,
            string developerName,
            string developerContact,
            string developerCountry = null,
            string developerTaxNumber = null)
        {
            Id = Check.IsNotNull(id, nameof(id));
            Name = Check.IsNotNull(name, nameof(name));
            Type = type;
            MainVersion = Check.IsNotNull(mainVersion, nameof(mainVersion));
            DeveloperName = Check.IsNotNull(developerName, nameof(developerName));
            DeveloperContact = Check.IsNotNull(developerContact, nameof(developerContact));
            DeveloperCountry = developerCountry.ToNonEmptyOption();
            DeveloperTaxNumber = developerTaxNumber.ToNonEmptyOption();
        }

        public string Id { get; }

        public string Name { get; }

        public SoftwareType Type { get; }

        public string MainVersion { get; }

        public string DeveloperName { get; }

        public string DeveloperContact { get; }

        public IOption<string> DeveloperCountry { get; }

        public IOption<string> DeveloperTaxNumber { get; }
    }
}