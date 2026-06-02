using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashRegisters;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashRegisters;

namespace Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.CashRegisters;

internal static class CashRegisterMapper
{
    public static UpsertCashRegisterRequest MapUpsertRequest(CashRegister cashRegister)
    {
        return new UpsertCashRegisterRequest
        {
            CashRegisterType = new CashRegisterTypeRequest
            {
                Type = MapCashRegisterType(cashRegister.Type),
                TssId = cashRegister.TssId
            },
            Brand = cashRegister.Brand,
            Model = cashRegister.Model,
            Software = new CashRegisterSoftware
            {
                Brand = cashRegister.SoftwareBrand,
                Version = cashRegister.SoftwareVersion
            },
            BaseCurrencyCode = cashRegister.BaseCurrencyCode
        };
    }

    public static CashRegister MapResponse(this CashRegisterResponse response)
    {
        return new CashRegister(
            ClientId: response.ClientId,
            Type: MapCashRegisterTypeResponse(response.CashRegisterType),
            TssId: response.TssId,
            SerialNumber: response.SerialNumber,
            Brand: response.Brand,
            Model: response.Model,
            SoftwareBrand: response.Software.Brand,
            SoftwareVersion: response.Software.Version,
            BaseCurrencyCode: response.BaseCurrencyCode
        );
    }

    private static string MapCashRegisterType(CashRegisterType type)
    {
        return type switch
        {
            CashRegisterType.Master => "MASTER",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static CashRegisterType MapCashRegisterTypeResponse(string type)
    {
        return type switch
        {
            "MASTER" => CashRegisterType.Master,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
