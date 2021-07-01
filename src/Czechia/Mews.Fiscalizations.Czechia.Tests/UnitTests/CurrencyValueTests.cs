using System;
using Mews.Eet.Dto;
using Mews.Eet.Extensions;
using NUnit.Framework;

namespace Mews.Eet.Tests.UnitTests
{
    public class CurrencyValueTests
    {
        [Test]
        public void IsDefinedWorks()
        {
            Assert.False((null as CurrencyValue).IsDefined());
            Assert.True(new CurrencyValue(100.00m).IsDefined());
        }

        [Test]
        public void PrecisionIncreaseWorks()
        {
            Assert.DoesNotThrow(() => new CurrencyValue(100m));
            Assert.DoesNotThrow(() => new CurrencyValue(100.0m));
        }

        [Test]
        public void RangeCheckWorks()
        {
            Assert.Throws<ArgumentException>(() => new CurrencyValue(555555555555555555555555555.00m));
            Assert.Throws<ArgumentException>(() => new CurrencyValue(-555555555555555555555555555.00m));
        }
    }
}