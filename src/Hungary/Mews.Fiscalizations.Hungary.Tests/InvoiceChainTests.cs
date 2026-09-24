namespace Mews.Fiscalizations.Hungary.Tests;

public sealed class InvoiceChainTests
{
    [Test]
    public void MaxLineNumber_OriginalOnly_IsTheOriginalsHighestLine()
    {
        var chain = MapChain(Original(maxLineNumber: 3));

        Assert.That(chain.MaxLineNumber, Is.EqualTo(3));
        Assert.That(chain.NextModificationIndex, Is.EqualTo(1));
    }

    [Test]
    public void MaxLineNumber_AfterAModification_ContinuesFromTheLinesItAdded()
    {
        // The modification added lines 4-5 to the original's numbering, but its own lines are numbered 1-2, so
        // NAV reports maxLineNumber 2 for it (spec 1.8.3.2 (2)). Continuing from 3 would make the next document
        // reuse line 4, which NAV rejects as INVOICE_LINE_ALREADY_EXISTS.
        var chain = MapChain(
            Original(maxLineNumber: 3),
            Modification(modificationIndex: 1, maxLineNumber: 2, (4, 5))
        );

        Assert.That(chain.MaxLineNumber, Is.EqualTo(5));
        Assert.That(chain.NextModificationIndex, Is.EqualTo(2));
    }

    [Test]
    public void MaxLineNumber_ModificationWithSeveralIntervals_UsesTheLastOne()
    {
        var chain = MapChain(
            Original(maxLineNumber: 3),
            Modification(modificationIndex: 1, maxLineNumber: 3, (4, 5), (8, 8))
        );

        Assert.That(chain.MaxLineNumber, Is.EqualTo(8));
    }

    [Test]
    public void MaxLineNumber_EmptyChain_IsZero()
    {
        var chain = MapChain();

        Assert.That(chain.IsEmpty, Is.True);
        Assert.That(chain.MaxLineNumber, Is.EqualTo(0));
    }

    private static InvoiceChain MapChain(params Dto.InvoiceChainElementType[] elements)
    {
        var response = new Dto.QueryInvoiceChainDigestResponse
        {
            invoiceChainDigestResult = new Dto.InvoiceChainDigestResultType
            {
                currentPage = 1,
                availablePage = 1,
                invoiceChainElement = elements
            }
        };
        return ModelMapper.MapInvoiceChain(requestXml: "", responseXml: "", response).SuccessResult;
    }

    private static Dto.InvoiceChainElementType Original(int maxLineNumber)
    {
        return new Dto.InvoiceChainElementType
        {
            invoiceChainDigest = Digest(Dto.ManageInvoiceOperationType.CREATE),
            invoiceLines = new Dto.InvoiceLinesType { maxLineNumber = maxLineNumber.ToString() }
        };
    }

    private static Dto.InvoiceChainElementType Modification(int modificationIndex, int maxLineNumber, params (int Start, int End)[] createdLines)
    {
        return new Dto.InvoiceChainElementType
        {
            invoiceChainDigest = Digest(Dto.ManageInvoiceOperationType.MODIFY),
            invoiceLines = new Dto.InvoiceLinesType
            {
                maxLineNumber = maxLineNumber.ToString(),
                newCreatedLines = createdLines.Select(l => new Dto.NewCreatedLinesType
                {
                    lineNumberIntervalStart = l.Start.ToString(),
                    lineNumberIntervalEnd = l.End.ToString()
                }).ToArray()
            },
            invoiceReferenceData = new Dto.InvoiceReferenceDataType { originalInvoiceNumber = "INV-1", Item = modificationIndex }
        };
    }

    private static Dto.InvoiceChainDigestType Digest(Dto.ManageInvoiceOperationType operation)
    {
        return new Dto.InvoiceChainDigestType
        {
            invoiceNumber = "INV-1",
            invoiceOperation = operation,
            insDate = new DateTime(2026, 9, 24, 10, 0, 0, DateTimeKind.Utc)
        };
    }
}
