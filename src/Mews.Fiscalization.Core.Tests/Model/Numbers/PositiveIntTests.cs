using System;
using FuncSharp;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class PositiveIntTests
    {
        [Test]
        [TestCase(-1, false)]
        [TestCase(0, false)]
        [TestCase(1, true)]
        public void PositiveIntValidatesCorrectly(int value, bool isSuccess)
        {
            Assert.AreEqual(isSuccess, PositiveInt.Create(value).IsSuccess);

            var exceptionConstraint = isSuccess.Match<IConstraint>(
                t => Throws.Nothing,
                f => Throws.TypeOf<ArgumentException>()
            );
            Assert.That(() => PositiveInt.CreateUnsafe(value), exceptionConstraint);
        }
    }
}