using System;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalization.Core.Tests.Model.LimitedTypes
{
    public sealed class NonNegativeAmount : LimitedDecimal
    {
        private static readonly DecimalLimitation Limitation = new DecimalLimitation(min: 0, maxDecimalPlaces: 2);

        public NonNegativeAmount(decimal value)
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
            Assert.IsFalse(NonNegativeAmount.IsValid(value));
            Assert.Throws<ArgumentException>(() => new NonNegativeAmount(value));
        }
    }
}