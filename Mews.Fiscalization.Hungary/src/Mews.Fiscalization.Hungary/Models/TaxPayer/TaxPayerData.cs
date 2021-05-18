using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class TaxPayerData
    {
        public TaxPayerData(string id, string name, Address address, string vatCode, DateTime? infoDate = null)
        {
            Id = id;
            Name = name;
            Address = address;
            VatCode = vatCode;
            InfoDate = infoDate;
        }

        public string Id { get; }

        public string Name { get; }

        public Address Address { get; }

        public string VatCode { get; }

        public DateTime? InfoDate { get; }
    }
}
