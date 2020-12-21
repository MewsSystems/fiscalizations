using Mews.Fiscalization.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class TaxpayerIdentificationNumberTests
    {
        [Test]
        [TestCase("AT", "U99999999")]
        [TestCase("BE", "0999999999")]
        [TestCase("BG", "999999999")]
        [TestCase("CY", "99999999L")]
        [TestCase("CZ", "99999999")]
        [TestCase("DE", "999999999A")]
        [TestCase("DK", "99999999")]
        [TestCase("EE", "999999999")]
        [TestCase("GR", "999999999")]
        [TestCase("ES", "X9999999X")]
        [TestCase("FI", "99999999")]
        [TestCase("FR", "XX999999999")]
        [TestCase("GB", "999999999")]
        [TestCase("HU", "99999999")]
        [TestCase("IE", "9S99999L")]
        [TestCase("IT", "99999999999")]
        [TestCase("LT", "999999999")]
        [TestCase("LU", "99999999")]
        [TestCase("LV", "99999999999")]
        [TestCase("MT", "99999999")]
        [TestCase("NL", "999999999B99")]
        [TestCase("PL", "9999999999")]
        [TestCase("PT", "999999999")]
        [TestCase("RO", "99")]
        [TestCase("SE", "999999999999")]
        [TestCase("SI", "99999999")]
        [TestCase("SK", "9999999999")]
        public void CreatingValidEuropeanTaxpayerNumberSucceeds(string countryCode, string taxpayerNumber)
        {
            var country = Country.GetByCode(countryCode).Get();
            var europeanCountry = EuropeanUnionCountry.GetByCode(countryCode).Get();
            Assert.IsTrue(EuropeanUnionTaxpayerIdentificationNumber.Create(europeanCountry, taxpayerNumber).IsSuccess);
            Assert.IsTrue(TaxpayerIdentificationNumber.Create(country, taxpayerNumber).IsSuccess);
        }

        [Test]
        [TestCase("US", "123456789")]
        [TestCase("AU", "ABCD12345111")]
        public void CreatingValidNonEuropeanTaxpayerNumberSucceeds(string countryCode, string taxpayerNumber)
        {
            var country = Country.GetByCode(countryCode).Get();
            Assert.IsTrue(TaxpayerIdentificationNumber.Create(country, taxpayerNumber).IsSuccess);
        }

        [Test]
        [TestCase("CZ", "ABC1234567")]
        [TestCase("CZ", "")]
        [TestCase("CZ", null)]
        [TestCase(null, "ABCD12344")]
        [TestCase(null, null)]
        public void CreatingInvalidEuropeanTaxpayerNumberFails(string countryCode, string taxpayerNumber)
        {
            var country = countryCode.IsNotNull() ? Country.GetByCode(countryCode).Get() : null;
            var europeanCountry = countryCode.IsNotNull() ? EuropeanUnionCountry.GetByCode(countryCode).Get() : null;
            Assert.IsTrue(EuropeanUnionTaxpayerIdentificationNumber.Create(europeanCountry, taxpayerNumber).IsError);
            Assert.IsTrue(TaxpayerIdentificationNumber.Create(country, taxpayerNumber).IsError);
        }

        [Test]
        [TestCase("US", "")]
        [TestCase("US", null)]
        [TestCase(null, "ABCD12345")]
        [TestCase(null, null)]
        public void CreatingInvalidTaxpayerNumberFails(string countryCode, string taxpayerNumber)
        {
            var country = countryCode.IsNotNull() ? Country.GetByCode(countryCode).Get() : null;
            Assert.IsTrue(TaxpayerIdentificationNumber.Create(country, taxpayerNumber).IsError);
        }
    }
}
