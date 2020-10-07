using System;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalization.Core.Tests.Model.LimitedTypes
{
    [TestFixture]
    public sealed class NonNegativeIntTests
    {
        [Test]
        public void NegativeNotAllowed()
        {
            var negativeValue = -1;
            Assert.IsFalse(NonNegativeInt.IsValid(negativeValue));
            Assert.Throws<ArgumentException>(() => new NonNegativeInt(negativeValue));
        }

        [Test]
        public void ZeroAllowed()
        {
            var zeroValue = 0;
            Assert.IsTrue(NonNegativeInt.IsValid(zeroValue));
            new NonNegativeInt(zeroValue);
        }

        [Test]
        public void PositiveAllowed()
        {
            var positiveValue = 1;
            Assert.IsTrue(NonNegativeInt.IsValid(positiveValue));
            new NonNegativeInt(positiveValue);
        }
    }
}