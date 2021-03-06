using Mews.Fiscalizations.Hungary.Models;
using Mews.Fiscalizations.Hungary.Utils;
using System.Linq;
using FuncSharp;

namespace Mews.Fiscalizations.Hungary
{
    internal static class ModelMapper
    {
        internal static ResponseResult<ExchangeToken, ExchangeTokenErrorCode> MapExchangeToken(Dto.TokenExchangeResponse response, TechnicalUser user)
        {
            try
            {
                var decryptedToken = Aes.Decrypt(user.EncryptionKey.Value, response.encodedExchangeToken);
                return new ResponseResult<ExchangeToken, ExchangeTokenErrorCode>(successResult: new ExchangeToken(
                    value: decryptedToken,
                    validFrom: response.tokenValidityFrom,
                    validTo: response.tokenValidityTo
                ));
            }
            catch
            {
                return new ResponseResult<ExchangeToken, ExchangeTokenErrorCode>(operationErrorResult: new ErrorResult<ExchangeTokenErrorCode>(
                    errorCode: ExchangeTokenErrorCode.InvalidEncryptionKey
                ));
            }
        }

        internal static ResponseResult<TaxPayerData, TaxPayerErrorCode> MapTaxPayerData(Dto.QueryTaxpayerResponse response)
        {
            if (response.taxpayerValidity)
            {
                var addressItem = response.taxpayerData.taxpayerAddressList.First();
                var taxPayerData = response.taxpayerData;
                var taxNumberDetail = taxPayerData.taxNumberDetail;
                return new ResponseResult<TaxPayerData, TaxPayerErrorCode>(successResult: new TaxPayerData(
                    id: taxNumberDetail.taxpayerId,
                    name: taxPayerData.taxpayerName,
                    address: MapAddress(addressItem),
                    vatCode: taxNumberDetail.vatCode,
                    infoDate: response.infoDate,
                    incorporationType: MapIncorporationType(taxPayerData.incorporation)
                ));
            }
            else
            {
                return new ResponseResult<TaxPayerData, TaxPayerErrorCode>(operationErrorResult: new ErrorResult<TaxPayerErrorCode>(
                    errorCode: TaxPayerErrorCode.InvalidTaxPayer
                ));
            }
        }

        internal static ResponseResult<TransactionStatus, TransactionErrorCode> MapTransactionStatus(Dto.QueryTransactionStatusResponse response)
        {
            var result = response.processingResults;
            if (result?.processingResult == null)
            {
                return new ResponseResult<TransactionStatus, TransactionErrorCode>(operationErrorResult: new ErrorResult<TransactionErrorCode>(
                    errorCode: TransactionErrorCode.InvalidTransactionId
                ));
            }

            return new ResponseResult<TransactionStatus, TransactionErrorCode>(
                successResult: new TransactionStatus(
                    invoiceStatuses: result.processingResult.Select(r => InvoiceStatus.Map(r))
                )
            );
        }

        internal static ResponseResult<string, ResultErrorCode> MapManageInvoice(Dto.ManageInvoiceResponse response)
        {
            return new ResponseResult<string, ResultErrorCode>(successResult: response.transactionId);
        }

        private static IncorporationType MapIncorporationType(Dto.IncorporationType incorporationType)
        {
            return incorporationType.Match(
                Dto.IncorporationType.ORGANIZATION, _ => IncorporationType.Organization,
                Dto.IncorporationType.SELF_EMPLOYED, _ => IncorporationType.SelfEmployed,
                Dto.IncorporationType.TAXABLE_PERSON, _ => IncorporationType.TaxablePerson
            );
        }

        private static Address MapAddress(Dto.TaxpayerAddressItemType addressItem)
        {
            var address = addressItem.taxpayerAddress;
            return new Address(
                countryCode: address.countryCode,
                postalCode: address.postalCode,
                city: address.city,
                streetName: address.streetName,
                number: address.number,
                floor: address.floor,
                door: address.door,
                type: MapAddressType(addressItem.taxpayerAddressType, nameof(addressItem.taxpayerAddress))
            );
        }

        private static AddressType MapAddressType(Dto.TaxpayerAddressTypeType type, string parameterName)
        {
            return type.Match(
                Dto.TaxpayerAddressTypeType.HQ, _ => AddressType.HQ,
                Dto.TaxpayerAddressTypeType.SITE, _ => AddressType.SITE,
                Dto.TaxpayerAddressTypeType.BRANCH, _ => AddressType.BRANCH
            );
        }
    }
}