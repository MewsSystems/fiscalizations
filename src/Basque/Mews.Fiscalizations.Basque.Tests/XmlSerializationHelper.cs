﻿using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Basque.Tests
{
    public static class XmlSerializationHelper
    {
        public static T Deserialize<T>(string filename)
        {
            string content = File.ReadAllText(filename);
            return XmlSerializer.Deserialize<T>(content);
        }
    }
}
