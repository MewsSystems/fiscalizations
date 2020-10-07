using System;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalization.Core.Tests.Model.LimitedTypes
{
    [TestFixture]
    public sealed class NonEmptyStringTests
    {
        [Test]
        public void EmptyStringNotAllowed()
        {
            var value = "";
            Assert.IsFalse(NonEmptyString.IsValid(value));
            Assert.Throws<ArgumentException>(() => new NonEmptyString(value));
        }

        [Test]
        public void NullNotAllowed()
        {
            var value = (string)null;
            Assert.IsFalse(NonEmptyString.IsValid(value));
            Assert.Throws<ArgumentNullException>(() => new NonEmptyString(value));
        }

        [Test]
        public void NonEmptyStringAllowed()
        {
            var value = "A";
            Assert.IsTrue(NonEmptyString.IsValid(value));
            new NonEmptyString(value);
        }
    }
}