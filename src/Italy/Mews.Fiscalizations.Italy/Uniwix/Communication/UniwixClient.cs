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
        => ExecuteRequestAsync<TResult>(url, HttpMethod.Get, content: null, CancellationToken.None);

    private Task<Try<TResult, ErrorResult>> PostAsync<TResult>(string url, HttpContent content)
        => ExecuteRequestAsync<TResult>(url, HttpMethod.Post, content, CancellationToken.None);

    private async Task<Try<TResult, ErrorResult>> ExecuteRequestAsync<TResult>(string url, HttpMethod httpMethod, HttpContent content, CancellationToken cancellationToken)
    {
        var response = await SendRequestAsync(url, httpMethod, content, cancellationToken);
        return response.FlatMap(ProcessResponse<TResult>);
    }

    private async Task<Try<UniwixResponse, ErrorResult>> SendRequestAsync(string url, HttpMethod httpMethod, HttpContent content, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(url, httpMethod, content);

        try
        {
            using var response = await _httpClient.SendAsync(request, cancellationToken);
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return Try.Success<UniwixResponse, ErrorResult>(new UniwixResponse(response.StatusCode, response.Content.Headers.ContentType?.MediaType, body));
        }
        catch (HttpRequestException)
        {
            return Try.Error<UniwixResponse, ErrorResult>(ErrorResult.Create("Request to Uniwix failed.", ErrorType.Connection));
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return Try.Error<UniwixResponse, ErrorResult>(ErrorResult.Create("Request to Uniwix timed out.", ErrorType.Connection));
        }
    }

    private HttpRequestMessage CreateRequest(string url, HttpMethod httpMethod, HttpContent content)
    {
        var credentials = $"{Configuration.Key}:{Configuration.Password}";
        var authenticationHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
        var request = new HttpRequestMessage(httpMethod, url)
        {
            Content = content
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authenticationHeaderValue);
        return request;
    }

    private Try<TResult, ErrorResult> ProcessResponse<TResult>(UniwixResponse response)
        => response.IsSuccessStatusCode
            ? ProcessSuccessResponse<TResult>(response)
            : ProcessErrorResponse<TResult>(response);

    private static Try<TResult, ErrorResult> ProcessSuccessResponse<TResult>(UniwixResponse response)
    {
        var deserializedResponse = DeserializeResponse<TResult>(response, "Uniwix returned a non-JSON success response.", "Uniwix returned an empty success response.");
        return deserializedResponse.FlatMap(successResponse =>
            successResponse.Result is null
                ? CreateMalformedResponseError<TResult>(response, "Uniwix returned an empty success response.")
                : Try.Success<TResult, ErrorResult>(successResponse.Result));
    }

    private Try<TResult, ErrorResult> ProcessErrorResponse<TResult>(UniwixResponse response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Try.Error<TResult, ErrorResult>(ErrorResult.Create("Uniwix authorization failed.", ErrorType.Unauthorized));
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var deserializedResponse = DeserializeResponse<ValidationError>(response, "Uniwix returned a non-JSON error response.");
            return deserializedResponse.FlatMap(validationErrorResponse =>
                validationErrorResponse.Result is null
                    ? CreateMalformedResponseError<TResult>(response, "Uniwix returned an empty error response.")
                    : Try.Error<TResult, ErrorResult>(ErrorResult.Create($"{validationErrorResponse.Code}: {validationErrorResponse.Result.Message}", ErrorType.Validation, validationErrorResponse.Result.Errors)));
        }

        var errorResponse = DeserializeResponse<string>(response, "Uniwix returned a non-JSON error response.");
        return errorResponse.FlatMap(deserializedErrorResponse =>
            Try.Error<TResult, ErrorResult>(ErrorResult.Create($"{deserializedErrorResponse.Code}: {deserializedErrorResponse.Result}", MapErrorType(deserializedErrorResponse.Code))));
    }

    private static Try<Response<TResult>, ErrorResult> DeserializeResponse<TResult>(UniwixResponse response, string malformedResponseMessage, string emptyResponseMessage = "Uniwix returned an empty error response.")
    {
        try
        {
            var deserializedResponse = JsonConvert.DeserializeObject<Response<TResult>>(response.Body);
            return deserializedResponse is null
                ? CreateMalformedResponseError<Response<TResult>>(response, emptyResponseMessage)
                : Try.Success<Response<TResult>, ErrorResult>(deserializedResponse);
        }
        catch (JsonException)
        {
            return CreateMalformedResponseError<Response<TResult>>(response, malformedResponseMessage);
        }
    }

    private static Try<TResult, ErrorResult> CreateMalformedResponseError<TResult>(UniwixResponse response, string message)
    {
        var contentType = response.ContentType ?? "unknown";
        return Try.Error<TResult, ErrorResult>(ErrorResult.Create($"{message} Status: {(int)response.StatusCode}. Content-Type: {contentType}. Body length: {response.Body.Length}.", ErrorType.Unknown));
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

    private sealed record UniwixResponse(HttpStatusCode StatusCode, string ContentType, string Body)
    {
        public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;
    }
}
