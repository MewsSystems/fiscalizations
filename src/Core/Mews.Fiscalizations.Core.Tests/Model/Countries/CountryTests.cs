using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalizations.Core.Tests.Model
{
    [TestFixture]
    public sealed class CountryTests
    {
        [Test]
        [TestCase("US")]
        [TestCase("invalid")]
        [TestCase("")]
        [TestCase(null)]
        public void EuropeanCountryWithInvalidCountryCodeNotFound(string countryCode)
        {
            Assert.IsTrue(EuropeanUnionCountry.GetByCode(countryCode).IsEmpty);
        }

        [Test]
        [TestCase("invalid")]
        [TestCase("")]
        [TestCase(null)]
        public void CountryWithInvalidCountryCodeNotFound(string countryCode)
        {
            Assert.IsTrue(Country.GetByCode(countryCode).IsEmpty);
        }

        [Test]
        [TestCase("CZ")]
        [TestCase("PL")]
        public void EuropeanCountryWithValidCountryCodeFound(string countryCode)
        {
            Assert.IsTrue(Country.GetByCode(countryCode).NonEmpty);
            Assert.IsTrue(EuropeanUnionCountry.GetByCode(countryCode).NonEmpty);
        }

        [Test]
        [TestCase("US")]
        [TestCase("AU")]
        public void CountryWithValidCountryCodeFound(string countryCode)
        {
            Assert.IsTrue(Country.GetByCode(countryCode).NonEmpty);
        }
    }
}
