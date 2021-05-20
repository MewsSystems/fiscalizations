using System.Text.RegularExpressions;

namespace Mews.Eet
{
    public class Patterns
    {
        public static readonly Regex BillNumber = new Regex("^[0-9a-zA-Z\\.,:;/#\\-_ ]{1,25}$");

        public static readonly Regex RegistryIdentifier = new Regex("^[0-9a-zA-Z\\.,:;/#\\-_ ]{1,20}$");

        public static readonly Regex TaxIdentifier = new Regex("^CZ[0-9]{8,10}$");
    }
}
