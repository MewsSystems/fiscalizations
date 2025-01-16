namespace Mews.Fiscalizations.Sweden.Models;

// TODO: Remove request/response content after testing.
public sealed record SendDataResponse(string ResponseCode, string ControlUnitSerial, string RequestContent, string ResponseContent);