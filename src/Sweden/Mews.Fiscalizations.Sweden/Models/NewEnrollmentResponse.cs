namespace Mews.Fiscalizations.Sweden.Models;

public sealed class NewEnrollmentResponse(int responseCode, string responseMessage, string applicationId, string requestId, string action, string registerId, string ccuId)
{
    public int ResponseCode { get; } = responseCode;

    public string ResponseMessage { get; } = responseMessage;

    public string ApplicationId{ get; } = applicationId;

    public string RequestId { get; } = requestId;

    public string Action { get; } = action;

    public string RegisterId { get; } = registerId;

    public string Ccuid { get; } = ccuId;
}