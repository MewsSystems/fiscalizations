using System;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Greece.Dto.Xsd
{
    [Serializable]
    public enum UnitOfMeasurement
    {
        [XmlEnum("1")]
        Piece = 1,
        [XmlEnum("2")]
        Kilogram = 2,
        [XmlEnum("3")]
        Litre = 3
    }
}
