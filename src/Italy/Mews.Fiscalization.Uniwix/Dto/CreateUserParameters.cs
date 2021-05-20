namespace Mews.Fiscalization.Uniwix.Dto
{
    public class CreateUserParameters
    {
        public CreateUserParameters(string taxIdentificationNumber, string userName, string description)
        {
            TaxIdentificationNumber = taxIdentificationNumber;
            UserName = userName;
            Description = description;
        }

        public string TaxIdentificationNumber { get; }

        public string UserName { get; }

        public string Description { get; }
    }
}