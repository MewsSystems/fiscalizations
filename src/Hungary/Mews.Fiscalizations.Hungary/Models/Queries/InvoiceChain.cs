namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// One element of an invoice's chain as returned by /queryInvoiceChainDigest - the original invoice and
/// every modification reported against it.
/// </summary>
public sealed class InvoiceChainDigest
{
    public InvoiceChainDigest(string invoiceNumber, string invoiceOperation, DateTime insertedUtc, int lastChainLineNumber, int? modificationIndex)
    {
        InvoiceNumber = invoiceNumber;
        InvoiceOperation = invoiceOperation;
        InsertedUtc = insertedUtc;
        LastChainLineNumber = lastChainLineNumber;
        ModificationIndex = modificationIndex.ToOption();
    }

    public string InvoiceNumber { get; }

    /// <summary>CREATE, MODIFY or STORNO, as NAV recorded it.</summary>
    public string InvoiceOperation { get; }

    public DateTime InsertedUtc { get; }

    /// <summary>
    /// The highest line number this element occupies in the original invoice's numbering. For the original that
    /// is its maxLineNumber. A modification numbers its own lines from 1 (spec 1.8.3.2 (2)), so where its lines
    /// sit in the chain is only in newCreatedLines, and this is the end of the last interval it added.
    /// </summary>
    public int LastChainLineNumber { get; }

    /// <summary>Absent on the original invoice, present on every modification.</summary>
    public Option<int> ModificationIndex { get; }
}

/// <summary>
/// What NAV already holds for an invoice. A modification document's modificationIndex and line numbering
/// have to continue from this rather than be derived locally, because only NAV knows what it accepted.
/// </summary>
public sealed class InvoiceChain
{
    public InvoiceChain(int currentPage, int availablePage, IEnumerable<InvoiceChainDigest> elements)
    {
        CurrentPage = currentPage;
        AvailablePage = availablePage;
        Elements = elements.ToList();
    }

    public int CurrentPage { get; }

    public int AvailablePage { get; }

    public List<InvoiceChainDigest> Elements { get; }

    /// <summary>
    /// True when NAV holds nothing for this invoice number, which is what modifyWithoutMaster is for.
    /// </summary>
    public bool IsEmpty => Elements.Count == 0;

    /// <summary>The index the next modification document must carry. NAV numbers modifications from 1.</summary>
    public int NextModificationIndex => Elements.Select(e => e.ModificationIndex.GetOrElse(0)).DefaultIfEmpty(0).Max() + 1;

    /// <summary>
    /// The highest line number already in the chain, in the original invoice's numbering; a new document's
    /// lineNumberReference values continue from it.
    /// </summary>
    public int MaxLineNumber => Elements.Select(e => e.LastChainLineNumber).DefaultIfEmpty(0).Max();
}
