﻿using Newtonsoft.Json;
using System;

namespace Mews.Fiscalization.Germany.Dto
{
    public enum TssState
    {
        UNINITIALIZED,
        INITIALIZED,
        DISABLED
    }

    public sealed class TssResponse
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public TssState State { get; set; }

        [JsonProperty("time_creation")]
        public long TimeCreation { get; set; }

        [JsonProperty("time_init")]
        public long TimeInit { get; set; }

        [JsonProperty("time_disable")]
        public long TimeDisable { get; set; }

        [JsonProperty("certificate")]
        public string Certificate { get; set; }

        [JsonProperty("certificate_serial")]
        public string CertificateSerial { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("signature_counter")]
        public int SignatureCounter { get; set; }

        [JsonProperty("signature_algorithm")]
        public string SignatureAlgorithm { get; set; }

        [JsonProperty("signature_timestamp_format")]
        public string SignatureTimestampFormat { get; set; }

        [JsonProperty("transaction_counter")]
        public long TransactionCounter { get; set; }
    }
}
