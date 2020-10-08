using System;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalization.Core.Tests.Model.LimitedTypes
{
    public sealed class PositiveAmount : LimitedDecimal
    {
        private static readonly DecimalLimitation Limitation = new DecimalLimitation(min: 0, includeMin: false, maxDecimalPlaces: 2);

        public PositiveAmount(decimal value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(decimal value)
        {
            return Limitation.IsValid(value);
        }
    }

    [TestFixture]
    public sealed class LimitedDecimalTests
    {
        [Test]
        public void TooManyDecimalPlacesNotAllowed()
        {
            var value = 1.102m;
            Assert.IsFalse(PositiveAmount.IsValid(value));
            Assert.Throws<ArgumentException>(() => new PositiveAmount(value));
        }

        [Test]
        public void BoundaryValueNotAllowed()
        {
            var zeroValue = 0m;
            Assert.IsFalse(PositiveAmount.IsValid(zeroValue));
            Assert.Throws<ArgumentException>(() => new PositiveAmount(zeroValue));
        }
    }
}