﻿using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Uniwix.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum ServiceType
    {
        /// <summary>
        /// Discount
        /// </summary>
        SC,
        /// <summary>
        /// Bonus
        /// </summary>
        PR,
        /// <summary>
        /// Refund
        /// </summary>
        AB,
        /// <summary>
        /// Extra charge
        /// </summary>
        AC,
    }
}