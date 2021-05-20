using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum IncomeClassificationCategory
    {
        [XmlEnum("category1_1")]
        CommoditySaleIncome,
        [XmlEnum("category1_2")]
        ProductSaleIncome,
        [XmlEnum("category1_3")]
        ProvisionOfServicesIncome,
        [XmlEnum("category1_4")]
        SaleOfFixedAssetsIncome,
        [XmlEnum("category1_5")]
        OtherIncomeAndProfits,
        [XmlEnum("category1_6")]
        SelfDeliveriesAndSelfSupplies,
        [XmlEnum("category1_7")]
        IncomeOnBehalfOfThirdParties,
        [XmlEnum("category1_8")]
        PastFiscalYearsIncome,
        [XmlEnum("category1_9")]
        FutureFiscalYearsIncome,
        [XmlEnum("category1_10")]
        OtherIncomeAdjustmentAndRegularisationEntries,
        [XmlEnum("category1_95")]
        OtherIncomeRelatedInformation
    }
}
