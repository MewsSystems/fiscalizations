using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public enum TipoCassaType
    {
        TC01,
        TC02,
        TC03,
        TC04,
        TC05,
        TC06,
        TC07,
        TC08,
        TC09,
        TC10,
        TC11,
        TC12,
        TC13,
        TC14,
        TC15,
        TC16,
        TC17,
        TC18,
        TC19,
        TC20,
        TC21,
        TC22,
    }
}