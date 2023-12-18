namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed class SignatureXPathExpression(string xPathExpression = null, Dictionary<string, string> namespaces = null)
{
    public string XPathExpression { get; } = xPathExpression;

    public Dictionary<string, string> Namespaces { get; } = namespaces ?? [];
}