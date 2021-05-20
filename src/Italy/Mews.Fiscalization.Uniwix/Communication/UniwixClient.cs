using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mews.Fiscalization.Italy;
using Mews.Fiscalization.Italy.Dto.Invoice;
using Mews.Fiscalization.Uniwix.Communication.Dto;
using Mews.Fiscalization.Uniwix.Dto;
using Mews.Fiscalization.Uniwix.Errors;
using Newtonsoft.Json;

namespace Mews.Fiscalization.Uniwix.Communication
{
    public class UniwixClient
    {
        private const string UniwixBaseUrl = "https://www.uniwix.com/api/Uniwix";

        static UniwixClient()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.ExpectContinue = true;
        }

        public UniwixClient(UniwixClientConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static HttpClient HttpClient { get;}

        private UniwixClientConfiguration Configuration { get; }

        public async Task<SendInvoiceResult> SendInvoiceAsync(ElectronicInvoice invoice)
        {
            var url = $"{UniwixBaseUrl}/Invoices/Upload";
            var invoiceFile = new ElectronicInvoiceFile(invoice);
            var content = new MultipartFormDataContent
            {
                { new ByteArrayContent(invoiceFile.Data), "fattura", invoiceFile.FileName }
            };

            try
            {
                var result = await PostAsync<PostInvoiceResponse>(url, content).ConfigureAwait(continueOnCapturedContext: false);
                return new SendInvoiceResult(result.FileId, result.Message);
            }
            catch (UniwixException e) when (e.Code == (int) UniwixErrorCodes.ValidationError)
            {
                throw new UniwixValidationException(e.Code, e.Reason, invoiceFile.Content);
            }
        }

        public async Task<InvoiceState> GetInvoiceStateAsync(string fileId)
        {
            var url = $"{UniwixBaseUrl}/Invoices/{fileId}";
            var result = await GetAsync<List<InvoiceStateResult>>(url).ConfigureAwait(continueOnCapturedContext: false);

            if (result.Count == 0)
            {
                throw new InvalidOperationException($"Invoice {fileId} not found.");
            }

            var state = result.OrderByDescending(s => s.Date).First();
            return new InvoiceState(fileId, GetSdiState(state), state.Message);
        }

        public Task<UniwixUser> CreateUserAsync(CreateUserParameters createUserParameters)
        {
            var url = $"{UniwixBaseUrl}/Users";
            var content = new MultipartFormDataContent
            {
                { new StringContent(createUserParameters.UserName), "username" },
                { new StringContent(createUserParameters.TaxIdentificationNumber), "piva" },
                { new StringContent(createUserParameters.Description), "descrizione" },
            };

            return PostAsync<UniwixUser>(url, content);
        }

        public Task<bool> VerifyCredentialsAsync()
        {
            var url = $"{UniwixBaseUrl}/Info";
            return ExecuteRequestAsync(url, HttpMethod.Get, content: null, responseProcessor: r => r.IsSuccessStatusCode);
        }

        private async Task<TResult> GetAsync<TResult>(string url)
        {
            return await ExecuteRequestAsync<TResult>(url, HttpMethod.Get, content: null).ConfigureAwait(continueOnCapturedContext: false);
        }

        private async Task<TResult> PostAsync<TResult>(string url, HttpContent content)
        {
            return await ExecuteRequestAsync<TResult>(url, HttpMethod.Post, content).ConfigureAwait(continueOnCapturedContext: false);
        }

        private async Task<TResult> ExecuteRequestAsync<TResult>(string url, HttpMethod httpMethod, HttpContent content)
        {
            var response = ExecuteRequestAsync(url, httpMethod, content, async httpResponse =>
            {
                var json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Response<TResult>>(json).Result;
                }

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UniwixAuthorizationException();
                }

                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    var validationErrorResponse = JsonConvert.DeserializeObject<Response<ValidationError>>(json);
                    throw new UniwixException(validationErrorResponse.Code, validationErrorResponse.Result.Message);
                }

                var errorResponse = JsonConvert.DeserializeObject<Response<string>>(json);
                throw new UniwixException(errorResponse.Code, errorResponse.Result);
            });

            return await (await response.ConfigureAwait(continueOnCapturedContext: false)).ConfigureAwait(continueOnCapturedContext: false);
        }

        private async Task<TResult> ExecuteRequestAsync<TResult>(string url, HttpMethod httpMethod, HttpContent content, Func<HttpResponseMessage, TResult> responseProcessor)
        {
            var credentials = $"{Configuration.Key}:{Configuration.Password}";
            var authenticationHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));

            using (var message = new HttpRequestMessage())
            {
                message.RequestUri = new Uri(url);
                message.Content = content;
                message.Method = httpMethod;
                message.Headers.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);

                try
                {
                    using (var httpResponse = await HttpClient.SendAsync(message).ConfigureAwait(continueOnCapturedContext: false))
                    {
                        return responseProcessor(httpResponse);
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new UniwixConnectionException(e);
                }
                catch (WebException e)
                {
                    throw new UniwixConnectionException(e);
                }
            }
        }

        private SdiState GetSdiState(InvoiceStateResult invoiceState)
        {
            if (invoiceState.State == null || invoiceState.State == UniwixProcesingState.Waiting)
            {
                return SdiState.Pending;
            }
            if (invoiceState.State == UniwixProcesingState.Accepted)
            {
                return SdiState.AcceptedByClient;
            }
            if (invoiceState.State == UniwixProcesingState.Rejected)
            {
                return SdiState.RejectedByClient;
            }

            var sdiStateMapping = new Dictionary<string, SdiState>
            {
                ["RC"] = SdiState.Delivered,
                ["MC"] = SdiState.DeliveryFailed,
                ["NS"] = SdiState.RejectedBySdi,
                ["DT"] = SdiState.DeadlinePassed,
                ["NE"] = SdiState.Processed
            };

            if (sdiStateMapping.TryGetValue(invoiceState.SdiState, out SdiState sdiState))
            {
                return sdiState;
            }

            throw new InvalidOperationException("Unknown invoice status.");
        }
    }
}