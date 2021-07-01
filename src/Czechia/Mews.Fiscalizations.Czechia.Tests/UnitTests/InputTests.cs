using System;
using Mews.Eet.Dto.Identifiers;
using NUnit.Framework;

namespace Mews.Eet.Tests.UnitTests
{
    public class InputTests
    {
        [Test]
        public void TaxIdValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new TaxIdentifier("abcd"));
        }

        [Test]
        public void RegistryIdValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new RegistryIdentifier(Guid.NewGuid().ToString()));
        }


        [Test]
        public void BillNumberValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new BillNumber(Guid.NewGuid().ToString()));
        }

        [Test]
        public void BillNumberValidationIsStrict()
        {
            Assert.Throws<ArgumentException>(() => new BillNumber("@@@@"));
        }


        [Test]
        public void PremisesIdValidationWorks()
        {
            Assert.Throws<ArgumentException>(() => new PremisesIdentifier(0));
        }

        [Test]
        public void TaxIdWorks()
        {
            Assert.DoesNotThrow(() => new TaxIdentifier("CZ12345678"));
        }

        [Test]
        public void RegistryIdWorks()
        {
            Assert.DoesNotThrow(() => new RegistryIdentifier("1"));
        }


        [Test]
        public void BillNumberWorks()
        {
            Assert.DoesNotThrow(() => new BillNumber("2016020004"));
        }


        [Test]
        public void PremisesIdWorks()
        {
            Assert.DoesNotThrow(() => new PremisesIdentifier(1));
        }

        [Test]
        public void IdentifierToStringWorks()
        {
            var number = "1234";
            var id = new BillNumber(number);
            var str = id.ToString();
            Assert.AreEqual(number, str);
        }
    }
}
