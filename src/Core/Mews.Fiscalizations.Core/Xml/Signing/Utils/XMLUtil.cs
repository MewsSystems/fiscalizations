using System.Text;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Utils;

internal static class XMLUtil
{
    public static byte[] ApplyTransform(XmlElement element, System.Security.Cryptography.Xml.Transform transform)
    {
        var buffer = Encoding.UTF8.GetBytes(element.OuterXml);
        using var ms = new MemoryStream(buffer);
        transform.LoadInput(ms);

        using var transformedStream = (MemoryStream)transform.GetOutput(typeof(Stream));
        return transformedStream.ToArray();
    }
}