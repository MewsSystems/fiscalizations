using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Mews.Fiscalizations.Austria.ATrust;
using Mews.Fiscalizations.Austria.Dto;
using Mews.Fiscalizations.Austria.Dto.Identifiers;
using NUnit.Framework;
using TimeZoneConverter;

namespace Mews.Fiscalizations.Austria.Tests
{
    public class ATrustSignerTests
    {
        public static readonly ATrustUserIdentifier UserId = new ATrustUserIdentifier(Environment.GetEnvironmentVariable("austrian_user_id") ?? "INSERT_USER_ID");
        public static readonly string Password = Environment.GetEnvironmentVariable("austrian_password") ?? "INSERT_PASSWORD";

        private static ATrustCredentials Credentials
        {
            get { return new ATrustCredentials(user: UserId, password: Password); }
        }

        [Test]
        public void ATrustSignerWorks()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var europeTimeZone = "Central Europe Standard Time";
            var austrianTimeZone = TimeZoneInfo.FindSystemTimeZoneById(isWindows ? europeTimeZone : TZConvert.WindowsToIana(europeTimeZone));
            var austrianCulture = CultureInfo.GetCultureInfo("de-AT");
            var signer = new ATrustSigner(Credentials, ATrustEnvironment.Test);
            var info = signer.GetCertificateInfo();
            var result = signer.Sign(new QrData(new Receipt(
                    number: new ReceiptNumber("83469"),
                    registerIdentifier: new RegisterIdentifier("DEMO-CASH-BOX817"),
                    taxData: new TaxData(
                        standartRate: new CurrencyValue(29.73m),
                        lowerReducedRate: new CurrencyValue(36.41m),
                        specialRate: new CurrencyValue(21.19m)
                    ),
                    turnover: new CurrencyValue(0.0m),
                    certificateSerialNumber: new CertificateSerialNumber(info.CertificateSerialNumberHex),
                    key: AesKeyGenerator.GetNext(),
                    created: new LocalDateTime(
                        new DateTime(2015, 11, 25, 19, 20, 11),
                        austrianTimeZone
                    )
                ), austrianCulture, austrianTimeZone
            ));
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetCertificateInfoWorks()
        {
            var info = new ATrustSigner(Credentials, ATrustEnvironment.Test).GetCertificateInfo();
            Assert.IsNotNull(info);
        }
    }
}
