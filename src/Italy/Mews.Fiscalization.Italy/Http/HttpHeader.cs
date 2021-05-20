namespace Mews.Fiscalization.Italy.Http
{
    public sealed class HttpHeader
    {
        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}
