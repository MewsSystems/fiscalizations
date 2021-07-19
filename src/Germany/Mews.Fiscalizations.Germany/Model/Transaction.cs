using System;

namespace Mews.Fiscalizations.Germany.Model
{
    public sealed class Transaction
    {
        public Transaction(
            Guid id,
            string number,
            DateTime? startUtc,
            TransactionState state,
            DateTime? endUtc = null,
            string certificateSerial = null,
            Signature signature = null,
            string qrCodeData = null)
        {
            Id = id;
            Number = number;
            StartUtc = startUtc;
            State = state;
            EndUtc = endUtc;
            CertificateSerial = certificateSerial;
            Signature = signature;
            QrCodeData = qrCodeData;
        }

        public Guid Id { get; }

        public string Number { get; }

        public DateTime? StartUtc { get; }

        public TransactionState State { get; }

        public DateTime? EndUtc { get; }

        public string CertificateSerial { get; }

        public Signature Signature { get; }

        public string QrCodeData { get; }
    }
}
