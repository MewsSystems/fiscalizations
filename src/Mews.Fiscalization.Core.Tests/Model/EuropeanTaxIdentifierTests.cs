using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using System;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class EuropeanTaxIdentifierTests
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
        public void ValidTaxIdentifierPassesValidation(string countryCode, string taxIdentifier)
        {
            Assert.IsTrue(EuropeanTaxIdentifier.IsValid(countryCode, taxIdentifier), $"Tax identifier: {taxIdentifier}, must be valid for country code {countryCode}.");
        }

        [Test]
        public void InvalidTaxIdentifierFailsValidation()
        {
            Assert.IsFalse(EuropeanTaxIdentifier.IsValid("ES", "ABC1234567"), "Invalid tax identifier shouldn't pass the validation.");

            var invalidCountryCodeException = Assert.Throws<ArgumentException>(() => EuropeanTaxIdentifier.IsValid("INVALID", "99999"));
            Assert.AreEqual(invalidCountryCodeException.Message, "Invalid or not implemented country code.");

            var nullCountryCodeException = Assert.Throws<ArgumentException>(() => EuropeanTaxIdentifier.IsValid(null, "99999"));
            Assert.AreEqual(nullCountryCodeException.Message, "Country code cannot be null or empty.");

            var invalidTaxIdentifierException = Assert.Throws<ArgumentException>(() => EuropeanTaxIdentifier.IsValid("CZ", ""));
            Assert.AreEqual(invalidTaxIdentifierException.Message, "Tax identifier cannot be null or empty.");
        }
    }
}
