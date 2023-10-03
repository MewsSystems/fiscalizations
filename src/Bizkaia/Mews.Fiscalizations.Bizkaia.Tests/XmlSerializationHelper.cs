using XmlSerializer = Mews.Fiscalizations.Core.Xml.XmlSerializer;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    public static class XmlSerializationHelper<T> where T: class
    {
        public static T Deserialize(string filename)
        {
            string content = File.ReadAllText(filename);
            return XmlSerializer.Deserialize<T>(content);
        }
    }
}
