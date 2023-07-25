namespace Mews.Fiscalizations.Core.Xml;

public sealed class XmlNamespace
{
    public XmlNamespace(string url)
        : this("", url)
    {
    }

    public XmlNamespace(string prefix, string url)
    {
        Prefix = prefix;
        Url = url;
    }

    public string Prefix { get; }

    public string Url { get; }
}