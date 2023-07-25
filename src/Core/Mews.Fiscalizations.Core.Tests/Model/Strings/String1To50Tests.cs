using System;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalizations.Core.Tests.Model;

[TestFixture]
public sealed class String1To50Tests
{
    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase("   ", true)]
    [TestCase("12345678901234567890123456789012345678901234567890", true)]
    [TestCase("123456789012345678901234567890123456789012345678901", false)]
    public void String1To50ValidatesCorrectly(string value, bool isSuccess)
    {
        Assert.AreEqual(isSuccess, String1To50.Create(value).IsSuccess);

        var exceptionConstraint = isSuccess.Match<IConstraint>(
            t => Throws.Nothing,
            f => Throws.TypeOf<ArgumentException>()
        );
        Assert.That(() => String1To50.CreateUnsafe(value), exceptionConstraint);
    }
}