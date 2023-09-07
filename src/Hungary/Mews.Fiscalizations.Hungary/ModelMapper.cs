namespace Mews.Fiscalizations.Hungary;

internal static class ModelMapper
{
    internal static ResponseResult<ExchangeToken, ExchangeTokenErrorCode> MapExchangeToken(
        string requestXml,
        string responseXml,
        Dto.TokenExchangeResponse response,
        TechnicalUser user)
    {
        try
        {
            var decryptedToken = Aes.Decrypt(user.EncryptionKey.Value, response.encodedExchangeToken);
            return new ResponseResult<ExchangeToken, ExchangeTokenErrorCode>(
                requestXml: requestXml,
                responseXml: responseXml,
                successResult: new ExchangeToken(
                    value: decryptedToken,
                    validFrom: response.tokenValidityFrom,
                    validTo: response.tokenValidityTo
                )
            );
        }
        catch
        {
            return new ResponseResult<ExchangeToken, ExchangeTokenErrorCode>(
                requestXml: requestXml,
                responseXml: responseXml,
                operationErrorResult: new ErrorResult<ExchangeTokenErrorCode>(errorCode: ExchangeTokenErrorCode.InvalidEncryptionKey)
            );
        }
    }

    internal static ResponseResult<TaxPayerData, TaxPayerErrorCode> MapTaxPayerData(
        string requestXml,
        string responseXml,
        Dto.QueryTaxpayerResponse response)
    {
        if (response.taxpayerValidity)
        {
            var addressItem = response.taxpayerData.taxpayerAddressList.First();
            var taxPayerData = response.taxpayerData;
            var taxNumberDetail = taxPayerData.taxNumberDetail;
            return new ResponseResult<TaxPayerData, TaxPayerErrorCode>(
                requestXml: requestXml,
                responseXml: responseXml,
                successResult: new TaxPayerData(
                    id: taxNumberDetail.taxpayerId,
                    name: taxPayerData.taxpayerName,
                    address: MapAddress(addressItem),
                    vatCode: taxNumberDetail.vatCode,
                    infoDate: response.infoDate,
                    incorporationType: MapIncorporationType(taxPayerData.incorporation)
                )
            );
        }
        else
        {
            return new ResponseResult<TaxPayerData, TaxPayerErrorCode>(
                requestXml: requestXml,
                responseXml: responseXml,
                operationErrorResult: new ErrorResult<TaxPayerErrorCode>(errorCode: TaxPayerErrorCode.InvalidTaxPayer)
            );
        }
    }

    internal static ResponseResult<TransactionStatus, TransactionErrorCode> MapTransactionStatus(
        string requestXml,
        string responseXml,
        Dto.QueryTransactionStatusResponse response)
    {
        var result = response.processingResults;
        if (result?.processingResult == null)
        {
            return new ResponseResult<TransactionStatus, TransactionErrorCode>(
                requestXml: requestXml,
                responseXml: responseXml,
                operationErrorResult: new ErrorResult<TransactionErrorCode>(errorCode: TransactionErrorCode.InvalidTransactionId)
            );
        }

        return new ResponseResult<TransactionStatus, TransactionErrorCode>(
            requestXml: requestXml,
            responseXml: responseXml,
            successResult: new TransactionStatus(invoiceStatuses: result.processingResult.Select(r => InvoiceStatus.Map(r)))
        );
    }

    internal static ResponseResult<string, ResultErrorCode> MapManageInvoice(
        string requestXml,
        string responseXml,
        Dto.ManageInvoiceResponse response)
    {
        return new ResponseResult<string, ResultErrorCode>(
            requestXml: requestXml,
            responseXml: responseXml,
            successResult: response.transactionId
        );
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
            type: MapAddressType(addressItem.taxpayerAddressType)
        );
    }

    private static AddressType MapAddressType(Dto.TaxpayerAddressTypeType type)
    {
        return type.Match(
            Dto.TaxpayerAddressTypeType.HQ, _ => AddressType.HQ,
            Dto.TaxpayerAddressTypeType.SITE, _ => AddressType.SITE,
            Dto.TaxpayerAddressTypeType.BRANCH, _ => AddressType.BRANCH
        );
    }
}