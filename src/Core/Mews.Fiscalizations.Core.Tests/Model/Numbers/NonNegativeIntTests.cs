using System;
using FuncSharp;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalizations.Core.Tests.Model;

[TestFixture]
public sealed class NonNegativeIntTests
{
    [Test]
    [TestCase(-1, false)]
    [TestCase(0, true)]
    [TestCase(1, true)]
    public void NonNegativeIntValidatesCorrectly(int value, bool isSuccess)
    {
        Assert.AreEqual(isSuccess, NonNegativeInt.Create(value).NonEmpty);

        var exceptionConstraint = isSuccess.Match<IConstraint>(
            t => Throws.Nothing,
            f => Throws.TypeOf<ArgumentException>()
        );
        Assert.That(() => NonNegativeInt.CreateUnsafe(value), exceptionConstraint);
    }
}