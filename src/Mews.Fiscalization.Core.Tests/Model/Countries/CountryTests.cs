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
            Assert.IsTrue(EuropeanUnionCountry.GetByCode(countryCode).IsEmpty);
        }

        [Test]
        [TestCase("invalid")]
        [TestCase("")]
        [TestCase(null)]
        public void CreatingCountryWithInvalidCountryCodeFails(string countryCode)
        {
            Assert.IsTrue(Country.GetByCode(countryCode).IsEmpty);
        }

        [Test]
        [TestCase("CZ")]
        [TestCase("PL")]
        public void CreatingEuropeanCountryWithValidCountryCodeSucceeds(string countryCode)
        {
            Assert.IsTrue(Country.GetByCode(countryCode).NonEmpty);
            Assert.IsTrue(EuropeanUnionCountry.GetByCode(countryCode).NonEmpty);
        }

        [Test]
        [TestCase("US")]
        [TestCase("AU")]
        public void CreatingCountryWithValidCountryCodeSucceeds(string countryCode)
        {
            Assert.IsTrue(Country.GetByCode(countryCode).NonEmpty);
        }
    }
}
