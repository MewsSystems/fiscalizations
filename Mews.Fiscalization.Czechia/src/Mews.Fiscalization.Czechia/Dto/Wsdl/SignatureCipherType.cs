﻿using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public enum SignatureCipherType
    {
        [XmlEnum("RSA2048")]
        Rsa2048
    }
}
