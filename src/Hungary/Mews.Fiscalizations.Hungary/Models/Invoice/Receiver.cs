using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models;

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

    public static Receiver LocalCompany(LocalTaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
    {
        return new Receiver(new Company(new LocalCompany(taxpayerId, name, address)));
    }

    public static ITry<Receiver, Error> ForeignCompany(Name name, SimpleAddress address, TaxpayerIdentificationNumber taxpayerId = null)
    {
        return Models.ForeignCompany.Create(name, address, taxpayerId).Map(c => new Receiver(new Company(c)));
    }

    public static Receiver Customer()
    {
        return new Receiver(new Customer());
    }
}