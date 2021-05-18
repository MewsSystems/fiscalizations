using Mews.Fiscalization.Greece.Dto.Xsd;
using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Result;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Greece
{
    internal class RestClient
    {
        private static readonly Uri ProductionBaseUri = new Uri("https://mydatapi.aade.gr/myDATA");
        private static readonly Uri SandboxBaseUri = new Uri("https://mydata-dev.azure-api.net");
        private static readonly string SendInvoicesEndpointMethodName = "SendInvoices";
        private static readonly string GetRequestDocsEndpointMethodName = "RequestDocs";
        private static readonly string UserIdHeaderName = "aade-user-id";
        private static readonly string SubscriptionKeyHeaderName = "Ocp-Apim-Subscription-Key";
        private static readonly string XmlMediaType = "application/xml";

        public string UserId { get; }

        public string SubscriptionKey { get; }

        public AadeLogger Logger { get; }

        public Uri BaseUri { get; }

        private HttpClient HttpClient { get; }

        internal RestClient(string userId, string subscriptionKey, AadeEnvironment environment, AadeLogger logger = null)
        {
            UserId = userId ?? throw new ArgumentNullException(userId);
            SubscriptionKey = subscriptionKey ?? throw new ArgumentNullException(subscriptionKey);

            BaseUri = environment == AadeEnvironment.Production
                ? ProductionBaseUri
                : SandboxBaseUri;

            Logger = logger;

            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add(UserIdHeaderName, $"{UserId}");
            HttpClient.DefaultRequestHeaders.Add(SubscriptionKeyHeaderName, $"{SubscriptionKey}");
        }

        internal Task<ResponseDoc> SendRequestAsync(InvoicesDoc invoicesDoc)
        {
            var requestContent = XmlManipulator.Serialize(invoicesDoc).DocumentElement.OuterXml;
            Logger?.Debug("Created XML document from DTOs.", new { XmlString = requestContent });

            var requestMessage = BuildHttpPostMessage(requestContent);
            
            return SendAsync(
                sendFunc:
                    () => HttpClient.SendAsync(requestMessage),
                buildErrorResponseFunc:
                    (errorCode, errorMessage) => BuildResponseDocWithErrors(errorCode, errorMessage, invoicesDoc.Invoices),
                buildResponseFunc:
                    async httpResponseMessage =>
                    {
                        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                        if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            return BuildResponseDocWithErrors(SendInvoiceErrorCodes.UnauthorizedErrorCode, "Authorization error", invoicesDoc.Invoices);
                        }
                        if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Forbidden && responseContent.Contains("This web app is stopped"))
                        {
                            return BuildResponseDocWithErrors(SendInvoiceErrorCodes.TimeoutErrorCode, "Timeout", invoicesDoc.Invoices);
                        }

                        var responseDoc = XmlManipulator.Deserialize<ResponseDoc>(responseContent);
                        Logger?.Debug("Result received and successfully deserialized.", responseDoc);

                        return responseDoc;
                    }
                );
        }

        internal Task<CheckUserCredentialsResult> CheckUserCredentialsAsync()
        {
            var queryString = new NameValueCollection();
            queryString.Add("mark", "mark");

            var endpointUri = new Uri(BaseUri, $"{GetRequestDocsEndpointMethodName}?{queryString}");

            return SendAsync(
                sendFunc: 
                    () => HttpClient.GetAsync(endpointUri),
                buildErrorResponseFunc:
                    (errorCode, errorMessage) => BuildCheckUserCredentialsResponseWithError(errorCode, errorMessage),
                buildResponseFunc:
                    httpResponseMessage => Task.FromResult(new CheckUserCredentialsResult(httpResponseMessage.StatusCode != System.Net.HttpStatusCode.Unauthorized))
                );
        }

        private async Task<TResult> SendAsync<TResult>(
            Func<Task<HttpResponseMessage>> sendFunc,
            Func<string, string, TResult> buildErrorResponseFunc,
            Func<HttpResponseMessage, Task<TResult>> buildResponseFunc)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpResponseMessage response;

            try
            {
                response = await sendFunc().ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (TaskCanceledException ex)
            {
                stopwatch.Stop();
                Logger?.Info($"HTTP request timeout after {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = stopwatch.ElapsedMilliseconds });

                return buildErrorResponseFunc(SendInvoiceErrorCodes.TimeoutErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.Info($"HTTP request failed after {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = stopwatch.ElapsedMilliseconds });

                return buildErrorResponseFunc(SendInvoiceErrorCodes.InternalServerErrorCode, ex.Message);
            }

            stopwatch.Stop();
            Logger?.Info($"HTTP request finished in {stopwatch.ElapsedMilliseconds}ms.", new { HttpRequestDuration = stopwatch.ElapsedMilliseconds });

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return buildErrorResponseFunc(SendInvoiceErrorCodes.InternalServerErrorCode, "Internal server error");
            }

            return await buildResponseFunc(response);
        }

        private HttpRequestMessage BuildHttpPostMessage(string messageContent)
        {
            var endpointUri = new Uri(BaseUri, SendInvoicesEndpointMethodName);
            var message = new HttpRequestMessage(HttpMethod.Post, endpointUri);

            message.Content = new StringContent(content: messageContent, encoding: Encoding.UTF8, mediaType: XmlMediaType);

            return message;
        }
        
        private CheckUserCredentialsResult BuildCheckUserCredentialsResponseWithError(string errorCode, string errorMessage)
        {
            return new CheckUserCredentialsResult(error: new ResultError(errorCode, errorMessage));
        }

        private ResponseDoc BuildResponseDocWithErrors(string errorCode, string errorMessage, Dto.Xsd.Invoice[] invoices)
        {
            return new ResponseDoc
            {
                Responses = invoices.Select((_, index) => new Response
                {
                    Index = index + 1,
                    Errors = new[]
                    {
                        new Error
                        {
                            Code = errorCode,
                            Message = errorMessage
                        }
                    }
                }).ToArray()
            };
        }
    }
}
