using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden;

public interface IInfrasecClient
{
    /// <summary>
    /// Accepts receipt VAT information and returns a control code.
    /// </summary>
    /// <param name="data">The transaction data to send.</param>
    /// <param name="applicationId">The application identifier.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>The response from the Infrasec API or an exception if the request fails.</returns>
    Task<Try<TransactionResponse, Exception>> SendTransactionAsync(TransactionData data, NonEmptyString applicationId, CancellationToken cancellationToken);
}