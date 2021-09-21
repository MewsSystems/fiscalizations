using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Mews.Fiscalizations.Germany.Dto
{
    public enum State
    {
        ACTIVE,
        CANCELLED,
        FINISHED
    }

    public enum PaymentType
    {
        CASH,
        NON_CASH
    }

    public enum VatRateType
    {
        NORMAL,
        REDUCED_1,
        SPECIAL_RATE_1,
        SPECIAL_RATE_2,
        NULL
    }

    public enum ReceiptType
    {
        RECEIPT,
        TRAINING,
        TRANSFER,
        ORDER,
        CANCELLATION,
        ABORT,
        BENEFIT_IN_KIND,
        INVOICE,
        OTHER,
        ANNULATION
    }

    internal sealed class TransactionResponse
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("time_start")]
        public long TimeStart { get; set; }

        [JsonProperty("time_end")]
        public long TimeEnd { get; set; }

        [JsonProperty("client_serial_number")]
        public string ClientSerialNumber { get; set; }

        [JsonProperty("certificate_serial")]
        public string CertificateSerial { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("client_id")]
        public Guid ClientId { get; set; }

        [JsonProperty("schema")]
        public Schema Schema { get; set; }

        [JsonProperty("qr_code_data")]
        public string QrCodeData { get; set; }

        [JsonProperty("revision")]
        public long Revision { get; set; }

        [JsonProperty("latest_revision")]
        public long LatestRevision { get; set; }

        [JsonProperty("log")]
        public Log Log { get; set; }

        [JsonProperty("signature")]
        public Signature Signature { get; set; }

        [JsonProperty("tss_id")]
        public Guid TssId { get; set; }

        [JsonProperty("tss_serial_number")]
        public string TssSerialNumber { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("_env")]
        public string Environment { get; set; }

        [JsonProperty("_version")]
        public string Version { get; set; }
    }

    internal sealed class Log
    {
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("timestamp_format")]
        public string TimestampFormat { get; set; }
    }

    internal sealed class Schema
    {
        [JsonProperty("standard_v1")]
        public StandardV1 StandardV1 { get; set; }
    }

    internal sealed class StandardV1
    {
        [JsonProperty("receipt")]
        public Receipt Receipt { get; set; }
    }

    internal sealed class Receipt
    {
        [JsonProperty("receipt_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReceiptType ReceiptType { get; set; }

        [JsonProperty("amounts_per_vat_rate")]
        public AmountsPerVatRate[] AmountsPerVatRate { get; set; }

        [JsonProperty("amounts_per_payment_type")]
        public AmountsPerPaymentType[] AmountsPerPaymentType { get; set; }
    }

    internal sealed class AmountsPerPaymentType
    {
        [JsonProperty("payment_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType PaymentType { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }

    internal sealed class AmountsPerVatRate
    {
        [JsonProperty("vat_rate")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VatRateType VatRate { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    internal sealed class Signature
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("counter")]
        public int Counter { get; set; }

        [JsonProperty("public_key")]
        public byte[] PublicKey { get; set; }
    }
}
