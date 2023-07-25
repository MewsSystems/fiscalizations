using System.Xml.Serialization;

namespace Mews.Fiscalizations.Italy.Dto.Notifications;

[XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
public enum ClientOutcome
{
    [XmlEnum("Ec01")]
    Approved,
    [XmlEnum("Ec02")]
    Rejected,
}