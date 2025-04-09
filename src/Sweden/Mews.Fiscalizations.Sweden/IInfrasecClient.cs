using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden;

public interface IInfrasecClient
{
    /// <summary>
    /// Checks the status of a ccu. Request and responses use xml.
    /// </summary>
    /// <param name="data">Data related to the cash register</param>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="requestId">Id identifying the request. If set to null, we will send a random number between 100000 and 999999</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request</param>
    /// <returns>The response from the Infrasec API or an exception if the request fails</returns>
    Task<Try<StatusEnrollmentResponse, Exception>> GetStatusCcuAsync(StatusEnrollmentData data, NonEmptyString applicationId, int? requestId, CancellationToken cancellationToken);

    /// <summary>
    /// Enrolls a new cash register. Request and responses use xml.
    /// </summary>
    /// <param name="data">Data related to the cash register</param>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="requestId">Id identifying the request. If set to null, we will send a random number between 100000 and 999999</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request</param>
    /// <returns>The response from the Infrasec API or an exception if the request fails</returns>
    Task<Try<NewEnrollmentResponse, Exception>> EnrollCcuAsync(NewEnrollmentData data, NonEmptyString applicationId, int? requestId, CancellationToken cancellationToken);

    /// <summary>
    /// Accepts receipt VAT information and returns a control code.
    /// </summary>
    /// <param name="data">The transaction data to send.</param>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="requestId">Id identifying the request. If set to null, we will send a new Guid.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>The response from the Infrasec API or an exception if the request fails.</returns>
    Task<Try<TransactionResponse, Exception>> SendTransactionAsync(TransactionData data, NonEmptyString applicationId, Guid? requestId, CancellationToken cancellationToken);
}