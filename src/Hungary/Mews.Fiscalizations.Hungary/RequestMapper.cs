namespace Mews.Fiscalizations.Hungary;

internal static class RequestMapper
{
    internal static Dto.InvoiceData MapModificationInvoice(ModificationInvoice invoice)
    {
        var lines = MapItems(invoice.Category, invoice.Items, invoice.ItemIndexOffset);
        var invoiceReference = new Dto.InvoiceReferenceType
        {
            modificationIndex = invoice.ModificationIndex,
            originalInvoiceNumber = invoice.OriginalDocumentNumber.Value,
            modifyWithoutMaster = invoice.ModifyWithoutMaster
        };

        var invoiceDto = GetCommonInvoice(invoice, lines, invoiceReference);
        return GetCommonInvoiceData(invoice, invoiceDto);
    }

    internal static Dto.InvoiceData MapInvoice(Invoice invoice)
    {
        var lines = MapItems(invoice.Category, invoice.Items);
        var invoiceDto = GetCommonInvoice(invoice, lines);
        return GetCommonInvoiceData(invoice, invoiceDto);
    }

    private static Dto.InvoiceData GetCommonInvoiceData(Invoice invoice, Dto.InvoiceType invoiceDto)
    {
        return new Dto.InvoiceData
        {
            invoiceIssueDate = invoice.IssueDate,
            invoiceNumber = invoice.Number.Value,
            completenessIndicator = invoice.IsCompleteDataReport,
            invoiceMain = new Dto.InvoiceMainType
            {
                Items = new object[] { invoiceDto }
            }
        };
    }

    private static Dto.InvoiceType GetCommonInvoice(Invoice invoice, IEnumerable<Dto.LineType> lines, Dto.InvoiceReferenceType invoiceReference = null)
    {
        var invoiceAmount = Models.Amount.Sum(invoice.Items.Values.Select(i => i.Value.TotalAmounts.Amount));
        var invoiceAmountHUF = Models.Amount.Sum(invoice.Items.Values.Select(i => i.Value.TotalAmounts.AmountHUF));
        var supplierInfo = invoice.SupplierInfo;
        var receiver = invoice.Receiver;
        return new Dto.InvoiceType
        {
            invoiceReference = invoiceReference,
            invoiceLines = new Dto.LinesType
            {
                mergedItemIndicator = false,
                line = lines.ToArray()
            },
            invoiceHead = new Dto.InvoiceHeadType
            {
                invoiceDetail = new Dto.InvoiceDetailType
                {
                    exchangeRate = invoice.ExchangeRate.Value,
                    currencyCode = invoice.CurrencyCode.Value,
                    invoiceAppearance = Dto.InvoiceAppearanceType.ELECTRONIC,
                    invoiceCategory = MapCategory(invoice.Category),
                    // For an aggregate invoice this is the latest of the per-item delivery dates, which is
                    // what the spec asks for; the individual dates are reported on the items themselves.
                    invoiceDeliveryDate = invoice.DeliveryDate,
                    // invoiceDeliveryPeriodStart/End are deliberately not emitted. They declare a periodic
                    // settlement supply under Afa tv. 58. paragraph, where the tax point follows the
                    // settlement period - a different thing from a gyujtoszamla under 164. paragraph, which
                    // is several distinct supplies invoiced together and carries its dates per item. Filling
                    // them raises NAV warning 561 unless periodicalSettlement is also true, and setting that
                    // would misdeclare when the tax became chargeable.
                    paymentDate = invoice.PaymentDate,
                    paymentDateSpecified = true,
                    selfBillingIndicator = invoice.IsSelfBilling,
                    cashAccountingIndicator = invoice.IsCashAccounting,
                    paymentMethod = invoice.PaymentMethod.Map(m => MapPaymentMethod(m)).GetOrElse(Dto.PaymentMethodType.OTHER),
                    paymentMethodSpecified = invoice.PaymentMethod.NonEmpty
                },
                supplierInfo = new Dto.SupplierInfoType
                {
                    supplierName = supplierInfo.Name.Value,
                    supplierAddress = MapAddress(supplierInfo.Address),
                    supplierTaxNumber = new Dto.TaxNumberType
                    {
                        taxpayerId = supplierInfo.TaxpayerId.Value.TaxpayerNumber,
                        vatCode = supplierInfo.VatCode.Value
                    }
                },
                customerInfo = new Dto.CustomerInfoType
                {
                    // NAV rejects a data report that carries a name or an address for a private person, so
                    // those are emitted only for a company.
                    customerName = receiver.Match(
                        customer => null,
                        company => company.Name.Value
                    ),
                    customerAddress = receiver.Match(
                        customer => null,
                        company => MapAddress(company.Address)
                    ),
                    customerVatStatus = receiver.Match(
                        customer => Dto.CustomerVatStatusType.PRIVATE_PERSON,
                        company => company.Match(
                            local => Dto.CustomerVatStatusType.DOMESTIC,
                            foreign => Dto.CustomerVatStatusType.OTHER
                        )
                    ),
                    // Spec 2.1.4.1 case 7: a company with no tax number reports no customerVatData at all.
                    customerVatData = receiver.Match(
                        customer => Option.Empty<Dto.CustomerVatDataType>(),
                        company => company.Match(
                            local => GetCustomerVatDataType(local.TaxpayerId.Value).ToOption(),
                            foreign => foreign.TaxpayerId.Map(i => GetCustomerVatDataType(i))
                        )
                    ).GetOrNull()
                },
            },
            invoiceSummary = new Dto.SummaryType
            {
                summaryGrossData = new Dto.SummaryGrossDataType
                {
                    invoiceGrossAmount = invoiceAmount.Gross.Value,
                    invoiceGrossAmountHUF = invoiceAmountHUF.Gross.Value
                },
                Items = new object[]
                {
                    MapTaxSummary(invoice, invoiceAmount, invoiceAmountHUF)
                }
            }
        };
    }

    private static Dto.InvoiceCategoryType MapCategory(InvoiceCategory category)
    {
        return category.Match(
            InvoiceCategory.Normal, _ => Dto.InvoiceCategoryType.NORMAL,
            InvoiceCategory.Aggregate, _ => Dto.InvoiceCategoryType.AGGREGATE
        );
    }

    private static Dto.PaymentMethodType MapPaymentMethod(PaymentMethod paymentMethod)
    {
        return paymentMethod.Match(
            PaymentMethod.Card, _ => Dto.PaymentMethodType.CARD,
            PaymentMethod.Cash, _ => Dto.PaymentMethodType.CASH,
            PaymentMethod.Transfer, _ => Dto.PaymentMethodType.TRANSFER,
            PaymentMethod.Voucher, _ => Dto.PaymentMethodType.VOUCHER,
            PaymentMethod.Other, _ => Dto.PaymentMethodType.OTHER
        );
    }

    private static Dto.CustomerVatDataType GetCustomerVatDataType(TaxpayerIdentificationNumber taxpayerNumber)
    {
        // Core still lists the United Kingdom as an EU member. Since Brexit a UK number is a third country tax
        // number, and NAV warns when it is reported as a community VAT number.
        var isThirdCountry = taxpayerNumber.Match(european => european.Country.Alpha2Code == Countries.UnitedKingdom.Alpha2Code, nonEuropean => true);
        if (isThirdCountry)
        {
            return new Dto.CustomerVatDataType
            {
                Item = taxpayerNumber.TaxpayerNumber,
                ItemElementName = Dto.ItemChoiceType.thirdStateTaxId
            };
        }
        return taxpayerNumber.Match(
            european => european.Country.Alpha2Code.Match(
                Countries.Hungary.Alpha2Code, _ => new Dto.CustomerVatDataType
                {
                    Item = new Dto.CustomerTaxNumberType
                    {
                        taxpayerId = taxpayerNumber.TaxpayerNumber
                    },
                    ItemElementName = Dto.ItemChoiceType.customerTaxNumber
                },
                _ => new Dto.CustomerVatDataType
                {
                    Item = taxpayerNumber.TaxpayerNumber,
                    ItemElementName = Dto.ItemChoiceType.communityVatNumber
                }
            ),
            nonEuropean => new Dto.CustomerVatDataType
            {
                Item = taxpayerNumber.TaxpayerNumber,
                ItemElementName = Dto.ItemChoiceType.thirdStateTaxId
            }
        );
    }

    private static Dto.SummaryNormalType MapTaxSummary(Invoice invoice, Models.Amount amount, Models.Amount amountHUF)
    {
        return new Dto.SummaryNormalType
        {
            invoiceNetAmount = amount.Net.Value,
            invoiceNetAmountHUF = amountHUF.Net.Value,
            invoiceVatAmount = amount.Tax.Value,
            invoiceVatAmountHUF = amountHUF.Tax.Value,
            summaryByVatRate = invoice.TaxSummary.Select(s => MapSummaryByVatRate(s)).ToArray()
        };
    }

    private static Dto.SummaryByVatRateType MapSummaryByVatRate(TaxSummaryItem taxSummary)
    {
        return new Dto.SummaryByVatRateType
        {
            vatRate = GetVatRate(taxSummary.VatRate),
            vatRateNetData = new Dto.VatRateNetDataType
            {
                vatRateNetAmount = taxSummary.Amount.Net.Value,
                vatRateNetAmountHUF = taxSummary.AmountHUF.Net.Value
            },
            vatRateVatData = new Dto.VatRateVatDataType
            {
                vatRateVatAmount = taxSummary.Amount.Tax.Value,
                vatRateVatAmountHUF = taxSummary.AmountHUF.Tax.Value
            },
            vatRateGrossData = new Dto.VatRateGrossDataType
            {
                vatRateGrossAmount = taxSummary.Amount.Gross.Value,
                vatRateGrossAmountHUF = taxSummary.AmountHUF.Gross.Value
            }
        };
    }

    private static Dto.AddressType MapAddress(SimpleAddress address)
    {
        return new Dto.AddressType
        {
            Item = new Dto.SimpleAddressType
            {
                additionalAddressDetail = address.AddtionalAddressDetail.Value,
                city = address.City.Value,
                countryCode = address.Country.Alpha2Code,
                postalCode = address.PostalCode.Value,
                region = address.Region.Map(r => r.Value).GetOrNull()
            }
        };
    }

    private static Dto.LineAmountsNormalType MapLineAmounts(InvoiceItem item)
    {
        return new Dto.LineAmountsNormalType
        {
            lineGrossAmountData = new Dto.LineGrossAmountDataType
            {
                lineGrossAmountNormal = item.TotalAmounts.Amount.Gross.Value,
                lineGrossAmountNormalHUF = item.TotalAmounts.AmountHUF.Gross.Value
            },
            lineNetAmountData = new Dto.LineNetAmountDataType
            {
                lineNetAmount = item.TotalAmounts.Amount.Net.Value,
                lineNetAmountHUF = item.TotalAmounts.AmountHUF.Net.Value
            },
            lineVatRate = GetVatRate(item.TotalAmounts.VatRate),
            lineVatData = new Dto.LineVatDataType
            {
                lineVatAmount = item.TotalAmounts.Amount.Tax.Value,
                lineVatAmountHUF = item.TotalAmounts.AmountHUF.Tax.Value
            }
        };
    }

    private static IEnumerable<Dto.LineType> MapItems(InvoiceCategory category, ISequence<InvoiceItem> items, int? modificationIndexOffset = null)
    {
        return items.Values.Select(i =>
        {
            var item = i.Value;
            var line = new Dto.LineType
            {
                lineNumber = i.Index.ToString(),
                // Mandatory since request version 1.1, and it declares which line fields NAV should expect.
                // Every line reported here carries a description, quantity, unit and unit price.
                lineExpressionIndicator = true,
                lineDescription = item.Description.Value,
                quantity = item.Quantity,
                quantitySpecified = true,
                unitOfMeasure = MapUnitOfMeasure(item.UnitOfMeasure.Kind),
                unitOfMeasureSpecified = true,
                unitOfMeasureOwn = item.UnitOfMeasure.OwnValue.GetOrNull(),
                unitPrice = item.UnitAmounts.Amount.Net.Value,
                unitPriceSpecified = true,
                unitPriceHUF = item.UnitAmounts.AmountHUF.Net.Value,
                unitPriceHUFSpecified = true,
                Item = MapLineAmounts(item),
                lineModificationReference = modificationIndexOffset.HasValue ? GetLineModificationReference(i, modificationIndexOffset.Value) : null
            };

            // Only an aggregate invoice reports per-item delivery dates and rates, and NAV runs its
            // aggregate-only reconciliation checks whenever this node is present.
            if (category == InvoiceCategory.Aggregate)
            {
                line.aggregateInvoiceLineData = new Dto.AggregateInvoiceLineDataType
                {
                    lineDeliveryDate = item.DeliveryDate,
                    lineExchangeRate = item.LineExchangeRate.Map(r => r.Value).GetOrElse(1m),
                    lineExchangeRateSpecified = item.LineExchangeRate.NonEmpty
                };
            }

            // NAV depositIndicator means a bottle or container deposit. An advance charge belongs in
            // advanceData (spec 2.9), which is what this is.
            if (item.IsAdvance)
            {
                line.advanceData = new Dto.AdvanceDataType
                {
                    advanceIndicator = true,
                    advancePaymentData = item.AdvancePaymentData.Map(d => new Dto.AdvancePaymentDataType
                    {
                        advanceOriginalInvoice = d.OriginalInvoiceNumber.Value,
                        advancePaymentDate = d.PaymentDate,
                        advanceExchangeRate = d.ExchangeRate.Value
                    }).GetOrNull()
                };
            }

            return line;
        });
    }

    private static Dto.UnitOfMeasureType MapUnitOfMeasure(UnitOfMeasureKind kind)
    {
        return kind.Match(
            UnitOfMeasureKind.Piece, _ => Dto.UnitOfMeasureType.PIECE,
            UnitOfMeasureKind.Day, _ => Dto.UnitOfMeasureType.DAY,
            UnitOfMeasureKind.Own, _ => Dto.UnitOfMeasureType.OWN
        );
    }

    private static Dto.LineModificationReferenceType GetLineModificationReference(Indexed<InvoiceItem> item, int modificationIndexOffset)
    {
        return new Dto.LineModificationReferenceType
        {
            lineNumberReference = (item.Index + modificationIndexOffset).ToString(),
            // Spec 2.5.3: after the INVALID_LINE_OPERATION validation only CREATE is accepted. A modification
            // adds new lines continuing the original invoice's numbering rather than editing existing ones.
            lineOperation = Dto.LineOperationType.CREATE
        };
    }

    private static Dto.VatRateType GetVatRate(VatRate vatRate)
    {
        return vatRate.Match(
            percentage => new Dto.VatRateType
            {
                Item = percentage,
                ItemElementName = Dto.ItemChoiceType2.vatPercentage
            },
            exemption => new Dto.VatRateType
            {
                Item = new Dto.DetailedReasonType { @case = exemption.Case, reason = exemption.Reason },
                ItemElementName = Dto.ItemChoiceType2.vatExemption
            },
            outOfScope => new Dto.VatRateType
            {
                Item = new Dto.DetailedReasonType { @case = outOfScope.Case, reason = outOfScope.Reason },
                ItemElementName = Dto.ItemChoiceType2.vatOutOfScope
            },
            _ => new Dto.VatRateType
            {
                Item = true,
                ItemElementName = Dto.ItemChoiceType2.vatDomesticReverseCharge
            },
            _ => new Dto.VatRateType
            {
                Item = true,
                ItemElementName = Dto.ItemChoiceType2.noVatCharge
            }
        );
    }
}
