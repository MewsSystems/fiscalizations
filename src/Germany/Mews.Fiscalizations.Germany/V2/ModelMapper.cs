﻿using FuncSharp;
using Mews.Fiscalizations.Germany.V2.Model;
using Mews.Fiscalizations.Germany.Utils;
using System;
using System.Linq;

namespace Mews.Fiscalizations.Germany.V2
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
            return new ResponseResult<Model.Client>(successResult: GetClient(client));
        }

        internal static ResponseResult<Tss> MapTss(Dto.TssResponse tss)
        {
            return new ResponseResult<Tss>(successResult: GetTss(tss));
        }

        internal static ResponseResult<MultipleTss> MapTSSs(Dto.MultipleTssResponse response)
        {
            return new ResponseResult<MultipleTss>(successResult: new MultipleTss(response.TssList.Select(t => GetTss(t))));
        }

        internal static ResponseResult<MultipleClient> MapClients(Dto.MultipleClientResponse response)
        {
            return new ResponseResult<MultipleClient>(successResult: new MultipleClient(response.Clients.Select(c => GetClient(c))));
        }

        internal static ResponseResult<CreateTssResult> MapCreateTss(Dto.CreateTssResponse createTssResponse)
        {
            return new ResponseResult<CreateTssResult>(successResult: new CreateTssResult(
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

        private static Tss GetTss(Dto.TssResponse tss)
        {
            return new Tss(
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
            );
        }

        private static Model.Client GetClient(Dto.ClientResponse client)
        {
            return new Model.Client(
                serialNumber: client.SerialNumber,
                created: client.TimeCreation.FromUnixTime(),
                state: MapClientState(client.State),
                tssId: client.TssId,
                id: client.Id
            );
        }

        private static TssState MapTssState(Dto.TssState state)
        {
            return state.Match(
                Dto.TssState.CREATED, _ => TssState.Created,
                Dto.TssState.DISABLED, _ => TssState.Disabled,
                Dto.TssState.INITIALIZED, _ => TssState.Initialized,
                Dto.TssState.UNINITIALIZED, _ => TssState.Uninitialized,
                Dto.TssState.DELETED, _ => TssState.Deleted,
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