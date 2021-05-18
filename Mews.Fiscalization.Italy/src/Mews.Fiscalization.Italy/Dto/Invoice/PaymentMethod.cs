using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum PaymentMethod
    {
        [XmlEnum("MP01")]
        Cash,
        [XmlEnum("MP02")]
        Check,
        [XmlEnum("MP03")]
        BankersDraft,
        [XmlEnum("MP04")]
        CashAtTreasury,
        [XmlEnum("MP05")]
        Transfer,
        [XmlEnum("MP06")]
        MoneyOrder,
        [XmlEnum("MP07")]
        BankBulletin,
        [XmlEnum("MP08")]
        PaymentCard,
        [XmlEnum("MP09")]
        DirectDebit,
        [XmlEnum("MP10")]
        UtilityDirectDebit,
        [XmlEnum("MP11")]
        FastDirectDebit,
        [XmlEnum("MP12")]
        CollectionOrder,
        [XmlEnum("MP13")]
        PaymentByNotice,
        [XmlEnum("MP14")]
        QuittanceStateTaxOffice,
        [XmlEnum("MP15")]
        GiroOnSpecialAccountingAccount,
        [XmlEnum("MP16")]
        BankingDomiciliation,
        [XmlEnum("MP17")]
        PostalDomiciliation,
        [XmlEnum("MP18")]
        BulletinOfPostal,
        [XmlEnum("MP19")]
        SepaDirectDebit,
        [XmlEnum("MP20")]
        SepaDirectDebitCore,
        [XmlEnum("MP21")]
        SepaDirectDebitB2B
    }
}