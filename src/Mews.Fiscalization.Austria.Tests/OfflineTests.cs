using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mews.Fiscalization.Austria.Dto;
using Mews.Fiscalization.Austria.Dto.Identifiers;
using Mews.Fiscalization.Austria.Offline;
using NUnit.Framework;
using TimeZoneConverter;

namespace Mews.Fiscalization.Austria.Tests
{
    public class OfflineTests
    {
        public static readonly string CertificateData = "MIIDegIBAzCCA0AGCSqGSIb3DQEHAaCCAzEEggMtMIIDKTCCAh8GCSqGSIb3DQEHBqCCAhAwggIMAgEAMIICBQYJKoZIhvcNAQcBMBwGCiqGSIb3DQEMAQYwDgQImiYmqb2YnPsCAggAgIIB2PELpKIMSu+PRU82cB5bkjMCR79d+t08AMR4HWT35N6JUtaJmzZek6JAP4IusiMDU0TgXvvBKmO6OM4dN1EPlcqYvELpeIGgNa/+odGgJGiM7TLlmLmtQSjIySr9haS710SBrBL4464jzzzhGeABbdEV1I4bDSuB0nSqDfh/9xskJvB5sklxk599aWkodp40lcRI9oGvtKGdRSXF8i06S669TrZ65lqntCUrWXuGcFqEAvWOQXsnuG0o8HLeiZzda22c8gcU/6cLJHdA57qE7LSD99b/HZNz35NFEtUjQtd3M60dHs1+aHoU0XV0Av1dqLfohqKaPfTSkEFBQ18NASXgFK33LZSeUnvNmR+zX19k3Dz1lLugugZhqEzKz2kWdw2uUwROQJ3RY/VxkSl3ZabcfLMhRP4qJ6iLBH3q4qSU8Fgu86B1fqSBXK33ylltSGe3F257B5G3wpwsoFWh55J26Ggbo+uALu0TSemw1BPXT9qrMj+Q1V5rUDynCMkphz99EgBxPxLlCcmSR86X1sa/0hgWXI7LjH9GGvxYSdi11j+G3LF+SCr2y/+FiiduSlh0Um6Zbb1J2MuWXfw2cvy0Gkr4fLYjdfnbJLFepSX3tX0/8sGuEqEwggECBgkqhkiG9w0BBwGggfQEgfEwge4wgesGCyqGSIb3DQEMCgECoIG0MIGxMBwGCiqGSIb3DQEMAQMwDgQIVPRU6MhGtk4CAggABIGQsZ8U01TXTfftU+K7XdawadWrhg3Aj+b8LUoLk16RGniPBXlG2T6IWSBMi2kKc/QmsNellx4HyTEPKiC/GqqHFZxspeRuIKKy6EBCIWXOYTRqEoBy//etgEV1s2yj2tDfIkjZ2kkt/9ydSJPMlDSlSaS/C7h37owZdvC/kXLkhphguDf4R4p/7xCmm2qPeDCHMSUwIwYJKoZIhvcNAQkVMRYEFCchXQb0E101J24QyRXeMdxpV9MYMDEwITAJBgUrDgMCGgUABBQVY50LXy3izUqNAnWPxrW5smzyNQQIheQuwv/ZW0kCAggA";

        [Test]
        public void OfflineSignatureWorks()
        {
            var certificate = new X509Certificate2(Convert.FromBase64String(CertificateData));
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var europeTimeZone = "Central Europe Standard Time";
            var austrianTimeZone = TimeZoneInfo.FindSystemTimeZoneById(isWindows ? europeTimeZone : TZConvert.WindowsToIana(europeTimeZone));
            var austrianCulture = CultureInfo.GetCultureInfo("de-AT");

            var signer = new OfflineSigner(certificate);
            var result = signer.Sign(new QrData(new Receipt(
                number: new ReceiptNumber("83469"),
                registerIdentifier: new RegisterIdentifier("DEMO-CASH-BOX817"),
                taxData: new TaxData(
                    standartRate: new CurrencyValue(29.73m),
                    lowerReducedRate: new CurrencyValue(36.41m),
                    specialRate: new CurrencyValue(21.19m)
                ),
                turnover: new CurrencyValue(0.0m),
                certificateSerialNumber: new CertificateSerialNumber(certificate.SerialNumber),
                previousJwsRepresentation: new JwsRepresentation("eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.WDFJeExVRlVNVjloT0RRME1URXpZaTFoTTJRM0xUUmxObU10T0RGak9DMDJOalU0TXpnMk9HVm1NelpmTTE4eU1ERTNMVEV5TFRFeVZERXlPalV6T2pVMlh6QXNNREJmTVRBd0xEQXdYekFzTURCZk1Dd3dNRjh3TERBd1h6ZzNMMnR2YW05RVYwUjNQVjh3TUVJd05qQkJNRUkwTWpFMlJUQXhSRFJmZVROVVp6TXlOV1Z0Y0UwOQ.6mzl1HSWmJyWaUG0pZlNuF29Eg9jocyXSuBxYWnwskE3fpVLd2PTIHG9ecBvQnCW3SokaMiEEgYN969Z4P7i0w"),
                key: Convert.FromBase64String("RCsRmHn5tkLQrRpiZq2ucwPpwvHJLiMgLvwrwEImddI="),
                created: new LocalDateTime(
                    new DateTime(year: 2015, month: 11, day: 25, hour: 19, minute: 20, second: 11),
                    austrianTimeZone
                )
            ), austrianCulture, austrianTimeZone));
            Assert.IsNotNull(result);
        }
    }
}