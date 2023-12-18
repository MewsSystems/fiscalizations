using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignatureCommitment
{
    public SignatureCommitmentType CommitmentType { get; }

    public List<XmlElement> CommitmentTypeQualifiers { get; }

    public SignatureCommitment(SignatureCommitmentType commitmentType)
    {
        CommitmentType = commitmentType;
        CommitmentTypeQualifiers = [];
    }

    public void AddQualifierFromXml(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        CommitmentTypeQualifiers.Add(doc.DocumentElement);
    }
}