﻿using System.Net.Http.Headers;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Sweden.Models;
using FuncSharp;
using Mews.Fiscalizations.Sweden.DTOs;
using Mews.Fiscalizations.Sweden.Mappers;

namespace Mews.Fiscalizations.Sweden;

public sealed class InfrasecClient : IInfrasecClient
{
    private readonly HttpClient _transactionHttpClient;
    private readonly HttpClient _enrollmentHttpClient;
    private readonly string _infrasecTransactionApiUrl;
    private readonly string _infrasecEnrollmentApiUrl;
    private const string TestTransactionApiUrl = "https://tcs-verify.infrasec-api.se";
    private const string ProductionTransactionApiUrl = "https://tcs.infrasec-api.se"; // TODO: check if this is correct
    private const string TestEnrollmentApiUrl = "https://idm-verify.infrasec.se/api/EnrollCCU.php";
    private const string TcsServerEndpoint = "/tcsserver";

    public InfrasecClient(InfrasecConfiguration configuration)
    {
        _infrasecTransactionApiUrl = configuration.Environment switch
        {
            Environment.Test => TestTransactionApiUrl,
            Environment.Production => ProductionTransactionApiUrl,
            _ => throw new ArgumentOutOfRangeException(configuration.Environment.ToString())
        };

        _infrasecEnrollmentApiUrl = configuration.Environment switch
        {
            Environment.Test => TestEnrollmentApiUrl,
            Environment.Production => throw new NotSupportedException("Production environment is not supported yet"),
            _ => throw new ArgumentOutOfRangeException(configuration.Environment.ToString())
        };

        //transaction http client
        var transactionHandler = new HttpClientHandler();
        transactionHandler.ClientCertificates.Add(configuration.TransactionCertificate);
        transactionHandler.ClientCertificates.AddRange(configuration.TransactionSigningCertificates.ToArray());
        transactionHandler.ServerCertificateCustomValidationCallback = (_, cert, _, errors) => ValidateServerCertificate(cert!, configuration.TransactionSigningCertificates, errors);

        _transactionHttpClient = new HttpClient(transactionHandler);
        _transactionHttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(configuration.UserAgent);

        //enrollment http client
        var enrollmentHandler = new HttpClientHandler();
        enrollmentHandler.ClientCertificates.Add(configuration.EnrollmentCertificate);
        enrollmentHandler.ClientCertificates.AddRange(configuration.EnrollmentSigningCertificates.ToArray());
        enrollmentHandler.ServerCertificateCustomValidationCallback = (_, cert, _, errors) => ValidateServerCertificate(cert!, configuration.EnrollmentSigningCertificates, errors);

        _enrollmentHttpClient = new HttpClient(enrollmentHandler);
        _enrollmentHttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(configuration.UserAgent);
    }

    public async Task<Try<StatusEnrollmentResponse, Exception>> GetStatusCcuAsync(StatusEnrollmentData data,
        NonEmptyString applicationId, int? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToStatusDto(applicationId: applicationId, requestId: requestId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var endpoint = new Uri(_infrasecEnrollmentApiUrl);
        var xmlString = xml.OuterXml;
        var stringContent = new StringContent(xmlString, new MediaTypeHeaderValue(MediaTypeNames.Application.Xml)
        {
            CharSet = string.Empty
        });
        var response = await _enrollmentHttpClient.PostAsync(endpoint, stringContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<StatusEnrollmentResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}")
            );
        }
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var enrollmentResponse = Try.Catch<IdmResponse, Exception>(
            _ => XmlSerializer.Deserialize<IdmResponse>(responseContent)
        );
        return enrollmentResponse.Map(r => r.FromStatusDto());
    }

    public async Task<Try<NewEnrollmentResponse, Exception>> EnrollCcuAsync(NewEnrollmentData data, NonEmptyString applicationId, int? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToNewDto(applicationId: applicationId, requestId: requestId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var endpoint = new Uri(_infrasecEnrollmentApiUrl);
        var xmlString = xml.OuterXml;
        var stringContent = new StringContent(xmlString, new MediaTypeHeaderValue(MediaTypeNames.Application.Xml)
        {
            CharSet = string.Empty
        });
        var response = await _enrollmentHttpClient.PostAsync(endpoint, stringContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<NewEnrollmentResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}")
            );
        }
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var enrollmentResponse = Try.Catch<IdmResponse, Exception>(
            _ => XmlSerializer.Deserialize<IdmResponse>(responseContent)
        );
        return enrollmentResponse.Map(r => r.FromNewDto());
    }

    public async Task<Try<TransactionResponse, Exception>> SendTransactionAsync(TransactionData data, NonEmptyString applicationId, Guid? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToDto(applicationId:applicationId, requestId: requestId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var endpoint = new Uri($"{_infrasecTransactionApiUrl}{TcsServerEndpoint}");
        var response = await _transactionHttpClient.PostAsync(endpoint, new StringContent(xml.OuterXml, Encoding.UTF8, MediaTypeNames.Application.Xml), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<TransactionResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}")
            );
        }
        var tcsResponse = await Try.CatchAsync<DTOs.TcsResponse, Exception>(
            async _ => XmlSerializer.Deserialize<DTOs.TcsResponse>(await response.Content.ReadAsStringAsync(cancellationToken))
        );
        return tcsResponse.Map(r => r.FromDto());
    }

    private static bool ValidateServerCertificate(X509Certificate certificate, IEnumerable<X509Certificate> signingCertificates, SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors.HasFlag(SslPolicyErrors.None))
        {
            return true;
        }
        if (sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors))
        {
            foreach (var cert in signingCertificates)
            {
                var chain = new X509Chain
                {
                    ChainPolicy = new X509ChainPolicy
                    {
                        RevocationMode = X509RevocationMode.NoCheck,
                        ExtraStore = { cert },
                        VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority
                    }
                };
                return chain.Build((X509Certificate2)certificate);
            }
        }
        return false;
    }
}