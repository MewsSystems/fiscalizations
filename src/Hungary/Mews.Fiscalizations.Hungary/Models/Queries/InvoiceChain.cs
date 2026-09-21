namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// One element of an invoice's chain as returned by /queryInvoiceChainDigest - the original invoice and
/// every modification reported against it.
/// </summary>
public sealed class InvoiceChainDigest
{
    public InvoiceChainDigest(string invoiceNumber, string invoiceOperation, DateTime insertedUtc, int maxLineNumber, int? modificationIndex)
    {
        InvoiceNumber = invoiceNumber;
        InvoiceOperation = invoiceOperation;
        InsertedUtc = insertedUtc;
        MaxLineNumber = maxLineNumber;
        ModificationIndex = modificationIndex.ToOption();
    }

    public string InvoiceNumber { get; }

    /// <summary>CREATE, MODIFY or STORNO, as NAV recorded it.</summary>
    public string InvoiceOperation { get; }

    public DateTime InsertedUtc { get; }

    /// <summary>The highest line number this element reported.</summary>
    public int MaxLineNumber { get; }

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

    /// <summary>The highest line number already in the chain; a new document's lines continue from it.</summary>
    public int MaxLineNumber => Elements.Select(e => e.MaxLineNumber).DefaultIfEmpty(0).Max();
}
