using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden;

public sealed class Srv4posClient()
{
    private const string BaseUrl = "https://s.srv4pos.com/v45";

    static Srv4posClient()
    {
        HttpClient = new HttpClient();
    }

    private static HttpClient HttpClient { get; }

    private static void SetAuthorizationHeader(string token)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(token)));
    }

    /// <summary>
    /// A request to create an activation that is immediately ready for use, you only need to deliver the API key to a new cash register.
    /// </summary>
    public async Task<Try<CreateActivationResponse, ErrorResponse>> CreateActivation(string username, string password, CreateActivationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        SetAuthorizationHeader($"{username}:{password}");

        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/activations-advanced", request.ToDto(), cancellationToken);
        return await HandleResponse<DTOs.CreateActivationResponse, CreateActivationResponse>(response, r => r.FromDto(), cancellationToken);
    }

    /// <summary>
    /// Request for recording fiscal data to the control unit.
    /// </summary>
    public async Task<Try<SendDataResponse, ErrorResponse>> SendDataToControlUnitAsync(string apiKey, string corporateId, string cashRegisterName, SendDataRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        SetAuthorizationHeader(apiKey);

        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/{corporateId}/registration/{cashRegisterName}/kd", request.ToDto(), cancellationToken);

        // TODO: returning the request and response content is needed for getting certified, we will remove it later.
        var requestContent = await response.RequestMessage.Content.ReadAsStringAsync(cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return await HandleResponse<DTOs.SendDataResponse, SendDataResponse>(response, r => r.FromDto(requestContent, responseContent), cancellationToken);
    }

    /// <summary>
    /// Checks if a cash register name is unique within the country and corporate.
    /// </summary>
    public async Task<Try<bool, ErrorResponse>> CheckCashRegisterUniquenessAsync(Country country, string corporateId, string cashRegisterName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new DTOs.CheckCashRegisterUniquenessRequest
        {
            CountryCode = country.Alpha2Code,
            CorporateId = corporateId,
            CashRegisterName = cashRegisterName
        };
        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/noauth/activations/cash-register-name/existance", request, cancellationToken);
        return await HandleResponse<DTOs.CheckCashRegisterUniquenessResponse, bool>(response, r => r.Exists, cancellationToken);
    }

    private static async Task<Try<TResult, ErrorResponse>> HandleResponse<TDto, TResult>(HttpResponseMessage response, Func<TDto, TResult> map, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return Try.Success<TResult, ErrorResponse>(map(await response.Content.ReadFromJsonAsync<TDto>(cancellationToken)));
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        return (int)response.StatusCode switch
        {
            >= 500 => Try.Error<TResult, ErrorResponse>(new ErrorResponse(NonEmptyString.CreateUnsafe("Server side error, try again."), Srv4posErrorType.ServerSideError, responseContent)),
            >= 400 => HandleErrorResponse<TResult>(responseContent),
            _ => Try.Error<TResult, ErrorResponse>(new ErrorResponse(NonEmptyString.CreateUnsafe("Unknown error"), Srv4posErrorType.UnknownError, responseContent))
        };
    }

    private static Try<TResult, ErrorResponse> HandleErrorResponse<TResult>(string responseContent)
    {
        var errorResponse = JsonSerializer.Deserialize<DTOs.Srv4posBasicErrorResponse>(responseContent);
        if (errorResponse.Error == "NonUniqueJsonException")
        {
            return Try.Error<TResult, ErrorResponse>(new ErrorResponse(NonEmptyString.CreateUnsafe("Non-unique cash register name or corporate id."), Srv4posErrorType.NonUniqueCashRegisterNameOrCorporateId, responseContent));
        }
        if (errorResponse.Error == "ValueNotValidJsonException")
        {
            var srv4PosError = JsonSerializer.Deserialize<DTOs.Srv4posError>(responseContent);
            if (srv4PosError.Details?.Count > 0)
            {
                var errorType = srv4PosError.Details[0].Field switch
                {
                    "corporateId" => Srv4posErrorType.InvalidCorporateId,
                    "cashRegisterName" => Srv4posErrorType.InvalidCashRegisterName,
                    _ => Srv4posErrorType.InvalidFieldFormat
                };
                return Try.Error<TResult, ErrorResponse>(new ErrorResponse(NonEmptyString.CreateUnsafe("Invalid field format"), errorType, responseContent));
            }
            return Try.Error<TResult, ErrorResponse>(new ErrorResponse(NonEmptyString.CreateUnsafe("Invalid request."), Srv4posErrorType.InvalidRequest, responseContent));
        }
        return Try.Error<TResult, ErrorResponse>(new ErrorResponse(NonEmptyString.CreateUnsafe("Unknown error"), Srv4posErrorType.UnknownError, responseContent));
    }
}