﻿using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum Art73Type
    {
        [XmlEnum("SI")]
        Yes
    }
}