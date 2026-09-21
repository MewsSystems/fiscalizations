namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// One transaction from /queryTransactionList. This is how a transaction id lost to a client timeout is
/// recovered before an invoice is submitted a second time (spec 1.9.2).
/// </summary>
public sealed class TransactionListItem
{
    public TransactionListItem(string transactionId, DateTime insertedUtc, string requestStatus, bool isTechnicalAnnulment)
    {
        TransactionId = transactionId;
        InsertedUtc = insertedUtc;
        RequestStatus = requestStatus;
        IsTechnicalAnnulment = isTechnicalAnnulment;
    }

    public string TransactionId { get; }

    public DateTime InsertedUtc { get; }

    public string RequestStatus { get; }

    public bool IsTechnicalAnnulment { get; }
}

public sealed class TransactionList
{
    public TransactionList(int currentPage, int availablePage, IEnumerable<TransactionListItem> transactions)
    {
        CurrentPage = currentPage;
        AvailablePage = availablePage;
        Transactions = transactions.ToList();
    }

    public int CurrentPage { get; }

    public int AvailablePage { get; }

    public List<TransactionListItem> Transactions { get; }
}
