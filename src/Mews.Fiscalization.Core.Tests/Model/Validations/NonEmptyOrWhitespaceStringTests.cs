using System;
using FuncSharp;
using Mews.Fiscalization.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalization.Core.Tests.Model
{
    [TestFixture]
    public sealed class NonEmptyOrWhitespaceStringTests
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("   ", false)]
        [TestCase(".", true)]
        [TestCase("/", true)]
        [TestCase("-", true)]
        public void NonEmptyOrWhitespaceStringValidatesCorrectly(string value, bool isSuccess)
        {
            Assert.AreEqual(isSuccess, NonEmptyOrWhitespaceString.Create(value).IsSuccess);

            var exceptionConstraint = isSuccess.Match<IConstraint>(
                t => Throws.Nothing,
                f => Throws.TypeOf<ArgumentException>()
            );
            Assert.That(() => NonEmptyOrWhitespaceString.CreateUnsafe(value), exceptionConstraint);
        }
    }
}