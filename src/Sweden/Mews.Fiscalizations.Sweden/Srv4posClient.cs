using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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
    public async Task<Try<CreateActivationResponse, string>> CreateActivation(string username, string password, CreateActivationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        SetAuthorizationHeader($"{username}:{password}");

        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/activations-advanced", request.ToDto(), cancellationToken);
        return await HandleResponse<DTOs.CreateActivationResponse, CreateActivationResponse>(response, r => r.FromDto(), cancellationToken);
    }

    /// <summary>
    /// Request for recording fiscal data to the control unit.
    /// </summary>
    public async Task<Try<SendDataResponse, string>> SendDataToControlUnitAsync(string apiKey, string corporateId, string cashRegisterName, SendDataRequest request, CancellationToken cancellationToken = default)
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
    public async Task<Try<bool, string>> CheckCashRegisterUniquenessAsync(Country country, string corporateId, string cashRegisterName, CancellationToken cancellationToken = default)
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

    // TODO: Not a great way to handle responses and errors, we will improve it later.
    private static async Task<Try<TResult, string>> HandleResponse<TDto, TResult>(HttpResponseMessage response, Func<TDto, TResult> map, CancellationToken cancellationToken)
    {
        var result = await response.IsSuccessStatusCode.MatchAsync(
            async t =>
            {
                var dataResponse = await Try.CatchAsync<TDto, Exception>(
                    async _ => await response.Content.ReadFromJsonAsync<TDto>(cancellationToken)
                );
                return dataResponse.MapError(e => e.Message);
            },
            async f => Try.Error<TDto, string>(await response.Content.ReadAsStringAsync(cancellationToken))
        );

        return result.Map(map);
    }
}