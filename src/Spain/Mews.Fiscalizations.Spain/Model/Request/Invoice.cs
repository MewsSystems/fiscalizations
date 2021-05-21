using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public class Invoice
    {
        public Invoice(
            TaxPeriod taxPeriod,
            InvoiceId id,
            InvoiceType type,
            SchemeOrEffect schemeOrEffect,
            String0To500 description,
            TaxBreakdown taxBreakdown,
            bool issuedByThirdParty,
            CounterPartyCompany counterPartyCompany)
        {
            TaxPeriod = Check.IsNotNull(taxPeriod, nameof(taxPeriod));
            Id = Check.IsNotNull(id, nameof(id));
            Type = type;
            SchemeOrEffect = schemeOrEffect;
            Description = Check.IsNotNull(description, nameof(description));
            TaxBreakdown = Check.IsNotNull(taxBreakdown, nameof(taxBreakdown));
            IssuedByThirdParty = issuedByThirdParty;
            CounterPartyCompany = Check.IsNotNull(counterPartyCompany, nameof(counterPartyCompany));
        }

        public TaxPeriod TaxPeriod { get; }

        public InvoiceId Id { get; }

        public InvoiceType Type { get; }

        public SchemeOrEffect SchemeOrEffect { get; }

        public String0To500 Description { get; }

        public TaxBreakdown TaxBreakdown { get; }

        public bool IssuedByThirdParty { get; }

        public CounterPartyCompany CounterPartyCompany { get; }

        public decimal TotalAmount { get { return TaxBreakdown.TotalAmount; } }
    }
}
