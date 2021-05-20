using Mews.Fiscalization.Germany.Model;
using Mews.Fiscalization.Germany.Utils;
using System;

namespace Mews.Fiscalization.Germany
{
    internal static class ModelMapper
    {
        internal static ResponseResult<Transaction> MapTransaction(Dto.TransactionResponse transaction)
        {
            return new ResponseResult<Transaction>(successResult: new Transaction(
                id: transaction.Id,
                number: transaction.Number.ToString(),
                startUtc: transaction.TimeStart.FromUnixTime(),
                endUtc: transaction.TimeEnd.FromUnixTime(),
                certificateSerial: transaction.CertificateSerial,
                signature: new Signature(
                    value: transaction.Signature.Value,
                    counter: transaction.Signature.Counter,
                    algorithm: transaction.Signature.Algorithm,
                    publicKey: transaction.Signature.PublicKey
                ),
                qrCodeData: transaction.QrCodeData
            ));
        }

        internal static ResponseResult<AccessToken> MapAccessToken(Dto.AuthorizationTokenResponse token)
        {
            return new ResponseResult<AccessToken>(successResult: new AccessToken(
                value: token.AccessToken,
                expirationUtc: token.AccessTokenExpiresAt.FromUnixTime()
            ));
        }

        internal static ResponseResult<Model.Client> MapClient(Dto.ClientResponse client)
        {
            return new ResponseResult<Model.Client>(successResult: new Model.Client(
                serialNumber: client.SerialNumber,
                created: client.TimeCreation.FromUnixTime(),
                updated: client.TimeUpdate.FromUnixTime(),
                tssId: client.TssId,
                id: client.Id
            ));
        }

        internal static ResponseResult<Tss> MapTss(Dto.TssResponse tss)
        {
            return new ResponseResult<Tss>(successResult: new Tss(
                id: tss.Id,
                description: tss.Description,
                state: MapTssState(tss.State),
                createdUtc: tss.TimeCreation.FromUnixTime(),
                initializedUtc: tss.TimeInit.FromUnixTime(),
                disabledUtc: tss.TimeDisable.FromUnixTime(),
                certificate: tss.Certificate,
                certificateSerial: tss.CertificateSerial,
                publicKey: tss.PublicKey,
                signatureCounter: tss.SignatureCounter,
                signatureAlgorithm: tss.SignatureAlgorithm,
                transactionCounter: tss.TransactionCounter
            ));
        }

        private static TssState MapTssState(Dto.TssState state)
        {
            switch (state)
            {
                case Dto.TssState.DISABLED:
                    return TssState.Disabled;
                case Dto.TssState.INITIALIZED:
                    return TssState.Initialized;
                case Dto.TssState.UNINITIALIZED:
                    return TssState.Uninitialized;
                default:
                    throw new NotImplementedException($"Tss state: {state} is not implemented.");
            };
        }
    }
}
