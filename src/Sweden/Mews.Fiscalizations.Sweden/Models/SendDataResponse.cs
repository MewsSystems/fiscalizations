namespace Mews.Fiscalizations.Sweden.Models;

public sealed record SendDataResponse(string ResponseCode, string ControlUnitSerial, string RequestContent, string ResponseContent);