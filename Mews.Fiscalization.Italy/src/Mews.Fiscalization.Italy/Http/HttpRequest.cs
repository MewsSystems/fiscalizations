using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Italy.Http
{
    public sealed class HttpRequest
    {
        public HttpRequest(Uri uri, HttpMethod method, HttpContent content, Dictionary<string, string> headers = null)
        {
            Uri = uri;
            Method = method;
            Content = content;
            Headers = headers ?? new Dictionary<string, string>();
        }

        public Uri Uri { get; }

        public HttpMethod Method { get; }

        public HttpContent Content { get; }

        public Dictionary<string, string> Headers { get; }
    }
}
