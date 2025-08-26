using Mews.Fiscalizations.Germany.V2.Model;
using Mews.Fiscalizations.Germany.Utils;

namespace Mews.Fiscalizations.Germany.V2;

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
            environment: MapEnvironment(token.AuthorizationTokenData.Environment),
            expirationUtc: token.AccessTokenExpiresAt.FromUnixTime()
        ));
    }

    internal static Model.Client MapClient(Dto.ClientResponse client)
    {
        return new Model.Client(
            serialNumber: client.SerialNumber,
            created: client.TimeCreation.FromUnixTime(),
            state: MapClientState(client.State),
            tssId: client.TssId,
            id: client.Id
        );
    }

    internal static Tss MapTss(Dto.TssResponse tss)
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

    private static FiskalyEnvironment MapEnvironment(Dto.FiskalyEnvironment environment)
    {
        return environment.Match(
            Dto.FiskalyEnvironment.TEST, _ => FiskalyEnvironment.Test,
            Dto.FiskalyEnvironment.LIVE, _ => FiskalyEnvironment.Production
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
            Dto.TssState.DEFECTIVE, _ => TssState.Defective,
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