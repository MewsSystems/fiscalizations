using System;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class TaxPayerData
    {
        public TaxPayerData(string id, string name, Address address, string vatCode, IncorporationType incorporationType, DateTime? infoDate = null)
        {
            Id = id;
            Name = name;
            Address = address;
            VatCode = vatCode;
            IncorporationType = incorporationType;
            InfoDate = infoDate;
        }

        public string Id { get; }

        public string Name { get; }

        public Address Address { get; }

        public string VatCode { get; }

        public IncorporationType IncorporationType { get; }

        public DateTime? InfoDate { get; }
    }
}
