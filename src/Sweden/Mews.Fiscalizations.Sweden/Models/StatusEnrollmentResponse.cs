namespace Mews.Fiscalizations.Sweden.Models;

public sealed class StatusEnrollmentResponse(int responseCode, string responseMessage, string action, string ccuId, int active, int loginCount, string lastLogin)
{
    public int ResponseCode { get; } = responseCode;
    public string ResponseMessage { get; } = responseMessage;
    public string Action { get; } = action;
    public string CcuId { get; } = ccuId;
    public int Active { get; } = active;
    public int LoginCount { get; } = loginCount;
    public string LastLogin { get; } = lastLogin;
}