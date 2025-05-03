using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Client;
using Mews.Fiscalizations.Fiskaly.Models.SignES.ClientDevices;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.ClientDevices;

internal static class ClientDeviceMapper
{
    public static ClientDevice MapClientDeviceResponse(this ClientResponse response)
    {
        return new ClientDevice(
            ClientId: response.ClientData.Id,
            SignerId: response.ClientData.Signer.Id
        );
    }
}