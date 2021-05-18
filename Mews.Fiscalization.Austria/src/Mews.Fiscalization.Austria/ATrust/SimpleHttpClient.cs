using System.IO;
using System.Net;
using System.Text;

namespace Mews.Fiscalization.Austria.ATrust
{
    public class SimpleHttpClient
    {
        public SimpleHttpClient(EndpointUrl endpointUrl)
        {
            EndpointUrl = endpointUrl;
        }

        public EndpointUrl EndpointUrl { get; }

        public string PostJson(string operation, string requestBody)
        {
            var request = CreateJsonPostRequest($"{EndpointUrl.Value}/{operation}", requestBody);
            return GetResponseBody(request);
        }

        public string GetJson(string operation)
        {
            var request = CreateGetRequest($"{EndpointUrl.Value}/{operation}");
            return GetResponseBody(request);
        }

        private HttpWebRequest CreateRequest(string method, string url)
        {
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method;
            return webRequest;
        }

        private string GetResponseBody(HttpWebRequest request)
        {
            var response = request.GetResponse() as HttpWebResponse;
            var reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            return reader.ReadToEnd();
        }

        private HttpWebRequest CreateGetRequest(string url)
        {
            return CreateRequest("GET", url);
        }

        private HttpWebRequest CreateJsonPostRequest(string url, string requestBody)
        {
            var data = Encoding.UTF8.GetBytes(requestBody);

            var webRequest = CreateRequest("POST", url);
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = data.Length;
            webRequest.GetRequestStream().Write(data, 0, data.Length);

            return webRequest;
        }
    }
}
