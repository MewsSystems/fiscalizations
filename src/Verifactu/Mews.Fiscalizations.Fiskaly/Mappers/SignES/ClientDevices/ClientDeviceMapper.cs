using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.ClientDevices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.ClientDevices;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.ClientDevices;

internal static class ClientDeviceMapper
{
    public static ClientDevice MapClientDeviceResponse(this ContentWrapper<ClientResponse> response)
    {
        return new ClientDevice(
            ClientId: response.Content.Id,
            SignerId: response.Content.Signer.Id
        );
    }
}