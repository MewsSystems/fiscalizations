namespace Mews.Fiscalizations.Germany.V2.Model;

public sealed class Transaction
{
    public Transaction(
        Guid id,
        Guid clientId,
        Guid tssId,
        string clientSerialNumber,
        string tssSerialNumber,
        string number,
        DateTime? startUtc,
        TransactionState state,
        DateTime? endUtc = null,
        string certificateSerial = null,
        Signature signature = null,
        string qrCodeData = null)
    {
        Id = id;
        ClientId = clientId;
        TssId = tssId;
        ClientSerialNumber = clientSerialNumber;
        TssSerialNumber = tssSerialNumber;
        Number = number;
        StartUtc = startUtc;
        State = state;
        EndUtc = endUtc;
        CertificateSerial = certificateSerial;
        Signature = signature;
        QrCodeData = qrCodeData;
    }

    public Guid Id { get; }

    public Guid ClientId { get; }

    public Guid TssId { get; }

    public string ClientSerialNumber { get; }

    public string TssSerialNumber { get; }

    public string Number { get; }

    public DateTime? StartUtc { get; }

    public TransactionState State { get; }

    public DateTime? EndUtc { get; }

    public string CertificateSerial { get; }

    public Signature Signature { get; }

    public string QrCodeData { get; }
}