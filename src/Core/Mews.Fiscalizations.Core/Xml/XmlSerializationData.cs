using FuncSharp;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Core.Xml
{
    public sealed class XmlSerializationData
    {
        public XmlSerializationData(IEnumerable<XmlNamespace> namespaces = null)
            : this(Encoding.UTF8, namespaces)
        {
        }

        public XmlSerializationData(Encoding encoding, IEnumerable<XmlNamespace> namespaces = null)
        {
            Encoding = encoding;
            Namespaces = namespaces.ToOption();
        }

        public Encoding Encoding { get; }

        public IOption<IEnumerable<XmlNamespace>> Namespaces { get; }
    }
}
