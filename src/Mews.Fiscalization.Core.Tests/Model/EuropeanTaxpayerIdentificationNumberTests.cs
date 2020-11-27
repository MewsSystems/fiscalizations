using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using System;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class EuropeanTaxpayerIdentificationNumberTests
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
        public void ValidTaxpayerNumberPassesValidation(string countryCode, string taxpayerNumber)
        {
            Assert.IsTrue(EuropeanTaxpayerIdentificationNumber.IsValid(countryCode, taxpayerNumber), $"Taxpayer number: {taxpayerNumber}, must be valid for country code {countryCode}.");
        }

        [Test]
        public void InvalidTaxpayerNumberFailsValidation()
        {
            Assert.IsFalse(EuropeanTaxpayerIdentificationNumber.IsValid("ES", "ABC1234567"), "Invalid taxpayer identification number shouldn't pass the validation.");

            var invalidCountryCodeException = Assert.Throws<ArgumentException>(() => EuropeanTaxpayerIdentificationNumber.IsValid("INVALID", "99999"));
            Assert.AreEqual(invalidCountryCodeException.Message, "Country code format must be alpha-2.");

            var nullCountryCodeException = Assert.Throws<ArgumentException>(() => EuropeanTaxpayerIdentificationNumber.IsValid(null, "99999"));
            Assert.AreEqual(nullCountryCodeException.Message, "Country code cannot be null or empty.");

            var invalidTaxIdentifierException = Assert.Throws<ArgumentException>(() => EuropeanTaxpayerIdentificationNumber.IsValid("CZ", ""));
            Assert.AreEqual(invalidTaxIdentifierException.Message, "Taxpayer identification number cannot be null or empty.");
        }
    }
}
