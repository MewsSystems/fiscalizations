using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalizations.Hungary
{
    internal static class RequestMapper
    {
        internal static Dto.InvoiceData MapModificationInvoice(ModificationInvoice invoice)
        {
            var lines = MapItems(invoice.Items, invoice.ItemIndexOffset);
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
            var lines = MapItems(invoice.Items);
            var invoiceDto = GetCommonInvoice(invoice, lines);
            return GetCommonInvoiceData(invoice, invoiceDto);
        }

        private static Dto.InvoiceData GetCommonInvoiceData(Invoice invoice, Dto.InvoiceType invoiceDto)
        {
            return new Dto.InvoiceData
            {
                invoiceIssueDate = invoice.IssueDate,
                invoiceNumber = invoice.Number.Value,
                completenessIndicator = invoice.Receiver.Match(
                    customer => false,
                    company => true
                ),
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
                    line = lines.ToArray()
                },
                invoiceHead = new Dto.InvoiceHeadType
                {
                    invoiceDetail = new Dto.InvoiceDetailType
                    {
                        exchangeRate = invoice.ExchangeRate.Value,
                        currencyCode = invoice.CurrencyCode.Value,
                        invoiceAppearance = Dto.InvoiceAppearanceType.ELECTRONIC,
                        invoiceCategory = Dto.InvoiceCategoryType.AGGREGATE,
                        invoiceDeliveryDate = invoice.DeliveryDate,
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
                vatRate = GetVatRate(taxSummary.TaxRatePercentage),
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
                lineVatRate = GetVatRate(item.TotalAmounts.TaxRatePercentage),
                lineVatData = new Dto.LineVatDataType
                {
                    lineVatAmount = item.TotalAmounts.Amount.Tax.Value,
                    lineVatAmountHUF = item.TotalAmounts.AmountHUF.Tax.Value
                }
            };
        }

        private static IEnumerable<Dto.LineType> MapItems(ISequence<InvoiceItem> items, int? modificationIndexOffset = null)
        {
            return items.Values.Select(i => new Dto.LineType
            {
                lineNumber = i.Index.ToString(),
                lineDescription = i.Value.Description.Value,
                quantity = i.Value.Quantity,
                unitOfMeasureOwn = i.Value.MeasurementUnit.ToString(),
                unitPrice = i.Value.UnitAmounts.Amount.Net.Value,
                unitPriceHUF = i.Value.UnitAmounts.AmountHUF.Net.Value,
                quantitySpecified = true,
                unitOfMeasureSpecified = true,
                unitPriceSpecified = true,
                unitPriceHUFSpecified = true,
                depositIndicator = i.Value.IsDeposit,
                Item = MapLineAmounts(i.Value),
                aggregateInvoiceLineData = new Dto.AggregateInvoiceLineDataType
                {
                    lineExchangeRateSpecified = true,
                    lineExchangeRate = i.Value.ExchangeRate.Map(r => r.Value).GetOrElse(0m),
                    lineDeliveryDate = i.Value.ConsumptionDate
                },
                lineModificationReference = modificationIndexOffset.HasValue ? GetLineModificationReference(i, modificationIndexOffset.Value) : null
            });
        }

        private static Dto.LineModificationReferenceType GetLineModificationReference(Indexed<InvoiceItem> item, int modificationIndexOffset)
        {
            return new Dto.LineModificationReferenceType
            {
                lineNumberReference = (item.Index + modificationIndexOffset).ToString(),
                lineOperation = Dto.LineOperationType.CREATE
            };
        }

        private static Dto.VatRateType GetVatRate(IOption<decimal> taxRatePercentage)
        {
            return taxRatePercentage.Match(
                p => new Dto.VatRateType
                {
                    Item = p,
                    ItemElementName = Dto.ItemChoiceType2.vatPercentage
                },
                _ => new Dto.VatRateType
                {
                    Item = true,
                    ItemElementName = Dto.ItemChoiceType2.noVatCharge
                }
            );
        }
    }
}
