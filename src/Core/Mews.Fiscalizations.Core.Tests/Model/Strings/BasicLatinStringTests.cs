namespace Mews.Fiscalizations.Core.Tests.Model;

[TestFixture]
public sealed class BasicLatinStringTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("abcdefghijklmnopqrstuvwxyz0123456789", "abcdefghijklmnopqrstuvwxyz0123456789")]
    [TestCase("!@#$%^&**()_-", "!@#$%^&**()_-")]
    [TestCase("Euro € sign", "Euro  sign")]
    [TestCase("€€€", "")]
    public void ToBasicLatinStringSucceeds(string input, string expectedOutput)
    {
        Assert.AreEqual(expectedOutput, input.ToBasicLatin());
    }
}