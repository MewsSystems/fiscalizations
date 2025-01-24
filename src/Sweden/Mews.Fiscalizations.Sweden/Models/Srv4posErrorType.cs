namespace Mews.Fiscalizations.Sweden.Models;

public enum Srv4posErrorType
{
    ServerSideError = 0,
    InvalidRequest = 1,
    InvalidCorporateId = 2,
    InvalidCashRegisterName = 3,
    InvalidFieldFormat = 4,
    NonUniqueCashRegisterNameOrCorporateId = 5,
    UnknownError = 6
}