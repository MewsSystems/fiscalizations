﻿using Mews.Fiscalizations.Core.Compression;

namespace Mews.Fiscalizations.Core.Tests.Compression;

[TestFixture]
public class GzipCompressionTests
{
    private const string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

    [Test]
    public void GzipCompression_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            string compressedString = GzipCompressionHelper.Compress(
                inputString: Message
                );

            string decompressedString = GzipCompressionHelper.Decompress(
                compressedString: compressedString,
                encoding: System.Text.Encoding.ASCII
                );

            Assert.AreEqual(Message, decompressedString);
        });
    }
}
