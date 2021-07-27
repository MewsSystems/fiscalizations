﻿using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum VatDueDate
    {
        [XmlEnum("D")]
        Deferred,
        [XmlEnum("I")]
        Immediate,
        [XmlEnum("S")]
        SplitPayment,
    }
}