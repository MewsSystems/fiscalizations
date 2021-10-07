using Mews.Fiscalizations.Germany.V2.Model;

namespace Mews.Fiscalizations.Germany.Tests.V2
{
    public static class TestFixture
    {
        public static readonly ApiKey ApiKey = ApiKey.Create("test_6l2lccg6diqc3ji7r7oindcou_tat").Success.Get();
        public static readonly ApiSecret ApiSecret = ApiSecret.Create("tmAZNmiyQPZVLyucOe6lD9F0oFJSSLrZ5d35apqDUaW").Success.Get();
        public static readonly string AdminPin = "5843764324";
        public static FiskalyTestData FiskalyTestData;
    }
}