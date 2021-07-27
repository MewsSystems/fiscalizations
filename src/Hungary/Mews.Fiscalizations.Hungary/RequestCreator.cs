using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Hungary.Models;
using Mews.Fiscalizations.Hungary.Utils;
using System;
using System.Linq;

namespace Mews.Fiscalizations.Hungary
{
    internal static class RequestCreator
    {
        private static readonly string RequestVersion = "3.0";
        private static readonly string HeaderVersion = "1.0";
        private static readonly string RequestEncryptionAlgorithm = "SHA3-512";
        private static readonly string PasswordEncryptionAlgorithm = "SHA-512";

        internal static Dto.TokenExchangeRequest CreateTokenExchangeRequest(TechnicalUser user, SoftwareIdentification software)
        {
            return CreateRequest<Dto.TokenExchangeRequest>(user, software);
        }

        internal static Dto.QueryTaxpayerRequest CreateQueryTaxpayerRequest(TechnicalUser user, SoftwareIdentification software, string taxNumber)
        {
            var request = CreateRequest<Dto.QueryTaxpayerRequest>(user, software);
            request.taxNumber = taxNumber;
            return request;
        }

        internal static Dto.QueryTransactionStatusRequest CreateQueryTransactionStatusRequest(TechnicalUser user, SoftwareIdentification software, string invoiceId)
        {
            var request = CreateRequest<Dto.QueryTransactionStatusRequest>(user, software);
            request.transactionId = invoiceId;
            return request;
        }

        internal static Dto.ManageInvoiceRequest CreateManageInvoicesRequest(TechnicalUser user, SoftwareIdentification software, ExchangeToken token, ISequence<Invoice> invoices)
        {
            return CreateManageInvoicesRequest(user, software, token, Dto.ManageInvoiceOperationType.CREATE, invoices, i => RequestMapper.MapInvoice(i));
        }

        internal static Dto.ManageInvoiceRequest CreateManageInvoicesRequest(TechnicalUser user, SoftwareIdentification software, ExchangeToken token, ISequence<ModificationInvoice> invoices)
        {
            return CreateManageInvoicesRequest(user, software, token, Dto.ManageInvoiceOperationType.MODIFY, invoices, d => RequestMapper.MapModificationInvoice(d));
        }

        private static Dto.ManageInvoiceRequest CreateManageInvoicesRequest<T>(
            TechnicalUser user,
            SoftwareIdentification software,
            ExchangeToken token,
            Dto.ManageInvoiceOperationType operation,
            ISequence<T> invoices,
            Func<T, Dto.InvoiceData> invoiceDataGetter)
        {
            var operations = invoices.Values.Select(item =>
            {
                var invoiceData = invoiceDataGetter(item.Value);
                var parameters = new XmlSerializationParameters(namespaces: ServiceInfo.XmlNamespace.ToEnumerable());
                var serializedInvoiceData = XmlSerializer.Serialize(invoiceData, parameters);
                var invoiceDataBytes = ServiceInfo.Encoding.GetBytes(serializedInvoiceData.OuterXml);
                return new Dto.InvoiceOperationType
                {
                    index = item.Index,
                    invoiceData = invoiceDataBytes,
                    invoiceOperation = operation,
                    electronicInvoiceHash = invoiceData.completenessIndicator.Match(
                        t => new Dto.CryptoType
                        {
                            cryptoType = RequestEncryptionAlgorithm,
                            Value = Sha512.GetSha3Hash(Convert.ToBase64String(invoiceDataBytes))
                        },
                        f => null
                    )
                };
            });
            var invoiceHashes = operations.Select(t => Sha512.GetSha3Hash($"{t.invoiceOperation}{Convert.ToBase64String(t.invoiceData)}"));
            var invoiceSignatureData = string.Join("", invoiceHashes);

            var request = CreateRequest<Dto.ManageInvoiceRequest>(user, software, invoiceSignatureData);
            request.exchangeToken = ServiceInfo.Encoding.GetString(token.Value);
            request.invoiceOperations = new Dto.InvoiceOperationListType
            {
                compressedContent = false,
                invoiceOperation = operations.ToArray()
            };

            return request;
        }

        private static T CreateRequest<T>(TechnicalUser user, SoftwareIdentification software, string additionalSignatureData = null)
            where T : Dto.BasicOnlineInvoiceRequestType, new()
        {
            var requestId = RequestId.CreateRandom();
            var nowUtc = DateTime.UtcNow;
            var timestamp = new DateTime(nowUtc.Year, nowUtc.Month, nowUtc.Day, nowUtc.Hour, nowUtc.Minute, nowUtc.Second, DateTimeKind.Utc);
            return new T
            {
                header = new Dto.BasicHeaderType
                {
                    requestId = requestId,
                    timestamp = timestamp,
                    requestVersion = RequestVersion,
                    headerVersion = HeaderVersion
                },
                user = new Dto.UserHeaderType
                {
                    login = user.Login.Value,
                    passwordHash = new Dto.CryptoType
                    {
                        cryptoType = PasswordEncryptionAlgorithm,
                        Value = user.PasswordHash
                    },
                    taxNumber = user.TaxId.TaxpayerNumber,
                    requestSignature = new Dto.CryptoType
                    {
                        cryptoType = RequestEncryptionAlgorithm,
                        Value = GetRequestSignature(user, requestId, timestamp, additionalSignatureData)
                    }
                },
                software = new Dto.SoftwareType
                {
                    softwareId = software.Id,
                    softwareName = software.Name,
                    softwareOperation = (Dto.SoftwareOperationType)software.Type,
                    softwareMainVersion = software.MainVersion,
                    softwareDevName = software.DeveloperName,
                    softwareDevContact = software.DeveloperContact,
                    softwareDevCountryCode = software.DeveloperCountry,
                    softwareDevTaxNumber = software.DeveloperTaxNumber
                }
            };
        }

        private static string GetRequestSignature(TechnicalUser user, string requestId, DateTime timestamp, string additionalSignatureData = null)
        {
            var formattedTimestamp = timestamp.ToString("yyyyMMddHHmmss");
            var signatureData = $"{requestId}{formattedTimestamp}{user.SigningKey.Value}{additionalSignatureData}";
            return Sha512.GetSha3Hash(signatureData);
        }
    }
}