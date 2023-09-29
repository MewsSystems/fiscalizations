using System.Reflection.Metadata.Ecma335;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    internal static class TicketBaiInvoiceHelper
    {
        internal static TicketBai CreateSampleTbaiInvoice()
        {
            TicketBai ticketBai = new TicketBai();

            ticketBai.Cabecera = CreateHeader();
            ticketBai.Sujetos = CreateSubjects();
            ticketBai.Factura = CreateInvoice();
            ticketBai.HuellaTBAI = CreateFingerprint();
            ticketBai.Signature = CreateSignature();

            return ticketBai;
        }

        private static Cabecera1 CreateHeader()
        {
            return new Cabecera1
            {
                IDVersionTBAI = IDVersionTicketBaiType1.Item12
            };

        }

        private static Sujetos CreateSubjects()
        {
            return new Sujetos
            {
                Emisor = new Emisor1
                {
                    NIF = "B00000034",
                    ApellidosNombreRazonSocial = "HOTEL ADIBIDEZ"
                },
                Destinatarios = new IDDestinatario[] 
                {
                    new IDDestinatario 
                    {
                        NIF = "B26248146",
                        ApellidosNombreRazonSocial = "EMPRESA LANTEGIA"
                    }
                }
            };

        }

        private static Factura CreateInvoice()
        {
            return new Factura 
            {
                CabeceraFactura = new CabeceraFacturaType1
                {
                    SerieFactura = "B2022",
                    NumFactura = "0100",
                    FechaExpedicionFactura = "30-01-2022",
                    HoraExpedicionFactura = "18:00:17"
                },
                DatosFactura = new DatosFacturaType
                {
                    DescripcionFactura = "Servicios Hotel",
                    ImporteTotalFactura = "2343.00",
                    Claves = new IDClaveType[] 
                    {
                        new IDClaveType 
                        {
                            ClaveRegimenIvaOpTrascendencia = IdOperacionesTrascendenciaTributariaType.Item01
                        }
                    }
                },
                TipoDesglose = new TipoDesgloseType
                {
                    DesgloseFactura = new DesgloseFacturaType
                    {
                        Sujeta = new SujetaType
                        {
                            NoExenta = new DetalleNoExentaType[]
                            {
                                new DetalleNoExentaType
                                {
                                    DesgloseIVA = new DetalleIVAType[]
                                    {
                                        new DetalleIVAType
                                        {
                                            BaseImponible = "300.00",
                                            TipoImpositivo = "21.00",
                                            CuotaImpuesto = "63.00"
                                        },
                                        new DetalleIVAType 
                                        {
                                            BaseImponible = "1800.00",
                                            TipoImpositivo = "10.00",
                                            CuotaImpuesto = "180.00"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private static HuellaTBAI CreateFingerprint()
        {
            return new HuellaTBAI
            {
                EncadenamientoFacturaAnterior = new EncadenamientoFacturaAnteriorType
                {
                    SerieFacturaAnterior = "B2022",
                    NumFacturaAnterior = "0099",
                    FechaExpedicionFacturaAnterior = "29-01-2022",
                    SignatureValueFirmaFacturaAnterior = "BeMkKwXaFsxHQec65SKpVP7EU9o4nUXOx7SAftIToFsxH+2j2tXPXhpBUnS26dhdSpiMl2DlTuqRsFdZfWyYazaGHgSRQHZZAnFt"
                },
                Software = new SoftwareFacturacionType
                {
                    LicenciaTBAI = "TBAIPRUEBA",
                    EntidadDesarrolladora = new EntidadDesarrolladoraType
                    {
                        Item = "A48119820"
                    },
                    Nombre = "DFBTBAI",
                    Version = "1.04.00"
                },
                NumSerieDispositivo = "GP4FC5J"
            };

        }

        private static Signature CreateSignature()
        {
            return new Signature
            {
                Id = "Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-Signature",
                SignedInfo = CreateSignedInfo(),
                SignatureValue = CreateSignatureValue(),
                KeyInfo = CreateKeyInfo(),
                Object = CreateSignatureObject()
            };
        }

        private static ObjectType[] CreateSignatureObject()
        {
            return new ObjectType[]
            {
                new ObjectType
                {
                    QualifyingProperties = new QualifyingProperties
                    {
                        Id = "Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-QualifyingProperties",
                        Target = "#Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-Signature",
                        SignedProperties = CreateSignedProperties(),
                    }
                }
            };
        }

        private static SignedPropertiesType CreateSignedProperties()
        {
            return new SignedPropertiesType
            {
                Id = "Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-SignedProperties",
                SignedSignatureProperties = CreateSignedSignatureProperties(),
                SignedDataObjectProperties = CreateDataObjectProperties()
            };
        }

        private static SignedDataObjectPropertiesType CreateDataObjectProperties()
        {
            return new SignedDataObjectPropertiesType
            {
                DataObjectFormat = new DataObjectFormatType
                {
                    ObjectReference = "#Reference-cf54e6a9-7bf2-4128-8cae-474a267a16a1",
                    ObjectIdentifier = new ObjectIdentifierType
                    {
                        Identifier = new IdentifierType
                        {
                            Qualifier = "OIDAsURN",
                            Value = "urn:oid:1.2.840.10003.5.109.10"
                        }
                    },
                    MimeType = "text/xml"
                }
            };
        }

        private static SignedSignaturePropertiesType CreateSignedSignatureProperties()
        {
            return new SignedSignaturePropertiesType
            {
                SigningTime = "2020-10-02T14:54:50+02:00",
                SigningCertificate = new SigningCertificateType
                {
                    Cert = new CertType
                    {
                        CertDigest = new CertDigestType
                        {
                            DigestMethod = new DigestMethodType
                            {
                                Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                            },
                            DigestValue = "+iJollIf11D+A9/mAzOUM6SSJvFPOneoOn7NIKf+NqkpcE7VUMx4xiGjw0D8JNrfrexJwxmlMTWd3Eg/d3Bq2Q=="
                        }, 
                        IssuerSerial = new IssuerSerialType
                        {
                            X509IssuerName = "CN=CA AAPP Vascas (2) - DESARROLLO, OU=AZZ Ziurtagiri publikoa - Certificado publico SCA, O=IZENPE S.A., C=ES",
                            X509SerialNumber = "56643058864757982732206463601082748842"
                        }
                    }
                },
                SignaturePolicyIdentifier = new SignaturePolicyIdentifierType
                {
                    SignaturePolicyId = new SignaturePolicyIdType
                    {
                        SigPolicyId = new SigPolicyIdType
                        {
                            Identifier = "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_0.pdf",
                        },
                        SigPolicyHash = new SigPolicyHashType
                        {
                            DigestMethod = new DigestMethodType
                            {
                                Algorithm = "http://www.w3.org/2001/04/xmlenc#sha256"
                            },
                            DigestValue = "Quzn98x3PMbSHwbUzaj5f5KOpiH0u8bvmwbbbNkO9Es="
                        },
                        SigPolicyQualifiers = new SigPolicyQualifiersType
                        {
                            SigPolicyQualifier = new SigPolicyQualifierType
                            {
                                SPURI = "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_0.pdf"
                            }
                        }
                    }
                }
            };
        }

        private static KeyInfoType CreateKeyInfo()
        {
            return new KeyInfoType
            {
                Id = "Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-KeyInfo",
                KeyValue = new KeyValueType
                {
                    RSAKeyValue = new RSAKeyValueType
                    {
                        Modulus = "uX1Cod1XbgABYf/Vyv8x5HoaJp5jn4qcAIvGaXbTL+MVkTAw1uWMCbrwPpAg8aUWvdOYrDk+sZ1P7Egw/LmDp+v0VhtUln9LfGhKpNJGHwr0E6IW9W1HnCTSmVrmdqKUc4Rj8ys7P0dMo22iCO0bBHJMs55A7BQrBI2urKjmF/ele7tqL2T3zMoRLwG9KAbmyDcKarUDnp5ghdyFQtFFzsE+LeYoxlrH0UHhtdkqzS35IKn3sAxNyB3QwESywEQZMRCubYVAfES1UhIrJLy7WrP/3VUt0IL3iSkYdILzLczGQ5kNzwpooSjZiJdabWO4vo9ekhC3YQuSUFIYQuo0iQ==",
                        Exponent = "AQAB"
                    }
                }
            };
        }

        private static SignatureValueType CreateSignatureValue()
        {
            return new SignatureValueType
            {
                Id = "Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-SignatureValue",
                Value = "DSI1XA1ErCoqaEln++216eTQpN0qF7HQb/I3QhF54V11LEGY3ucMVH33GceRE9LtYJI7AoI0YS3q0qPOn+eTlH6ZHVXXkcVZyi2gBZcuCKqRiRzSiuS0MazDgYqBdrDWhAIByP6AJ2vRhxhZceACeHFNzfmjeDpjX01cUox/RFT//C4cUOuTWADlkq/PqEN0dK/UQbZEOssk4diIlCeYU/exVYoRIpkE9OcFrlwOJv/UJ3ezBlL9pd/6G5rhNMtwqnPXLkbBq993uuVCU2SeKi79HuAa/gCvHlFwrjhwJV6VgcGhPylJ67xJk6dq+ukSW2s6kI4OgauH4SzYUaICAQ=="
            };
        }

        private static SignedInfoType CreateSignedInfo()
        {
            return new SignedInfoType
            {
                CanonicalizationMethod = new CanonicalizationMethodType
                {
                    Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315"
                },
                SignatureMethod = new SignatureMethodType
                {
                    Algorithm = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"
                },
                Reference = new ReferenceType[]
                {
                    new ReferenceType
                    {
                        Id = "Reference-cf54e6a9-7bf2-4128-8cae-474a267a16a1",
                        Type = "http://www.w3.org/2000/09/xmldsig#Object",
                        URI = "",
                        Transforms = new TransformType[]
                        {
                            new TransformType
                            {
                                Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315"
                            },
                            new TransformType
                            {
                                Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature"
                            },
                            new TransformType
                            {
                                Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116",
                                Items = new string[]
                                {
                                    "not(ancestor-or-self::ds:Signature)"
                                },
                                ItemsElementName = new ItemsChoiceType[]
                                {
                                    ItemsChoiceType.XPath
                                }
                            }
                        },
                        DigestMethod = new DigestMethodType
                        {
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = "iU0b5R4S1WQlJgzMUW74XmUjDxuDmSUvGQUfVgDrNvkqoRTnW3tgbYJhYK0PHTeRiFBMEl1zd9vqwnK9O00r5A=="
                    },
                    new ReferenceType
                    {
                        Type = "http://uri.etsi.org/01903#SignedProperties",
                        URI = "#Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-SignedProperties",
                        DigestMethod = new DigestMethodType
                        {
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = "+tVpModLD/ECct8v0HCxYz+UuAP5QU1GfiPhQU85Sgnscnj3OuLzLg74C/8G9EtS9qzaBbyyr3eTx/zu5/Shrw=="
                    },
                    new ReferenceType
                    {
                        URI = "#Signature-a53a6ab2-f904-4f7c-be64-603333f651bf-KeyInfo",
                        DigestMethod = new DigestMethodType
                        {
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = "9PwT2aXW6Wa81pwlZmQjvuJzy3a/HJ6gUuaJGJGB0ZQGpMsvYuJ5imlZgNtgldG9EvX0CJioRs9KIia3WyF6ig=="
                    }
                }
            };
        }
    }
}
