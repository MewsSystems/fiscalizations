using Mews.Fiscalization.Austria.Dto;
using Newtonsoft.Json;

namespace Mews.Fiscalization.Austria.ATrust
{
    public sealed class ATrustSignerInput
    {

        public ATrustSignerInput(string password, string dataToBeSigned)
        {
            Password = password;
            DataToBeSigned = dataToBeSigned;
        }

        public ATrustSignerInput(string password, QrData qrData)
            : this(password, qrData.ToString())
        {
            QrData = qrData;
        }

        [JsonProperty("password")]
        public string Password { get; }

        [JsonProperty("jws_payload")]
        public string DataToBeSigned { get; }

        [JsonIgnore]
        public QrData QrData { get; }
    }
}
