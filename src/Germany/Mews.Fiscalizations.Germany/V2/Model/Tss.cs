using System;

namespace Mews.Fiscalizations.Germany.V2.Model;

public class Tss
{
    public Tss(
        Guid id,
        string description,
        TssState state,
        DateTime createdUtc,
        DateTime initializedUtc,
        DateTime disabledUtc,
        string certificate,
        string serialNumber,
        string publicKey,
        int signatureCounter,
        string signatureAlgorithm,
        long transactionCounter)
    {
        Id = id;
        Description = description;
        State = state;
        CreatedUtc = createdUtc;
        InitializedUtc = initializedUtc;
        DisabledUtc = disabledUtc;
        Certificate = certificate;
        SerialNumber = serialNumber;
        PublicKey = publicKey;
        SignatureCounter = signatureCounter;
        SignatureAlgorithm = signatureAlgorithm;
        TransactionCounter = transactionCounter;
    }

    public Guid Id { get; }

    public string Description { get; }

    public TssState State { get; }

    public DateTime CreatedUtc { get; }

    public DateTime InitializedUtc { get; }

    public DateTime DisabledUtc { get; }

    public string Certificate { get; }

    public string SerialNumber { get; }

    public string PublicKey { get; }

    public int SignatureCounter { get; }

    public string SignatureAlgorithm { get; }

    public long TransactionCounter { get; }
}