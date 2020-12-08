using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using System;

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
        [TestCase("DE", "999999999")]
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
            var country = new EuropeanUnionCountry(countryCode);
            Assert.DoesNotThrow(() => new EuropeanUnionTaxpayerIdentificationNumber(country, taxpayerNumber));
            Assert.DoesNotThrow(() => new TaxpayerIdentificationNumber(country, taxpayerNumber));
            Assert.IsTrue(EuropeanUnionTaxpayerIdentificationNumber.IsValid(country, taxpayerNumber), $"Taxpayer number: {taxpayerNumber}, must be valid for country code {countryCode}.");
        }

        [Test]
        [TestCase("US", "123456789")]
        [TestCase("AU", "ABCD12345111")]
        public void CreatingValidNonEuropeanTaxpayerNumberSucceeds(string countryCode, string taxpayerNumber)
        {
            var country = new Country(countryCode);
            Assert.DoesNotThrow(() => new TaxpayerIdentificationNumber(country, taxpayerNumber));
            Assert.IsTrue(TaxpayerIdentificationNumber.IsValid(country, taxpayerNumber), $"Taxpayer number: {taxpayerNumber}, must be valid for country code {countryCode}.");
        }

        [Test]
        [TestCase("CZ", "ABC1234567")]
        [TestCase("CZ", "")]
        [TestCase("CZ", null)]
        [TestCase(null, "ABCD12344")]
        [TestCase(null, null)]
        public void CreatingInvalidEuropeanTaxpayerNumberFails(string countryCode, string taxpayerNumber)
        {
            var country = countryCode.IsNotNull() ? new EuropeanUnionCountry(countryCode) : null;
            Assert.IsFalse(EuropeanUnionTaxpayerIdentificationNumber.IsValid(country, taxpayerNumber), "Invalid taxpayer identification number shouldn't pass the validation.");
            Assert.That(() => new EuropeanUnionTaxpayerIdentificationNumber(country, taxpayerNumber), Throws.Exception);
            Assert.That(() => new TaxpayerIdentificationNumber(country, taxpayerNumber), Throws.Exception);
        }

        [Test]
        [TestCase("US", "")]
        [TestCase("US", null)]
        [TestCase(null, "ABCD12345")]
        [TestCase(null, null)]
        public void CreatingInvalidTaxpayerNumberFails(string countryCode, string taxpayerNumber)
        {
            var country = countryCode.IsNotNull() ? new Country(countryCode) : null;
            Assert.That(() => new TaxpayerIdentificationNumber(country, taxpayerNumber), Throws.Exception);
            Assert.IsFalse(TaxpayerIdentificationNumber.IsValid(country, taxpayerNumber), "Invalid taxpayer identification number shouldn't pass the validation.");
        }
    }
}
