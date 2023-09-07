namespace Mews.Fiscalizations.Hungary.Models;

public sealed class Company : Coproduct2<LocalCompany, ForeignCompany>
{
    public Company(LocalCompany localCompany)
        : base(localCompany)
    {
    }

    public Company(ForeignCompany foreignCompany)
        : base(foreignCompany)
    {
    }

    public Name Name
    {
        get
        {
            return Match(
                local => local.Name,
                foreign => foreign.Name
            );
        }
    }

    public SimpleAddress Address
    {
        get
        {
            return Match(
                local => local.Address,
                foreign => foreign.Address
            );
        }
    }
}