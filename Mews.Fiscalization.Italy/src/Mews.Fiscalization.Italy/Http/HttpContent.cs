using System.Text;

namespace Mews.Fiscalization.Italy.Http
{
    public sealed class HttpContent
    {
        public HttpContent(string value, Encoding encoding, string mimeType)
        {
            Value = value;
            MimeType = mimeType;
            Encoding = encoding;
        }

        public string Value { get; }

        public Encoding Encoding { get; }

        public string MimeType { get; }
    }
}
