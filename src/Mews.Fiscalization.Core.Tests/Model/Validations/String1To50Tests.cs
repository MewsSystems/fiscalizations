using System;
using FuncSharp;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class String1To50Tests
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("   ", true)]
        [TestCase("01234567890123456789012345678901234567890123456789", true)]
        [TestCase("012345678901234567890123456789012345678901234567890", false)]
        public void NonNegativeIntValidatesCorrectly(string value, bool isSuccess)
        {
            Assert.AreEqual(isSuccess, String1To50.Create(value).IsSuccess);

            var exceptionConstraint = isSuccess.Match<IConstraint>(
                t => Throws.Nothing,
                f => Throws.TypeOf<ArgumentException>()
            );
            Assert.That(() => NonEmptyString.CreateUnsafe(value), exceptionConstraint);
        }
    }
}