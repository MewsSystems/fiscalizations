using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FuncSharp;
using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK;
using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.Auth;
using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings;
using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashRegisters;
using Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.Auth;
using Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.CashPointClosings;
using Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.CashRegisters;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashRegisters;

namespace Mews.Fiscalizations.Fiskaly.APIClients;

public class DsfinvkApiClient(HttpClient httpClient, string apiKey, string apiSecret)
{
    // DSFinV-K exposes a single endpoint; the environment (test vs. production) is encoded in the API key itself
    // and surfaced back on the access token claims.
    private readonly Uri _baseUri = new("https://dsfinvk.fiskaly.com");

    private const string RelativeApiUrl = "api/v1/";
    private const string AuthEndpoint = "auth";
    private const string CashRegistersEndpoint = "cash_registers";
    private const string CashPointClosingsEndpoint = "cash_point_closings";

    private const string AuthSchema = "Bearer";
    private const string JsonContentType = "application/json";

    public async Task<ResponseResult<AccessToken>> GetAccessTokenAsync(
        CancellationToken cancellationToken = default
    )
    {
        var request = new AuthorizationTokenRequest { ApiKey = apiKey, ApiSecret = apiSecret };

        return await ProcessRequestAsync<AuthorizationTokenResponse, AccessToken>(
            method: HttpMethod.Post,
            endpoint: AuthEndpoint,
            request: new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                JsonContentType
            ),
            successFunc: r => r.MapAuthResponse(),
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<CashRegister>> UpsertCashRegisterAsync(
        AccessToken token,
        Guid clientId,
        CashRegister cashRegister,
        CancellationToken cancellationToken = default
    )
    {
        var requestBody = new StringContent(
            JsonSerializer.Serialize(
                CashRegisterMapper.MapUpsertRequest(cashRegister),
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }
            ),
            Encoding.UTF8,
            JsonContentType
        );

        return await ProcessRequestAsync<CashRegisterResponse, CashRegister>(
            method: HttpMethod.Put,
            endpoint: $"{CashRegistersEndpoint}/{clientId}",
            request: requestBody,
            successFunc: r => r.MapResponse(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<CashRegister>> GetCashRegisterAsync(
        AccessToken token,
        Guid clientId,
        CancellationToken cancellationToken = default
    )
    {
        return await ProcessRequestAsync<CashRegisterResponse, CashRegister>(
            method: HttpMethod.Get,
            endpoint: $"{CashRegistersEndpoint}/{clientId}",
            request: null,
            successFunc: r => r.MapResponse(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<CashPointClosingResult>> InsertCashPointClosingAsync(
        AccessToken token,
        CashPointClosing closing,
        CancellationToken cancellationToken = default
    )
    {
        var requestBody = new StringContent(
            JsonSerializer.Serialize(
                CashPointClosingMapper.MapInsertRequest(closing),
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }
            ),
            Encoding.UTF8,
            JsonContentType
        );

        return await ProcessRequestAsync<CashPointClosingResponse, CashPointClosingResult>(
            method: HttpMethod.Put,
            endpoint: $"{CashPointClosingsEndpoint}/{closing.ClosingId}",
            request: requestBody,
            successFunc: r => r.MapResponse(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<CashPointClosingResult>> GetCashPointClosingAsync(
        AccessToken token,
        Guid closingId,
        CancellationToken cancellationToken = default
    )
    {
        return await ProcessRequestAsync<CashPointClosingResponse, CashPointClosingResult>(
            method: HttpMethod.Get,
            endpoint: $"{CashPointClosingsEndpoint}/{closingId}",
            request: null,
            successFunc: r => r.MapResponse(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    // Delete returns no body; the Nothing result type signals "success, no payload".
    public async Task<ResponseResult<Nothing>> DeleteCashPointClosingAsync(
        AccessToken token,
        Guid closingId,
        CancellationToken cancellationToken = default
    )
    {
        return await ProcessRequestAsync<CashPointClosingResponse, Nothing>(
            method: HttpMethod.Delete,
            endpoint: $"{CashPointClosingsEndpoint}/{closingId}",
            request: null,
            successFunc: _ => null,
            token: token,
            cancellationToken: cancellationToken
        );
    }

    private async Task<ResponseResult<TResult>> ProcessRequestAsync<TDto, TResult>(
        HttpMethod method,
        string endpoint,
        StringContent request,
        Func<TDto, TResult> successFunc,
        CancellationToken cancellationToken,
        AccessToken token = null
    )
        where TDto : class
        where TResult : class
    {
        var uri = new Uri(_baseUri, $"{RelativeApiUrl}{endpoint}");
        using var requestMessage = new HttpRequestMessage(method, uri);

        if (method != HttpMethod.Get && method != HttpMethod.Delete)
        {
            requestMessage.Content = request;
        }

        if (token is not null)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                AuthSchema,
                token.Value
            );
        }

        using var httpResponse = await httpClient.SendAsync(requestMessage, cancellationToken);
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (httpResponse.IsSuccessStatusCode)
        {
            var dto = string.IsNullOrWhiteSpace(content)
                ? null
                : JsonSerializer.Deserialize<TDto>(content);
            return new ResponseResult<TResult>(successResult: successFunc(dto));
        }

        var error = string.IsNullOrWhiteSpace(content)
            ? null
            : JsonSerializer.Deserialize<DsfinvkErrorResponse>(content);
        return new ResponseResult<TResult>(
            errorResult: new ErrorResult(
                Status: error?.StatusCode ?? (int)httpResponse.StatusCode,
                Code: error?.Code,
                Error: error?.Error,
                Message: error?.Message
            )
        );
    }
}
