using FuncSharp;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Core.Xml
{
    public sealed class XmlSerializationParameters
    {
        public XmlSerializationParameters(Encoding encoding = null, IEnumerable <XmlNamespace> namespaces = null)
        {
            Encoding = encoding ?? Encoding.UTF8;
            Namespaces = namespaces.ToOption();
        }

        public Encoding Encoding { get; }

        public IOption<IEnumerable<XmlNamespace>> Namespaces { get; }
    }
}
