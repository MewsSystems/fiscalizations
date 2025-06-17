namespace Mews.Fiscalizations.Sweden.Models;

public sealed class StatusEnrollmentResponse(
    string requestXml,
    int responseCode,
    string responseMessage,
    string responseReason,
    string action,
    string tcsId,
    int active,
    int loginCount,
    string lastLogin)
{
    public string RequestXml { get; } = requestXml;

    public int ResponseCode { get; } = responseCode;

    public string ResponseReason { get; } = responseReason;

    public string ResponseMessage { get; } = responseMessage;

    public string Action { get; } = action;

    public string TcsId { get; } = tcsId;

    public int Active { get; } = active;

    public int LoginCount { get; } = loginCount;

    public string LastLogin { get; } = lastLogin;
}