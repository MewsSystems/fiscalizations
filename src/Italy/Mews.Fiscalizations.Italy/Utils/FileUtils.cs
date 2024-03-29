namespace Mews.Fiscalizations.Italy.Utils;

public static class FileUtils
{
    public static string SanitizePath(string path)
    {
        var illegalChars = Path.GetInvalidPathChars();
        return new String(path.Where(c => !illegalChars.Contains(c)).ToArray());
    }
}