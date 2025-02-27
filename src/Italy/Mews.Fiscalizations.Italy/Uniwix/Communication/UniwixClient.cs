using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using Newtonsoft.Json;
using Mews.Fiscalizations.Italy.Uniwix.Communication.Dto;
using Mews.Fiscalizations.Italy.Uniwix.Errors;

namespace Mews.Fiscalizations.Italy.Uniwix.Communication;

public class UniwixClient
{
    private const string UniwixBaseUrl = "https://www.uniwix.com/api/Uniwix";
    private readonly HttpClient _httpClient;

    public UniwixClient(HttpClient httpClient, UniwixClientConfiguration configuration)
    {
        Configuration = configuration;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.ExpectContinue = true;
    }

    private UniwixClientConfiguration Configuration { get; }

    public async Task<Try<SendInvoiceResult, ErrorResult>> SendInvoiceAsync(ElectronicInvoice invoice)
    {
        var url = $"{UniwixBaseUrl}/Invoices/Upload";
        var invoiceFile = new ElectronicInvoiceFile(invoice);
        var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(invoiceFile.Data), "fattura", invoiceFile.FileName }
        };

        var result = await PostAsync<PostInvoiceResponse>(url, content);
        return result.Map(r => new SendInvoiceResult(r.FileId, r.Message));
    }

    public async Task<Try<InvoiceState, ErrorResult>> GetInvoiceStateAsync(string fileId)
    {
        var url = $"{UniwixBaseUrl}/Invoices/{fileId}";
        var result = await GetAsync<List<InvoiceStateResult>>(url);

        var validatedResult = result.Where(a => a is not null && a.NonEmpty(), _ => ErrorResult.Create($"Invoice {fileId} not found.", ErrorType.InvoiceNotFound));
        return validatedResult.Map(r =>
        {
            var state = r.OrderByDescending(s => s.Date).First();
            return new InvoiceState(fileId, GetSdiState(state), state.Message);
        });
    }

    public Task<Try<UniwixUser, ErrorResult>> CreateUserAsync(CreateUserParameters createUserParameters)
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

    public async Task<Try<bool, ErrorResult>> VerifyCredentialsAsync()
    {
        // This endpoint returns the account information, so if the ITry is success it means the credentials are valid.
        var result = await GetAsync<object>($"{UniwixBaseUrl}/Info");
        return result.Map(r => true);
    }

    private Task<Try<TResult, ErrorResult>> GetAsync<TResult>(string url)
    {
        return ExecuteRequestAsync<TResult>(url, HttpMethod.Get, content: null);
    }

    private Task<Try<TResult, ErrorResult>> PostAsync<TResult>(string url, HttpContent content)
    {
        return ExecuteRequestAsync<TResult>(url, HttpMethod.Post, content);
    }

    private Task<Try<TResult, ErrorResult>> ExecuteRequestAsync<TResult>(string url, HttpMethod httpMethod, HttpContent content)
    {
        return ExecuteRequestAsync(url, httpMethod, content, async httpResponse =>
        {
            var json = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<Response<TResult>>(json).Result;
                return Try.Success<TResult, ErrorResult>(result);
            }

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Try.Error<TResult, ErrorResult>(ErrorResult.Create("Uniwix authorization failed.", ErrorType.Unauthorized));
            }

            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var validationErrorResponse = JsonConvert.DeserializeObject<Response<ValidationError>>(json);
                return Try.Error<TResult, ErrorResult>(ErrorResult.Create($"{validationErrorResponse.Code}: {validationErrorResponse.Result.Message}", ErrorType.Validation, validationErrorResponse.Result.Errors));
            }

            var errorResponse = JsonConvert.DeserializeObject<Response<string>>(json);
            return Try.Error<TResult, ErrorResult>(ErrorResult.Create($"{errorResponse.Code}: {errorResponse.Result}", MapErrorType(errorResponse.Code)));
        });
    }

    private ErrorType MapErrorType(int errorCode)
    {
        return errorCode.Match(
            -1, _ => ErrorType.Validation,
            -2, _ => ErrorType.Validation,
            -3, _ => ErrorType.Validation,
            -4, _ => ErrorType.InsufficientCredit,
            -5, _ => ErrorType.Connection,
            -6, _ => ErrorType.InvoiceNotFound,
            -7, _ => ErrorType.InvoiceStatusNotFound,
            -8, _ => ErrorType.Validation,
            -9, _ => ErrorType.Connection,
            -10, _ => ErrorType.FileNotAvailable,
            -11, _ => ErrorType.Validation,
            -12, _ => ErrorType.FileExistsInQueue,
            -13, _ => ErrorType.Unauthorized,
            -15, _ => ErrorType.FileExistsInQueue,
            _ => ErrorType.Unknown
        );
    }

    private async Task<Try<TResult, ErrorResult>> ExecuteRequestAsync<TResult>(
        string url,
        HttpMethod httpMethod,
        HttpContent content,
        Func<HttpResponseMessage, Task<Try<TResult, ErrorResult>>> responseProcessor)
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
                using var httpResponse = await _httpClient.SendAsync(message);
                return await responseProcessor(httpResponse);
            }
            catch (HttpRequestException e)
            {
                return Try.Error<TResult, ErrorResult>(ErrorResult.Create(e.Message, ErrorType.Connection));
            }
            catch (WebException e)
            {
                return Try.Error<TResult, ErrorResult>(ErrorResult.Create(e.Message, ErrorType.Connection));
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