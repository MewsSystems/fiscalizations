using System;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalization.Core.Tests.Model.LimitedTypes
{
    [TestFixture]
    public sealed class LimitedStringTests
    {
        [Test]
        public void TooShortStringNotAllowed()
        {
            var emptyValue = "";
            Assert.IsFalse(LimitedString1To50.IsValid(emptyValue));
            Assert.Throws<ArgumentException>(() => new LimitedString1To50(emptyValue));
        }

        [Test]
        public void TooLongStringNotAllowed()
        {
            var fiftyOneCharacterValue = "012345678901234567890123456789012345678901234567890";
            Assert.IsFalse(LimitedString1To50.IsValid(fiftyOneCharacterValue));
            Assert.Throws<ArgumentException>(() => new LimitedString1To50(fiftyOneCharacterValue));
        }

        [Test]
        public void ValidStringAllowed()
        {
            var fiftyCharacterValue = "01234567890123456789012345678901234567890123456789";
            Assert.IsTrue(LimitedString1To50.IsValid(fiftyCharacterValue));
            new LimitedString1To50(fiftyCharacterValue);
        }
    }
}