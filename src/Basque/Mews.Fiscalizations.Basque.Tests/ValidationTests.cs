namespace Mews.Fiscalizations.Basque.Tests;

[TestFixture]
public sealed class ValidationTests
{
    [Test]
    [TestCase("12345", true)]
    [TestCase("ABCDEFG", true)]
    [TestCase("ABCDEF123456", true)]
    [TestCase("12345@", false)]
    [TestCase("123456789123456789102", false)]
    [TestCase("", false)]
    [TestCase(" ", false)]
    public void PostalCodeTests(string postalCode, bool isValid)
    {
        Assert.That(PostalCode.Create(postalCode).IsSuccess, Is.EqualTo(isValid));
    }
}