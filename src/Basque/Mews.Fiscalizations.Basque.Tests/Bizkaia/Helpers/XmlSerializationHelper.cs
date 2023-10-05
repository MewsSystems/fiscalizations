using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers
{
    internal static class XmlSerializationHelper
    {
        public static T Deserialize<T>(string filename)
        {
            var content = File.ReadAllText(filename);
            return XmlSerializer.Deserialize<T>(content);
        }
    }
}
