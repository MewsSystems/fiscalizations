using FuncSharp;

namespace Mews.Fiscalizations.Sweden.Models;

public sealed class TransactionResponse(
    int responseCode,
    string responseMessage,
    string responseReason,
    string applicationId,
    string requestId,
    string skvResponseCode,
    string skvResponseMessage,
    string sequenceNumber,
    string controlServerId,
    string controlCode)
{
    public int ResponseCode { get; } = responseCode;

    public string ResponseMessage { get; } = responseMessage;

    public string ResponseReason { get; } = responseReason;

    public string ApplicationId { get; } = applicationId;

    public string RequestId { get; } = requestId;

    public string SKVResponseCode { get; } = skvResponseCode;

    public string SKVResponseMessage { get; } = skvResponseMessage;

    public string SequenceNumber { get; } = sequenceNumber;

    public string ControlServerId { get; } = controlServerId;

    public Option<NonEmptyString> ControlCode { get; } = controlCode.AsNonEmpty();
}