using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum WithheldTaxCategory
    {
        [XmlEnum("1")]
        Interests = 1,
        [XmlEnum("2")]
        Royalties = 2,
        [XmlEnum("3")]
        ManagementConsultantFees = 3,
        [XmlEnum("4")]
        TechnicalProjects = 4,
        [XmlEnum("5")]
        LiquidFuelAndTobaccoIndustryProducts = 5,
        [XmlEnum("6")]
        OtherGoods = 6,
        [XmlEnum("7")]
        ServicesProvision = 7,
        [XmlEnum("8")]
        ArchitectAndEngineerTaxContractualFeesDraftSurveysAndPlans = 8,
        [XmlEnum("9")]
        ArchitectAndEngineerTaxContractualFeesAnyOtherProjects = 9,
        [XmlEnum("10")]
        AttorneyFeeTax = 10,
        [XmlEnum("11")]
        Payroll = 11,
        [XmlEnum("12")]
        MerchantMarineOfficersPayroll = 12,
        [XmlEnum("13")]
        MerchantMarineLowerRankingCrewPayroll = 13,
        [XmlEnum("14")]
        SpecialSolidarityContribution = 14,
        [XmlEnum("15")]
        CompensationForTerminationOfEmployment = 15
    }
}
