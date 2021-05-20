using Newtonsoft.Json;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class CertificateInfo
    {
        [JsonProperty("Signaturzertifikat")]
        public string Certificate { get; set; }

        [JsonProperty("Zertifizierungsstellen")]
        public string[] CertificationBodies { get; set; }

        [JsonProperty("Zertifikatsseriennummer")]
        public string CertificateSerialNumber { get; set; }

        [JsonProperty("ZertifikatsseriennummerHex")]
        public string CertificateSerialNumberHex { get; set; }

        [JsonProperty("alg")]
        public string Algorithm { get; set; }
    }
}
