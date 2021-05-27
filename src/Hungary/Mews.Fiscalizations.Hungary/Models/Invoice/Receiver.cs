using FuncSharp;

namespace Mews.Fiscalizations.Hungary.Models
{
    public class Receiver : Coproduct2<Customer, Company>
    {
        public Receiver(Customer customer)
            : base(customer)
        {
        }

        public Receiver(Company company)
            : base(company)
        {
        }
    }
}
