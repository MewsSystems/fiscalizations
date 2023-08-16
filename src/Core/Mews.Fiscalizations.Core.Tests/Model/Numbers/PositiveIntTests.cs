using System;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalizations.Core.Tests.Model;

[TestFixture]
public sealed class PositiveIntTests
{
    [Test]
    [TestCase(-1, false)]
    [TestCase(0, false)]
    [TestCase(1, true)]
    public void PositiveIntValidatesCorrectly(int value, bool isSuccess)
    {
        Assert.AreEqual(isSuccess, PositiveInt.Create(value));

        var exceptionConstraint = isSuccess.Match<IConstraint>(
            t => Throws.Nothing,
            f => Throws.TypeOf<ArgumentException>()
        );
        Assert.That(() => PositiveInt.CreateUnsafe(value), exceptionConstraint);
    }
}