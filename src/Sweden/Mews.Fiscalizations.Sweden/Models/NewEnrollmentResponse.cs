namespace Mews.Fiscalizations.Sweden.Models;

public sealed class NewEnrollmentResponse(
    string requestXml,
    int responseCode,
    string responseMessage,
    string responseReason,
    string applicationId,
    string requestId,
    string action,
    string registerId,
    string ccuId)
{
    public string RequestXml { get; } = requestXml;

    public int ResponseCode { get; } = responseCode;

    public string ResponseMessage { get; } = responseMessage;

    public string ResponseReason => responseReason;

    public string ApplicationId{ get; } = applicationId;

    public string RequestId { get; } = requestId;

    public string Action { get; } = action;

    public string RegisterId { get; } = registerId;

    public string Ccuid { get; } = ccuId;
}