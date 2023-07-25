using System;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalizations.Core.Tests.Model;

[TestFixture]
public sealed class NonEmptyNorWhitespaceStringTests
{
    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase("   ", false)]
    [TestCase(".", true)]
    [TestCase("/", true)]
    [TestCase("-", true)]
    [TestCase("ASDF", true)]
    public void NonEmptyNorWhitespaceStringValidatesCorrectly(string value, bool isSuccess)
    {
        Assert.AreEqual(isSuccess, NonEmptyNorWhitespaceString.Create(value).IsSuccess);

        var exceptionConstraint = isSuccess.Match<IConstraint>(
            t => Throws.Nothing,
            f => Throws.TypeOf<ArgumentException>()
        );
        Assert.That(() => NonEmptyNorWhitespaceString.CreateUnsafe(value), exceptionConstraint);
    }
}