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
            Assert.IsFalse(NonNegativeInt.Create(negativeValue).IsSuccess);
        }

        [Test]
        public void ZeroAllowed()
        {
            var zeroValue = 0;
            Assert.IsTrue(NonNegativeInt.Create(zeroValue).IsSuccess);
        }

        [Test]
        public void PositiveAllowed()
        {
            var positiveValue = 1;
            Assert.IsTrue(NonNegativeInt.Create(positiveValue).IsSuccess);
        }
    }
}