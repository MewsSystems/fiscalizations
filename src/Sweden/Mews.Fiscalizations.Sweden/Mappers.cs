using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden;

internal static class Mappers
{
    public static DTOs.SendDataRequest ToDto(this SendDataRequest request)
    {
        return new DTOs.SendDataRequest
        {
            UnixTimestamp = ((DateTimeOffset)request.SaleDate).ToUnixTimeMilliseconds(),
            GrossAmount = request.GrossAmount,
            PrintType = (DTOs.PrintType)request.PrintType,
            ReceiptNumber = request.ReceiptNumber.ToNullable(),
            VatRateToSum = request.VatRateToSum,
            IsRefund = request.IsRefund
        };
    }

    public static SendDataResponse FromDto(this DTOs.SendDataResponse response, string requestContent, string responseContent)
    {
        return new SendDataResponse(response.Response, response.ControlUnitSerial, requestContent, responseContent);
    }

    public static DTOs.CreateActivationRequest ToDto(this CreateActivationRequest request)
    {
        return new DTOs.CreateActivationRequest
        {
            ApplicationPackage = request.ApplicationPackage,
            InstallationCreationInfo = request.InstallationCreationInfo.GetOrNull(i => i.ToDto()),
            Address = request.Address.ToDto(),
            Features = request.Features.ToArray(),
            ContactInfo = request.ContactInfo.GetOrNull(i => i.ToDto()),
            CorporateId = request.CorporateId,
            CountryCode = request.Country.Alpha2Code,
            ProductionNumber = request.ProductionNumber.GetOrNull(),
            CashRegisterName = request.CashRegisterName,
            ControlUnitSerial = request.ControlUnitSerial,
            ControlUnitLocation = request.ControlUnitLocation,
            ValidFromUnix = request.ValidFrom.ToNullable(f => ((DateTimeOffset)f).ToUnixTimeMilliseconds()),
            ValidToUnix = request.ValidTo.ToNullable(f => ((DateTimeOffset)f).ToUnixTimeMilliseconds()),
            ApplicationNameAndVersion = request.ApplicationNameAndVersion
        };
    }

    public static CreateActivationResponse FromDto(this DTOs.CreateActivationResponse response)
    {
        return new CreateActivationResponse(
            apiKey: response.ApiKey,
            productionNumber: response.ProductionNumber.AsNonEmpty().GetOrNull(),
            activationId: response.ActivationId,
            address: new Address(response.Address, response.City, response.Zip, response.CompanyName),
            phone: response.Phone.AsNonEmpty().GetOrNull()
        );
    }

    private static DTOs.Address ToDto(this Address address)
    {
        return new DTOs.Address
        {
            AddressLines = address.AddressLines,
            City = address.City,
            PostalCode = address.PostalCode,
            CompanyName = address.CompanyName
        };
    }

    private static DTOs.InstallationCreationInfo ToDto(this InstallationCreationInfo info)
    {
        return new DTOs.InstallationCreationInfo
        {
            BuildInfo = info.BuildInfo,
            DeviceId = info.DeviceId,
            ProgramVersion = info.ProgramVersion.GetOrNull()
        };
    }

    private static DTOs.ContactInfo ToDto(this ContactInfo info)
    {
        return new DTOs.ContactInfo
        {
            Email = info.Email,
            Name = info.Name,
            Phone = info.Phone
        };
    }
}