using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using System;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class CountryTests
    {
        [Test]
        public void CreatingCountryWithInvalidCountryCodeFails()
        {
            Assert.Throws<ArgumentException>(() => new EuropeanCountry("US"));
            Assert.Throws<ArgumentException>(() => new Country(""));
            Assert.Throws<ArgumentNullException>(() => new Country(null));
        }

        [Test]
        public void CreatingCountryWithValidCountryCodeSucceeds()
        {
            Assert.DoesNotThrow(() => new EuropeanCountry("CZ"));
            Assert.DoesNotThrow(() => new Country("CZ"));
            Assert.DoesNotThrow(() => new Country("US"));
            Assert.IsTrue(EuropeanCountry.IsValid("CZ"));
            Assert.IsTrue(Country.IsValid("CZ"));
            Assert.IsTrue(Country.IsValid("US"));
        }
    }
}
