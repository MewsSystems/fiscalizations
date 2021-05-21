using System;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Mews.Fiscalizations.Core.Tests.Model
{
    [TestFixture]
    public sealed class NonEmptyStringTests
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("   ", true)]
        [TestCase(".", true)]
        [TestCase("ASDF", true)]
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