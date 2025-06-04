namespace Mews.Fiscalizations.Sweden.Models;

public sealed record RegisterStatusResponse(
    string RequestXml,
    string ResponseXml,
    int ResponseCode,
    string ResponseMessage,
    string ResponseReason,
    string ApplicationId,
    string RequestId,
    string SkvResponseCode,
    string SkvResponseMessage
);