using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class InvoiceParty :  Coproduct2<LocalInvoiceParty, ForeignInvoiceParty>
    {
        public InvoiceParty(LocalInvoiceParty localParty)
            : base(localParty)
        {
        }

        public InvoiceParty(ForeignInvoiceParty foreeignParty)
            : base(foreeignParty)
        {
        }

        public InvoicePartyInfo Info
        {
            get
            {
                return Match(
                    localInvoiceParty => localInvoiceParty.Info,
                    foreignInvoiceParty => foreignInvoiceParty.Info
                );
            }
        }

        public Country Country
        {
            get
            {
                return Match(
                    localInvoiceParty => localInvoiceParty.Country,
                    foreignInvoiceParty => foreignInvoiceParty.Country
                );
            }
        }

        public static ITry<InvoiceParty, INonEmptyEnumerable<Error>> Create(InvoicePartyInfo info, Country country)
        {
            return ObjectValidations.NotNull(country).FlatMap(c =>
            {
                var validatedInfo = ObjectValidations.NotNull(info);
                return validatedInfo.FlatMap(i =>
                {
                    if (c == Countries.Greece)
                    {
                        return LocalInvoiceParty.Create(i).Map(p => new InvoiceParty(p));
                    }

                    return ForeignInvoiceParty.Create(i, c).Map(p => new InvoiceParty(p));
                });
            });
        }
    }
}
