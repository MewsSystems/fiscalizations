namespace Mews.Fiscalizations.Core.Tests.Model;

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
        Assert.That(EuropeanUnionCountry.GetByCode(countryCode).IsEmpty);
    }

    [Test]
    [TestCase("invalid")]
    [TestCase("")]
    [TestCase(null)]
    public void CountryWithInvalidCountryCodeNotFound(string countryCode)
    {
        Assert.That(Country.GetByCode(countryCode).IsEmpty);
    }

    [Test]
    [TestCase("CZ")]
    [TestCase("PL")]
    public void EuropeanCountryWithValidCountryCodeFound(string countryCode)
    {
        Assert.That(Country.GetByCode(countryCode).NonEmpty);
        Assert.That(EuropeanUnionCountry.GetByCode(countryCode).NonEmpty);
    }

    [Test]
    [TestCase("US")]
    [TestCase("AU")]
    public void CountryWithValidCountryCodeFound(string countryCode)
    {
        Assert.That(Country.GetByCode(countryCode).NonEmpty);
    }
}