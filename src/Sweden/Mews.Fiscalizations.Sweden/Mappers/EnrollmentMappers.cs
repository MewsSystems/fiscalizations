﻿using Mews.Fiscalizations.Sweden.DTOs;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden.Mappers;

internal static class EnrollmentMappers
{
    private const string NewEnrollmentAction = "NEW";
    private const string StatusEnrollmentAction = "STATUS";
    private const string PosAuthorityCode = "S";
    private const string CertificateMethod = "NONE";
    private const string TcsEnable = "Yes";
    private const string SwishEnable = "No";
    private const string DigitalReceipt = "No";

    internal static StatusEnrollmentResponse FromStatusDto(this IdmResponse dto, string requestXml)
    {
        return new StatusEnrollmentResponse(
            requestXml: requestXml,
            responseCode: dto.ResponseCode,
            responseMessage: dto.ResponseMessage,
            responseReason: dto.ResponseReason,
            action: dto.Action,
            tcsId: dto.TcsId!,
            active: dto.Active!.Value,
            loginCount: dto.LoginCount!.Value,
            lastLogin: dto.LastLogin!
        );
    }

    internal static NewEnrollmentResponse FromNewDto(this IdmResponse dto, string requestXml)
    {
        return new NewEnrollmentResponse(
            requestXml: requestXml,
            responseCode: dto.ResponseCode,
            responseMessage: dto.ResponseMessage,
            responseReason: dto.ResponseReason,
            applicationId: dto.ApplicationId!,
            requestId: dto.RequestId!,
            action: dto.Action,
            registerId: dto.RegisterId!,
            tcsId: dto.TcsId!
        );
    }

    internal static IdmRequest ToNewDto(this NewEnrollmentData data, string applicationId, int? requestId)
    {
        return new IdmRequest
        {
            ApplicationId = applicationId,
            RequestId = requestId ?? new Random().Next(100000, 999999),
            EnrollData = new EnrollData
            {
                Action = NewEnrollmentAction,
                PartnerAuthority = new PartnerAuthority
                {
                    PartnerCode = data.PartnerCode,
                    PartnerName = data.PartnerName,
                    PosAuthorityCode = PosAuthorityCode
                },
                OrganizationBranch = new OrganizationBranch
                {
                    BranchCode = string.Empty,
                    BranchName = string.Empty
                },
                OrganizationChain = new OrganizationChain
                {
                    ChainCode = data.ChainCode,
                    ChainName = data.ChainName
                },
                StoreInfo = new StoreInfo
                {
                    StoreId = data.StoreId,
                    StoreName = data.StoreName,
                    Address = data.StoreAddressLine,
                    City = data.StoreCity,
                    Zipcode = data.StoreZipCode
                },
                CompanyInfo = new CompanyInfo
                {
                    OrganizationNumber = data.StoreCompanyOrgNr,
                    Company = data.StoreCompanyName,
                    Address = data.StoreAddressLine,
                    City = data.StoreCity,
                    Zipcode = data.StoreZipCode
                },
                RegisterInfo = new RegisterInfo
                {
                    RegisterId = string.Empty,
                    RegisterMake = data.RegisterMake,
                    RegisterModel = data.IntegrationVersion,
                    LocalAlias = data.LocalAlias,
                    CounterNumber = string.Empty,
                    Address = data.StoreAddressLine,
                    Zipcode = data.StoreZipCode,
                    City = data.StoreCity
                },
                JournalLocation = new JournalLocation
                {
                    Company = data.StoreCompanyName,
                    Address = data.StoreAddressLine,
                    Zipcode = data.StoreZipCode,
                    City = data.StoreCity
                },
                OperationLocation = new OperationLocation
                {
                    Address = data.StoreAddressLine,
                    City = data.StoreCity,
                    Company = data.StoreCompanyName,
                    Zipcode = data.StoreZipCode
                },
                Certificate = new CertificateInfo
                {
                    Method = CertificateMethod,
                    Email = string.Empty,
                    Cellphone = string.Empty
                },
                PcxService = new PcxService
                {
                    Tcs = new Tcs
                    {
                        Enable = TcsEnable
                    },
                    Swish = new Swish
                    {
                        Enable = SwishEnable,
                        SwishNr = string.Empty,
                        SwishType = string.Empty
                    },
                    Sparakvittot = new Sparakvittot
                    {
                        Enable = DigitalReceipt,
                        SparakvittotAccount = string.Empty,
                        SparakvittotStoreid = string.Empty,
                        SparakvittotUsername = string.Empty,
                        SparakvittotPassword = string.Empty
                    }
                }
            }
        };
    }

    public static IdmRequest ToStatusDto(this StatusEnrollmentData data, string applicationId, int? requestId)
    {
        return new IdmRequest
        {
            ApplicationId = applicationId,
            RequestId = requestId ?? new Random().Next(100000, 999999),
            EnrollData = new EnrollData
            {
                Action = StatusEnrollmentAction,
                RegisterInfo = new RegisterInfo
                {
                    RegisterId = data.RegisterId
                }
            }
        };
    }
}