using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum FiscalRegime
    {
        [XmlEnum("RF01")]
        Ordinary,
        [XmlEnum("RF02")]
        RF02,
        [XmlEnum("RF03")]
        RF03,
        [XmlEnum("RF04")]
        RF04,
        [XmlEnum("RF05")]
        RF05,
        [XmlEnum("RF06")]
        RF06,
        [XmlEnum("RF07")]
        RF07,
        [XmlEnum("RF08")]
        RF08,
        [XmlEnum("RF09")]
        RF09,
        [XmlEnum("RF10")]
        RF10,
        [XmlEnum("RF11")]
        RF11,
        [XmlEnum("RF12")]
        RF12,
        [XmlEnum("RF13")]
        RF13,
        [XmlEnum("RF14")]
        RF14,
        [XmlEnum("RF15")]
        RF15,
        [XmlEnum("RF16")]
        RF16,
        [XmlEnum("RF17")]
        RF17,
        [XmlEnum("RF18")]
        RF18,
        [XmlEnum("RF19")]
        RF19
    }
}