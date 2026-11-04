using System.IO.Compression;

namespace Mews.Fiscalizations.Czechia.Tests;

/// <summary>
/// Shared test certificates of the EET 2.0 playground. The Financial Administration publishes them for all developers
/// at https://eet.gov.cz/cs/pro-vyvojare/dokumenty-ke-stazeni, so they are downloaded instead of being committed.
/// </summary>
public static class Playground
{
    public const string CertificatesUrl = "https://eet.gov.cz/assets/cs/cmsmedia/pro-vyvojare/CAEET_Playground_2026_v1.zip";

    public static readonly IReadOnlyList<string> TaxIds = ["CZ00000019", "CZ683555118", "CZ8551015704"];

    private static readonly Lazy<Task<IReadOnlyDictionary<string, Certificate>>> Certificates = new(DownloadCertificatesAsync);

    public static async Task<Certificate> GetCertificateAsync(string taxId)
    {
        var certificates = await Certificates.Value;
        return certificates[taxId];
    }

    private static async Task<IReadOnlyDictionary<string, Certificate>> DownloadCertificatesAsync()
    {
        using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        await using var stream = await httpClient.GetStreamAsync(CertificatesUrl);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        var password = ReadText(archive.Entries.Single(e => e.Name.StartsWith("password", StringComparison.OrdinalIgnoreCase))).Trim();
        return TaxIds.ToDictionary(
            taxId => taxId,
            taxId => Certificate.CreateUnsafe(ReadBytes(archive.Entries.Single(e => e.Name == $"CA_EET-Playground-{taxId}.p12")), password)
        );
    }

    private static string ReadText(ZipArchiveEntry entry)
    {
        using var reader = new StreamReader(entry.Open());
        return reader.ReadToEnd();
    }

    private static byte[] ReadBytes(ZipArchiveEntry entry)
    {
        using var stream = entry.Open();
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        return memory.ToArray();
    }
}
