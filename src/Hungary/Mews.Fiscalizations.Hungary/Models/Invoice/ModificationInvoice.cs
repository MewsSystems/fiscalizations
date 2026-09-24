namespace Mews.Fiscalizations.Hungary.Models;

public sealed class ModificationInvoice : Invoice
{
    private ModificationInvoice(
        Invoice invoice,
        ModificationOperation operation,
        InvoiceNumber originalDocumentNumber,
        int modificationIndex,
        int itemIndexOffset,
        bool modifyWithoutMaster)
        : base(
            invoice.Number,
            invoice.Category,
            invoice.IssueDate,
            invoice.PaymentDate,
            invoice.SupplierInfo,
            invoice.Receiver,
            invoice.CurrencyCode,
            invoice.ExchangeRate,
            invoice.Items,
            invoice.IsSelfBilling,
            invoice.IsCashAccounting,
            invoice.IsCompleteDataReport,
            invoice.PaymentMethod.ToNullable())
    {
        Operation = operation;
        OriginalDocumentNumber = originalDocumentNumber;
        ModificationIndex = modificationIndex;
        ItemIndexOffset = itemIndexOffset;
        ModifyWithoutMaster = modifyWithoutMaster;
    }

    /// <summary>Whether NAV is told MODIFY or STORNO.</summary>
    public ModificationOperation Operation { get; }

    public InvoiceNumber OriginalDocumentNumber { get; }

    /// <summary>
    /// NAV modificationIndex: this document's position in the original invoice's chain, starting at 1. It is
    /// read back from /queryInvoiceChainDigest rather than derived locally, because only NAV knows what it
    /// already holds.
    /// </summary>
    public int ModificationIndex { get; }

    /// <summary>
    /// The highest line number already reported in the chain. This document's lines continue from it, which
    /// is what lineNumberReference has to be for a CREATE line operation.
    /// </summary>
    public int ItemIndexOffset { get; }

    /// <summary>
    /// True when the invoice this document modifies was never reported and never will be, so NAV should not
    /// look for it.
    /// </summary>
    public bool ModifyWithoutMaster { get; }

    public static Try<ModificationInvoice, Error> Create(
        Invoice invoice,
        ModificationOperation operation,
        InvoiceNumber originalDocumentNumber,
        int modificationIndex,
        int itemIndexOffset,
        bool modifyWithoutMaster)
    {
        if (modificationIndex < 1)
        {
            // The schema's InvoiceUnboundedIndexType starts at 1.
            return Try.Error<ModificationInvoice, Error>(new Error("Modification index must be at least 1."));
        }
        if (itemIndexOffset < 0)
        {
            return Try.Error<ModificationInvoice, Error>(new Error("Item index offset cannot be negative."));
        }
        if (originalDocumentNumber.Value == invoice.Number.Value)
        {
            // NAV rejects a modification document whose own number equals the number it references.
            return Try.Error<ModificationInvoice, Error>(new Error("A modification document cannot have the same number as the invoice it modifies."));
        }
        return Try.Success<ModificationInvoice, Error>(new ModificationInvoice(
            invoice,
            operation,
            originalDocumentNumber,
            modificationIndex,
            itemIndexOffset,
            modifyWithoutMaster
        ));
    }
}
