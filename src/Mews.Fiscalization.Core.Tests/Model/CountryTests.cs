using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using System;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class CountryTests
    {
        [Test]
        [TestCase("US")]
        [TestCase("invalid")]
        [TestCase("")]
        [TestCase(null)]
        public void CreatingEuropeanCountryWithInvalidCountryCodeFails(string countryCode)
        {
            Assert.That(() => new EuropeanUnionCountry(countryCode), Throws.Exception);
        }

        [Test]
        [TestCase("invalid")]
        [TestCase("")]
        [TestCase(null)]
        public void CreatingCountryWithInvalidCountryCodeFails(string countryCode)
        {
            Assert.That(() => new Country(countryCode), Throws.Exception);
        }

        [Test]
        [TestCase("CZ")]
        [TestCase("PL")]
        public void CreatingEuropeanCountryWithValidCountryCodeSucceeds(string countryCode)
        {
            Assert.DoesNotThrow(() => new EuropeanUnionCountry(countryCode));
            Assert.DoesNotThrow(() => new Country(countryCode));
            Assert.IsTrue(EuropeanUnionCountry.IsValid(countryCode));
            Assert.IsTrue(Country.IsValid(countryCode));
        }

        [Test]
        [TestCase("US")]
        [TestCase("AU")]
        public void CreatingCountryWithValidCountryCodeSucceeds(string countryCode)
        {
            Assert.DoesNotThrow(() => new Country(countryCode));
            Assert.IsTrue(Country.IsValid(countryCode));
        }
    }
}
