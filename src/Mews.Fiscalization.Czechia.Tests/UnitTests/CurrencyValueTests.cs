using System;
using Mews.Eet.Dto;
using Mews.Eet.Extensions;
using Xunit;

namespace Mews.Eet.Tests.UnitTests
{
    public class CurrencyValueTests
    {
        [Fact]
        public void IsDefinedWorks()
        {
            Assert.False((null as CurrencyValue).IsDefined());
            Assert.True(new CurrencyValue(100.00m).IsDefined());
        }

        [Fact]
        public void PrecisionIncreaseWorks()
        {
            var ex = Record.Exception(() => new CurrencyValue(100m));
            var ex1 = Record.Exception(() => new CurrencyValue(100.0m));
            Assert.Null(ex);
            Assert.Null(ex1);
        }

        [Fact]
        public void RangeCheckWorks()
        {
            Assert.Throws<ArgumentException>(() => new CurrencyValue(555555555555555555555555555.00m));
            Assert.Throws<ArgumentException>(() => new CurrencyValue(-555555555555555555555555555.00m));
        }
    }
}