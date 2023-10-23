using System.IO.Compression;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Mews.Fiscalizations.Core.Compression
{
    public static class GzipCompressorHelper
    {
        public static byte[] Compress(this string inputString, Encoding encoding)
        {
            var bytes = encoding.GetBytes(inputString);
            using var memoryStream = new MemoryStream();
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                gzipStream.Write(bytes);
            }
            return memoryStream.ToArray();
        }

        public static string Decompress(this byte[] compressedBytes, Encoding encoding)
        {
            using var memoryStream = new MemoryStream(compressedBytes);
            using var outputStream = new MemoryStream();
            using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                decompressStream.CopyTo(outputStream);
            }
            return encoding.GetString(outputStream.ToArray());
        }
    }
}
