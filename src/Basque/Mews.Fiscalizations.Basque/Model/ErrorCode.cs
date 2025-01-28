namespace Mews.Fiscalizations.Basque.Model;

// Reference: https://www.gipuzkoa.eus/documents/2456431/28540714/cas+01+TICKETBAI+ALTA+2.1.pdf/710e8dd2-825e-5fd0-d825-a7be669e624e.
public enum ErrorCode
{
    // Sender certificate must be approved, not revoked and not expired (more than one month).
    RevokedOrExpiredCertificate = 001,

    // The file must comply with the XSD schema.
    XsdSchemeViolation = 002,

    // The file must include detail lines.
    MissingInvoiceDetailLines = 003,

    // All mandatory data must appear without erroneous data (NumFactura not empty, Signature and NumFacturaPrevious, SignatureValueFirmaFacturaPrevious
    // when the ChainingFacturaPrevious element is reported, LicenciaTBAI correct format,
    // Issuing NIF not admissible)
    MissingMandatoryData = 004,

    // The file must not have been previously received.
    DuplicateInvoice = 005,

    // If the reception service is not available, the operation must be repeated later.
    ServerErrorTryAgain = 006,

    // The sender certificate must be valid for the invoice issuer.
    InvalidIssuerCertificate = 007,

    // The signature must be valid or the signing certificate must not be expired (more than one month).
    InvalidSignatureOrSigningCertificate = 08,

    // Invalid or missing chain.
    InvalidOrMissingInvoiceChain = 09,

    InvalidIssuerNif = 10,

    // The rectified (Corrected) invoice is not indicated.
    CorrectedInvoiceNotIndicated = 011,

    InvalidIssuerNameOrNif = 12,

    // Issuer NIF must be registered in the Araba region.
    IssuerNifMustBeRegisteredInArabaRegion = 15,

    // The size of the message must not exceed the allowed size.
    MessageLengthLimitExceeded = 017,

    // Returned when using a test certificate in Araba region.
    ArabaRegionTestCertificate = 998,

    // Invalid ID field value. NIF-VAT must be correct for the indicated country. Brexit control.
    InvalidCountryTaxIdentifier = 1104,

    // Country Code is required when IDType other than NIF-VAT
    MissingCountryCode = 1124,

    // The Invoice Issue Date is greater than the current date.
    InvoiceIssueDateGreaterThanCurrentDate = 1125,

    // If the Simplified Substitution Issued Invoice field is ·N· and the Corrective Invoice field is not
    // reported, the Substituted Rectified (corrected) Invoices field may not be reported.
    CorrectedInvoiceMustNotBeReportedWhenCorrectiveInvoiceIsNotReported = 1137,

    // The operations may have, within the subject party, an exempt party and/or a non-exempt party. By
    // Therefore, only one block or both may appear, but at least one must appear (Exempt and/or NotExempt).
    InvoiceMustContainAtLeastOneExemptOrNonExemptParty = 1140,

    // The operations may have a subject part and a non-subject part. Therefore, there can be only one
    // block or both, but at least one must appear (Subject and/or NotSubject).
    InvoiceMustContainSubjectOrNoSubjectPart = 1141,

    // The Country Code field indicated for the identification of NIF-VAT does not coincide with the first two ID characters.
    TaxIdentifierCountryCodeDoesntMatchCountryCodeField = 1146,

    // Breakdown Type of Operation needs at least Provision of Services or Delivery or both.
    BreakdownMustHaveProvisionOrDeliveryOrBoth = 1148,

    // The recipient's NIF has the wrong format.
    InvalidReceiverTaxIdentifierFormat = 1153
}