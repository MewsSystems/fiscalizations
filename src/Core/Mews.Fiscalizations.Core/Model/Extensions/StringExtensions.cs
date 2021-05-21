namespace Mews.Fiscalizations.Core.Model
{
    public static class StringExtensions
    {
        public static bool IsNotNullNorWhitespace(this string s)
        {
            return !IsNullOrWhitespace(s);
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim());
        }
    }
}
