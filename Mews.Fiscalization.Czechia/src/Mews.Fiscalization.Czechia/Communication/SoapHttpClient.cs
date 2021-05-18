using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mews.Eet.Events;

namespace Mews.Eet.Communication
{
    public class SoapHttpClient
    {
        public SoapHttpClient(Uri endpointUri, TimeSpan timeout, EetLogger logger)
        {
            EndpointUri = endpointUri;
            HttpClient = new HttpClient() { Timeout = timeout };
            Logger = logger;
        }

        public event EventHandler<HttpRequestFinishedEventArgs> HttpRequestFinished;

        private Uri EndpointUri { get; }

        private HttpClient HttpClient { get; }

        private EetLogger Logger { get; }

        public async Task<string> SendAsync(string body, string operation)
        {
            HttpClient.DefaultRequestHeaders.Add("SOAPAction", operation);

            var requestContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            Logger?.Debug("Starting HTTP request.", new { HttpRequestBody = body });

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var response = await HttpClient.PostAsync(EndpointUri, requestContent).ConfigureAwait(continueOnCapturedContext: false))
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                stopwatch.Stop();
                var duration = stopwatch.ElapsedMilliseconds;
                Logger?.Info($"HTTP request finished in {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = duration });
                HttpRequestFinished?.Invoke(this, new HttpRequestFinishedEventArgs(duration));

                return result;
            }
        }
    }
}