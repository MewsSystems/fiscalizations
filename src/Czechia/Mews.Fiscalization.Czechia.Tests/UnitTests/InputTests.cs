using System;
using Mews.Eet.Dto.Identifiers;
using Xunit;

namespace Mews.Eet.Tests.UnitTests
{
    public class InputTests
    {
        [Fact]
        public void TaxIdValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new TaxIdentifier("abcd"));
        }

        [Fact]
        public void RegistryIdValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new RegistryIdentifier(Guid.NewGuid().ToString()));
        }


        [Fact]
        public void BillNumberValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new BillNumber(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void BillNumberValidationIsStrict()
        {
            Assert.Throws<ArgumentException>(() => new BillNumber("@@@@"));
        }


        [Fact]
        public void PremisesIdValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new PremisesIdentifier(0));
        }

        [Fact]
        public void TaxIdWorks()
        {
            var ex = Record.Exception(() => new TaxIdentifier("CZ12345678"));
            Assert.Null(ex);
        }

        [Fact]
        public void RegistryIdWorks()
        {
            var ex = Record.Exception(() => new RegistryIdentifier("1"));
            Assert.Null(ex);
        }


        [Fact]
        public void BillNumberWorks()
        {
            var ex = Record.Exception(() => new BillNumber("2016020004"));
            Assert.Null(ex);
        }


        [Fact]
        public void PremisesIdWorks()
        {
            var ex = Record.Exception(() => new PremisesIdentifier(1));
            Assert.Null(ex);
        }

        [Fact]
        public void IdentifierToStringWorks()
        {
            var number = "1234";
            var id = new BillNumber(number);
            var str = id.ToString();
            Assert.Equal(number, str);
        }
    }
}
