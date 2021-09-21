using FuncSharp;
using Mews.Fiscalizations.Germany.Model;
using Mews.Fiscalizations.Germany.Utils;
using System;

namespace Mews.Fiscalizations.Germany
{
    internal static class ModelMapper
    {
        internal static ResponseResult<Transaction> MapTransaction(Dto.TransactionResponse transaction)
        {
            return new ResponseResult<Transaction>(successResult: new Transaction(
                id: transaction.Id,
                number: transaction.Number.ToString(),
                startUtc: transaction.TimeStart.FromUnixTime(),
                state: MapTransactionState(transaction.State),
                endUtc: transaction.TimeEnd.FromUnixTime(),
                certificateSerial: transaction.CertificateSerial,
                clientId: transaction.ClientId,
                tssId: transaction.TssId,
                clientSerialNumber: transaction.ClientSerialNumber,
                tssSerialNumber: transaction.TssSerialNumber,
                signature: new Signature(
                    value: transaction.Signature.Value,
                    counter: transaction.Signature.Counter,
                    algorithm: transaction.Signature.Algorithm,
                    publicKey: transaction.Signature.PublicKey
                ),
                qrCodeData: transaction.QrCodeData
            ));
        }

        internal static TransactionState MapTransactionState(Dto.State state)
        {
            return state.Match(
                Dto.State.ACTIVE, _ => TransactionState.Active,
                Dto.State.CANCELLED, _ => TransactionState.Canceled,
                Dto.State.FINISHED, _ => TransactionState.Finished
            );
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
                state: MapClientState(client.State),
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
                serialNumber: tss.SerialNumber,
                publicKey: tss.PublicKey,
                signatureCounter: tss.SignatureCounter,
                signatureAlgorithm: tss.SignatureAlgorithm,
                transactionCounter: tss.TransactionCounter
            ));
        }

        internal static ResponseResult<CreateTss> MapCreateTss(Dto.CreateTssResponse createTssResponse)
        {
            return new ResponseResult<CreateTss>(successResult: new CreateTss(
                adminPuk: createTssResponse.AdminPuk,
                tss: new Tss(
                    id: createTssResponse.Id,
                    description: createTssResponse.Description,
                    state: MapTssState(createTssResponse.State),
                    createdUtc: createTssResponse.TimeCreation.FromUnixTime(),
                    initializedUtc: createTssResponse.TimeInit.FromUnixTime(),
                    disabledUtc: createTssResponse.TimeDisable.FromUnixTime(),
                    certificate: createTssResponse.Certificate,
                    serialNumber: createTssResponse.SerialNumber,
                    publicKey: createTssResponse.PublicKey,
                    signatureCounter: createTssResponse.SignatureCounter,
                    signatureAlgorithm: createTssResponse.SignatureAlgorithm,
                    transactionCounter: createTssResponse.TransactionCounter
                )
            ));
        }

        private static TssState MapTssState(Dto.TssState state)
        {
            return state.Match(
                Dto.TssState.CREATED, _ => TssState.Created,
                Dto.TssState.DISABLED, _ => TssState.Disabled,
                Dto.TssState.INITIALIZED, _ => TssState.Initialized,
                Dto.TssState.UNINITIALIZED, _ => TssState.Uninitialized,
                _ => throw new NotImplementedException($"Tss state: {state} is not implemented.")
            );
        }

        private static ClientState MapClientState(Dto.ClientState state)
        {
            return state.Match(
                Dto.ClientState.REGISTERED, _ => ClientState.Registered,
                Dto.ClientState.DEREGISTERED, _ => ClientState.Deregistered
            );
        }
    }
}
