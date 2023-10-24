using System.IO.Compression;
using System.Text;

namespace Mews.Fiscalizations.Core.Compression;

public static class GzipCompressorHelper
{
    public static async Task<byte[]> CompressAsync(this string inputString, Encoding encoding, CancellationToken cancellationToken)
    {
        var bytes = encoding.GetBytes(inputString);
        using var memoryStream = new MemoryStream();
        await using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
        {
            await gzipStream.WriteAsync(bytes, cancellationToken);
        }
        return memoryStream.ToArray();
    }

    public static async Task<string> DecompressAsync(this byte[] compressedBytes, Encoding encoding, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream(compressedBytes);
        using var outputStream = new MemoryStream();
        await using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
        {
            await decompressStream.CopyToAsync(outputStream, cancellationToken);
        }
        return encoding.GetString(outputStream.ToArray());
    }
}