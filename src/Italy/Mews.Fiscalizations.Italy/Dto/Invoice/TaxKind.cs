using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Invoice;

[Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
public enum TaxKind
{
    [XmlEnum("N1")]
    ExcludedArticle15,
    [XmlEnum("N2")]
    NotSubject,
    [XmlEnum("N2.1")]
    NotSubjectArticle7,
    [XmlEnum("N2.2")]
    NotSubjectOther,
    [XmlEnum("N3")]
    NonTaxable,
    [XmlEnum("N3.1")]
    NonTaxableExports,
    [XmlEnum("N3.2")]
    NonTaxableIntraCommunity,
    [XmlEnum("N3.3")]
    NonTaxableTransfersToSanMarino,
    [XmlEnum("N3.4")]
    NonTaxableOperationsAssimilatedToSalesOnExport,
    [XmlEnum("N3.5")]
    NonTaxableFollowingDeclarationsOfIntent,
    [XmlEnum("3.6")]
    NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation,
    [XmlEnum("N4")]
    Exempt,
    [XmlEnum("N5")]
    MarginScheme
}