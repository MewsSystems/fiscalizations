namespace Mews.Fiscalization.Spain.Model.Response
{
    public sealed class LocalCompany
    {
        public LocalCompany(string name, string taxpayerNumber)
        {
            Name = name;
            TaxpayerNumber = taxpayerNumber;
        }

        public string Name { get; }

        public string TaxpayerNumber { get; }
    }
}
