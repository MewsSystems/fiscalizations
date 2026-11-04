namespace Mews.Fiscalizations.Czechia.Model;

public sealed class SendRevenueResult
{
    internal SendRevenueResult(
        Guid messageUuid,
        Try<SendRevenueSuccess, SendRevenueError> outcome,
        IReadOnlyList<SendRevenueWarning> warnings,
        bool isTest,
        Option<string> globalTransactionId,
        string requestXml,
        string responseXml)
    {
        MessageUuid = messageUuid;
        Outcome = outcome;
        Warnings = warnings;
        IsTest = isTest;
        GlobalTransactionId = globalTransactionId;
        RequestXml = requestXml;
        ResponseXml = responseXml;
    }

    /// <summary>
    /// UUID of the sent data message (uuid_zpravy). A new one is generated for every attempt.
    /// </summary>
    public Guid MessageUuid { get; }

    /// <summary>
    /// Either the acknowledgement with the POK code, or the error returned by the tax authority.
    /// </summary>
    public Try<SendRevenueSuccess, SendRevenueError> Outcome { get; }

    public bool IsSuccess => Outcome.IsSuccess;

    public Option<SendRevenueSuccess> Success => Outcome.Success;

    public Option<SendRevenueError> Error => Outcome.Error;

    /// <summary>
    /// Non-critical check failures (Varovani). Returned with acknowledgements and with the verification mode result (error code 0).
    /// </summary>
    public IReadOnlyList<SendRevenueWarning> Warnings { get; }

    /// <summary>
    /// True when the message was processed by a non-production environment (the test attribute). Such POK codes are not valid.
    /// </summary>
    public bool IsTest { get; }

    /// <summary>
    /// Value of the X-Global-Transaction-Id response header, recommended to be logged for troubleshooting with the tax authority.
    /// </summary>
    public Option<string> GlobalTransactionId { get; }

    public string RequestXml { get; }

    public string ResponseXml { get; }
}

public sealed class SendRevenueSuccess
{
    internal SendRevenueSuccess(string confirmationCode, DateTimeOffset received)
    {
        ConfirmationCode = confirmationCode;
        Received = received;
    }

    /// <summary>
    /// Acknowledgement code (POK) to be printed on the receipt, e.g. b3a09b52-7c87-4014-a496-4c7a53cf9125-03.
    /// </summary>
    public string ConfirmationCode { get; }

    /// <summary>
    /// Date and time when the tax authority received the message (dat_prij).
    /// </summary>
    public DateTimeOffset Received { get; }
}

public sealed class SendRevenueError
{
    internal SendRevenueError(int code, string message, Option<DateTimeOffset> rejected)
    {
        Code = code;
        Message = message;
        Rejected = rejected;
    }

    /// <summary>
    /// Error code (kod). Negative codes are temporary technical errors, 0 means a successful check in verification mode.
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Error description in Czech without diacritics.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Date and time of the rejection (dat_odmit).
    /// </summary>
    public Option<DateTimeOffset> Rejected { get; }

    /// <summary>
    /// The tax authority could not process the message for technical reasons and it should be sent again later.
    /// </summary>
    public bool IsTemporary => Code < 0;

    /// <summary>
    /// The message sent in verification mode passed all critical checks.
    /// </summary>
    public bool IsSuccessfulVerification => Code == 0;
}

public sealed class SendRevenueWarning
{
    internal SendRevenueWarning(int code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Warning code (kod_varov), e.g. 1 when the EIČ in the message does not match the certificate.
    /// </summary>
    public int Code { get; }

    public string Message { get; }
}
