namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignatureCommitmentType(string uri)
{
    public static readonly SignatureCommitmentType ProofOfOrigin = new("http://uri.etsi.org/01903/v1.2.2#ProofOfOrigin");
    public static readonly SignatureCommitmentType ProofOfReceipt = new("http://uri.etsi.org/01903/v1.2.2#ProofOfReceipt");
    public static readonly SignatureCommitmentType ProofOfDelivery = new("http://uri.etsi.org/01903/v1.2.2#ProofOfDelivery");
    public static readonly SignatureCommitmentType ProofOfSender = new("http://uri.etsi.org/01903/v1.2.2#ProofOfSender");
    public static readonly SignatureCommitmentType ProofOfApproval = new("http://uri.etsi.org/01903/v1.2.2#ProofOfApproval");
    public static readonly SignatureCommitmentType ProofOfCreation = new("http://uri.etsi.org/01903/v1.2.2#ProofOfCreation");

    public string URI { get; } = uri;
}