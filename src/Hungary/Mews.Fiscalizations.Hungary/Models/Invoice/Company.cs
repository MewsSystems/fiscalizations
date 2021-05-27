using FuncSharp;

namespace Mews.Fiscalizations.Hungary.Models
{
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
    }
}
