using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum ExpenseClassificationCategory
    {
        [XmlEnum("category2_1")]
        CommodityPurchases,
        [XmlEnum("category2_2")]
        RawAndAdjuvantMaterialPurchases,
        [XmlEnum("category2_3")]
        ServicesReceipt,
        [XmlEnum("category2_4")]
        GeneralExpensesSubjectToVatDeduction,
        [XmlEnum("category2_5")]
        GeneralExpensesNotSubjectToVatDeduction,
        [XmlEnum("category2_6")]
        PersonnelFeesAndBenefits,
        [XmlEnum("category2_7")]
        FixedAssetPurchases,
        [XmlEnum("category2_8")]
        FixedAssetAmortisations,
        [XmlEnum("category2_9")]
        ExpensesOnBehalfOfThirdParties,
        [XmlEnum("category2_10")]
        PastFiscalYearsExpenses,
        [XmlEnum("category2_11")]
        FutureFiscalYearsExpenses,
        [XmlEnum("category2_12")]
        OtherExpenseAdjustmentAndRegularisationEntries,
        [XmlEnum("category2_13")]
        StockAtPeriodStart,
        [XmlEnum("category2_14")]
        StockAtPeriodEnd,
        [XmlEnum("category2_95")]
        OtherExpenseRelatedInformation
    }
}
