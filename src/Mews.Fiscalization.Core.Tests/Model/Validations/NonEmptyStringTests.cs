using System;
using FuncSharp;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class NonEmptyStringTests
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("   ", true)]
        [TestCase(".", true)]
        [TestCase("/", true)]
        [TestCase("-", true)]
        public void NonEmptyStringValidatesCorrectly(string value, bool isSuccess)
        {
            Assert.AreEqual(isSuccess, NonEmptyString.Create(value).IsSuccess);

            var exceptionConstraint = isSuccess.Match<IConstraint>(
                t => Throws.Nothing,
                f => Throws.TypeOf<ArgumentException>()
            );
            Assert.That(() => NonEmptyString.CreateUnsafe(value), exceptionConstraint);
        }
    }
}