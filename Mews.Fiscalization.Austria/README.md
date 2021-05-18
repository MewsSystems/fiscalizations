# Mews.Fiscalization.Austria

A client library that allows you to generate QR codes compliant with Austrian fiscal law (RKSV2017). Currently offers a signer based on A-Trust WS RK.Online API.

## Usage example
### Creating QR data from bills
```csharp
var qrData = new QrData(new Receipt(
    number: new ReceiptNumber("83469"),
    registerIdentifier: new RegisterIdentifier("DEMO-CASH-BOX817"),
    taxData: new TaxData(
        standartRate: new CurrencyValue(29.73m),
        lowerReducedRate: new CurrencyValue(36.41m),
        specialRate: new CurrencyValue(21.19m)
    ),
    turnover: new CurrencyValue(0.0m), 
    certificateSerialNumber: new CertificateSerialNumber("-3667961875706356849"),
    previousJwsRepresentation: new JwsRepresentation("..."),
    created: new LocalDateTime(
        new DateTime(2015, 11, 25, 19, 20, 11),
        LocalDateTime.AustrianTimezone
    )
);
```

### Using A-Trust RK.Online signer
```csharp
var signer = new ATrustSigner(Credentials, ATrustEnvironment.Test);
var output = signer.Sign(qrData);
```

### SignerOutput Usage
```csharp
fiscalRecord.JwsRepresentation = output.JwsRepresentation.Value;
fiscalRecord.QrData = output.SignedQrData.Value;
fiscalRecord.Signature = output.JwsRepresentation.Signature.Value;
```

## Security protocol
- TLS 1.1 and TLS 1.2 protocols must be enabled, that can be achieved by adding the following line to your code:
```csharp
ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11
```

# NuGet

We have published the library as [Mews.Fiscalization.Austria](https://www.nuget.org/packages/Mews.Fiscalization.Austria/).
