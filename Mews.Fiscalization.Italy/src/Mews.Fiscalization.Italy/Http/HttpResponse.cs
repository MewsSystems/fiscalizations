using System.Collections.Generic;
using System.Net;

namespace Mews.Fiscalization.Italy.Http
{
    public sealed class HttpResponse
    {
        public HttpResponse(HttpStatusCode code, HttpContent content = null, Dictionary<string, string> headers = null)
        {
            Code = code;
            Content = content;
            Headers = headers ?? new Dictionary<string, string>();
        }

        public HttpStatusCode Code { get; }

        public HttpContent Content { get; }

        public Dictionary<string, string> Headers { get; }
    }
}