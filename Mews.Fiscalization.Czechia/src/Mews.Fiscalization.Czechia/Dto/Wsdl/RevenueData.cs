using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class RevenueData
    {
        [XmlAttribute(AttributeName = "dic_popl")]
        public string TaxPayerTaxIdentifier { get; set; }

        [XmlAttribute(AttributeName = "dic_poverujiciho")]
        public string MandantingTaxPayerIdentifier { get; set; }

        [XmlAttribute(AttributeName = "id_provoz")]
        public int PremisesIdentifier { get; set; }

        [XmlAttribute(AttributeName = "id_pokl")]
        public string RegistryIdentifier { get; set; }

        [XmlAttribute(AttributeName = "porad_cis")]
        public string BillNumber { get; set; }

        [XmlAttribute(AttributeName = "dat_trzby")]
        public DateTime Accepted { get; set; }

        [XmlAttribute(AttributeName = "celk_trzba")]
        public decimal Total { get; set; }

        [XmlAttribute(AttributeName = "zakl_nepodl_dph")]
        public decimal NotTaxableNet { get; set; }
        [XmlIgnore]
        public bool NotTaxableNetSpecified { get; set; }

        [XmlAttribute(AttributeName = "zakl_dan3")]
        public decimal LowerReducedRateNet { get; set; }

        [XmlIgnore]
        public bool LowerReducedRateNetSpecified { get; set; }

        [XmlAttribute(AttributeName = "dan3")]
        public decimal LowerReducedRateTax { get; set; }

        [XmlIgnore]
        public bool LowerReducedRateTaxSpecified { get; set; }

        [XmlAttribute(AttributeName = "zakl_dan2")]
        public decimal ReducedRateNet { get; set; }

        [XmlIgnore]
        public bool ReducedRateNetSpecified { get; set; }

        [XmlAttribute(AttributeName = "dan2")]
        public decimal ReducedRateTax { get; set; }

        [XmlIgnore]
        public bool ReducedRateTaxSpecified { get; set; }

        [XmlAttribute(AttributeName = "zakl_dan1")]
        public decimal StandartRateNet { get; set; }

        [XmlIgnore]
        public bool StandartRateNetSpecified { get; set; }

        [XmlAttribute(AttributeName = "dan1")]
        public decimal StandartRateTax { get; set; }

        [XmlIgnore]
        public bool StandartRateTaxSpecified { get; set; }

        [XmlAttribute(AttributeName = "cest_sluz")]
        public decimal TravelServices { get; set; }

        [XmlIgnore]
        public bool TravelServicesSpecified { get; set; }

        [XmlAttribute(AttributeName = "pouzit_zboz3")]
        public decimal LowerReducedRateGoods { get; set; }

        [XmlIgnore]
        public bool LowerReducedRateGoodsSpecified { get; set; }

        [XmlAttribute(AttributeName = "pouzit_zboz2")]
        public decimal ReducedRateGoods { get; set; }

        [XmlIgnore]
        public bool ReducedRateGoodsSpecified { get; set; }

        [XmlAttribute(AttributeName = "pouzit_zboz1")]
        public decimal StandardRateGoods { get; set; }

        [XmlIgnore]
        public bool StandardRateGoodsSpecified { get; set; }

        [XmlAttribute(AttributeName = "urceno_cerp_zuct")]
        public decimal Deposit { get; set; }
        [XmlIgnore]
        public bool DepositSpecified { get; set; }

        [XmlAttribute(AttributeName = "cerp_zuct")]
        public decimal DepositUsed { get; set; }

        [XmlIgnore]
        public bool DepositUsedSpecified { get; set; }

        [XmlAttribute(AttributeName = "rezim")]
        public int Mode { get; set; }
    }
}
