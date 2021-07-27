namespace Mews.Fiscalizations.Italy.Errors
{
    public class UniwixValidationException : UniwixException
    {
        public string Xml { get; }

        public UniwixValidationException(int code, string reason, string xml)
            : base(code, reason)
        {
            Xml = xml;
        }
    }
}