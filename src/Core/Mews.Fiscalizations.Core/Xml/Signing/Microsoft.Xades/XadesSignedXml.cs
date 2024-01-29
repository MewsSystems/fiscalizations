using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Schema;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// Types of signature standards that can be contained in XadesSignedXml class instance
/// </summary>
internal enum KnownSignatureStandard
{
    /// <summary>
    /// XML Digital Signature (XMLDSIG)
    /// </summary>
    XmlDsig,
    /// <summary>
    /// XML Advanced Electronic Signature (XAdES) 
    /// </summary>
    Xades
}

/// <summary>
/// Bitmasks to indicate which checks need to be executed on the XAdES signature
/// </summary>
[Flags]
public enum XadesCheckSignatureMasks : ulong
{
    /// <summary>
    /// Check the signature of the underlying XMLDSIG signature
    /// </summary>
    CheckXmldsigSignature = 0x01,
    /// <summary>
    /// Validate the XML representation of the signature against the XAdES and XMLDSIG schemas
    /// </summary>
    ValidateAgainstSchema = 0x02,
    /// <summary>
    /// Check to see if first XMLDSIG certificate has same hashvalue as first XAdES SignatureCertificate
    /// </summary>
    CheckSameCertificate = 0x04,
    /// <summary>
    /// Check if there is a HashDataInfo for each reference if there is a AllDataObjectsTimeStamp
    /// </summary>
    CheckAllReferencesExistInAllDataObjectsTimeStamp = 0x08,
    /// <summary>
    /// Check if the HashDataInfo of each IndividualDataObjectsTimeStamp points to existing Reference
    /// </summary>
    CheckAllHashDataInfosInIndividualDataObjectsTimeStamp = 0x10,
    /// <summary>
    /// Perform XAdES checks on contained counter signatures 
    /// </summary>
    CheckCounterSignatures = 0x20,
    /// <summary>
    /// Counter signatures should all contain a reference to the parent signature SignatureValue element
    /// </summary>
    CheckCounterSignaturesReference = 0x40,
    /// <summary>
    /// Check if each ObjectReference in CommitmentTypeIndication points to Reference element
    /// </summary>
    CheckObjectReferencesInCommitmentTypeIndication = 0x80,
    /// <summary>
    /// Check if at least ClaimedRoles or CertifiedRoles present in SignerRole
    /// </summary>
    CheckIfClaimedRolesOrCertifiedRolesPresentInSignerRole = 0x0100,
    /// <summary>
    /// Check if HashDataInfo of SignatureTimeStamp points to SignatureValue
    /// </summary>
    CheckHashDataInfoOfSignatureTimeStampPointsToSignatureValue = 0x0200,
    /// <summary>
    /// Check if the QualifyingProperties Target attribute points to the signature element
    /// </summary>
    CheckQualifyingPropertiesTarget = 0x0400,
    /// <summary>
    /// Check that QualifyingProperties occur in one Object, check that there is only one QualifyingProperties and that signed properties occur in one QualifyingProperties element
    /// </summary>
    CheckQualifyingProperties = 0x0800,
    /// <summary>
    /// Check if all required HashDataInfos are present on SigAndRefsTimeStamp
    /// </summary>
    CheckSigAndRefsTimeStampHashDataInfos = 0x1000,
    /// <summary>
    /// Check if all required HashDataInfos are present on RefsOnlyTimeStamp
    /// </summary>
    CheckRefsOnlyTimeStampHashDataInfos = 0x2000,
    /// <summary>
    /// Check if all required HashDataInfos are present on ArchiveTimeStamp
    /// </summary>
    CheckArchiveTimeStampHashDataInfos = 0x4000,
    /// <summary>
    /// Check if a XAdES-C signature is also a XAdES-T signature
    /// </summary>
    CheckXadesCIsXadesT = 0x8000,
    /// <summary>
    /// Check if a XAdES-XL signature is also a XAdES-X signature
    /// </summary>
    CheckXadesXLIsXadesX = 0x010000,
    /// <summary>
    /// Check if CertificateValues match CertificateRefs
    /// </summary>
    CheckCertificateValuesMatchCertificateRefs = 0x020000,
    /// <summary>
    /// Check if RevocationValues match RevocationRefs
    /// </summary>
    CheckRevocationValuesMatchRevocationRefs = 0x040000,
    /// <summary>
    /// Do all known tests on XAdES signature
    /// </summary>
    AllChecks = 0xFFFFFF
}

/// <summary>
/// Facade class for the XAdES signature library.  The class inherits from
/// the System.Security.Cryptography.Xml.SignedXml class and is backwards
/// compatible with it, so this class can host xmldsig signatures and XAdES
/// signatures.  The property SignatureStandard will indicate the type of the
/// signature: XMLDSIG or XAdES.
/// </summary>
public sealed class XadesSignedXml : SignedXml
{
    /// <summary>
    /// The XAdES XML namespace URI
    /// </summary>
    public const string XadesNamespaceUri = "http://uri.etsi.org/01903/v1.3.2#";

    /// <summary>
    /// The XAdES v1.4.1 XML namespace URI
    /// </summary>
    public const string XadesNamespace141Uri = "http://uri.etsi.org/01903/v1.4.1#";

    /// <summary>
    /// Mandated type name for the Uri reference to the SignedProperties element
    /// </summary>
    public const string SignedPropertiesType = "http://uri.etsi.org/01903#SignedProperties";


    public const string XmlDsigObjectType = "http://www.w3.org/2000/09/xmldsig#Object";

    private static readonly string[] idAttrs = {
        "_id",
        "_Id",
        "_ID"
    };

    private XmlDocument cachedXadesObjectDocument;
    private string signedPropertiesIdBuffer;
    private bool validationErrorOccurred;
    private string validationErrorDescription;
    private string signedInfoIdBuffer;
    private readonly XmlDocument signatureDocument;

    public static string XmlDSigPrefix { get; private set; }

    public static string XmlXadesPrefix { get; private set; }


    /// <summary>
    /// Property indicating the type of signature (XmlDsig or XAdES)
    /// </summary>
    private KnownSignatureStandard SignatureStandard { get; set; }

    /// <summary>
    /// Read-only property containing XAdES information
    /// </summary>
    internal XadesObject XadesObject
    {
        get
        {
            var retVal = new XadesObject();
            retVal.LoadXml(GetXadesObjectElement(GetXml()), GetXml());
            return retVal;
        }
    }

    /// <summary>
    /// Setting this property will add an ID attribute to the SignatureValue element.
    /// This is required when constructing a XAdES-T signature.
    /// </summary>
    public string SignatureValueId { get; set; }

    private XmlElement ContentElement { get; set; }

    public XmlElement SignatureNodeDestination { get; set; }

    public bool AddXadesNamespace { get; set; }

    /// <summary>
    /// Default constructor for the XadesSignedXml class
    /// </summary>
    public XadesSignedXml()
    {
        XmlDSigPrefix = "ds";
        XmlXadesPrefix = "xades";

        cachedXadesObjectDocument = null;
        SignatureStandard = KnownSignatureStandard.XmlDsig;
    }

    /// <summary>
    /// Constructor for the XadesSignedXml class
    /// </summary>
    /// <param name="signatureElement">XmlElement used to create the instance</param>
    public XadesSignedXml(XmlElement signatureElement)
        : base(signatureElement)
    {
        XmlDSigPrefix = "ds";
        XmlXadesPrefix = "xades";

        cachedXadesObjectDocument = null;
    }

    /// <summary>
    /// Constructor for the XadesSignedXml class
    /// </summary>
    /// <param name="signatureDocument">XmlDocument used to create the instance</param>
    public XadesSignedXml(XmlDocument signatureDocument)
        : base(signatureDocument)
    {
        XmlDSigPrefix = "ds";
        XmlXadesPrefix = "xades";
        this.signatureDocument = signatureDocument;

        cachedXadesObjectDocument = null;
    }

    /// <summary>
    /// Load state from an XML element
    /// </summary>
    /// <param name="xmlElement">The XML element from which to load the XadesSignedXml state</param>
    public new void LoadXml(XmlElement xmlElement)
    {
        cachedXadesObjectDocument = null;
        SignatureValueId = null;
        base.LoadXml(xmlElement);

        foreach (XmlAttribute attr in xmlElement.Attributes)
        {
            if (attr.Name.StartsWith("xmlns"))
            {
                if (string.Equals(attr.Value, XadesNamespaceUri, StringComparison.CurrentCultureIgnoreCase))
                {
                    XmlXadesPrefix = attr.Name.Split(':')[1];
                }
                else if (string.Equals(attr.Value, XmlDsigNamespaceUrl, StringComparison.CurrentCultureIgnoreCase))
                {
                    XmlDSigPrefix = attr.Name.Split(':')[1];
                }
            }
        }

        var idAttribute = xmlElement.Attributes.GetNamedItem("Id");
        if (idAttribute != null)
        {
            Signature.Id = idAttribute.Value;
        }
        SetSignatureStandard(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);

        xmlNamespaceManager.AddNamespace("ds", XmlDsigNamespaceUrl);
        xmlNamespaceManager.AddNamespace("xades", XadesNamespaceUri);

        var xmlNodeList = xmlElement.SelectNodes("ds:SignatureValue", xmlNamespaceManager);
        if (xmlNodeList.Count > 0)
        {
            if (((XmlElement)xmlNodeList[0]).HasAttribute("Id"))
            {
                SignatureValueId = ((XmlElement)xmlNodeList[0]).Attributes["Id"].Value;
            }
        }

        xmlNodeList = xmlElement.SelectNodes("ds:SignedInfo", xmlNamespaceManager);
        if (xmlNodeList.Count > 0)
        {
            signedInfoIdBuffer = ((XmlElement)xmlNodeList[0]).HasAttribute("Id") ? ((XmlElement)xmlNodeList[0]).Attributes["Id"].Value : null;
        }
    }

    /// <summary>
    /// Returns the XML representation of the this object
    /// </summary>
    /// <returns>XML element containing the state of this object</returns>
    public new XmlElement GetXml()
    {
        var retVal = base.GetXml();

        SetPrefix(XmlDSigPrefix, retVal);

        var xmlNamespaceManager = new XmlNamespaceManager(retVal.OwnerDocument.NameTable);
        xmlNamespaceManager.AddNamespace("ds", XmlDsigNamespaceUrl);


        if (!string.IsNullOrEmpty(SignatureValueId))
        {
            xmlNamespaceManager = new XmlNamespaceManager(retVal.OwnerDocument.NameTable);
            xmlNamespaceManager.AddNamespace("ds", XmlDsigNamespaceUrl);
            var xmlNodeList = retVal.SelectNodes("ds:SignatureValue", xmlNamespaceManager);
            if (xmlNodeList.Count > 0)
            {
                ((XmlElement)xmlNodeList[0]).SetAttribute("Id", SignatureValueId);
            }
        }

        return retVal;
    }

    /// <summary>
    /// Overridden virtual method to be able to find the nested SignedProperties
    /// element inside of the XAdES object
    /// </summary>
    /// <param name="xmlDocument">Document in which to find the Id</param>
    /// <param name="idValue">Value of the Id to look for</param>
    /// <returns>XmlElement with requested Id</returns>
    public override XmlElement GetIdElement(XmlDocument xmlDocument, string idValue)
    {
        XmlElement retVal = null;

        if (xmlDocument != null)
        {
            retVal = base.GetIdElement(xmlDocument, idValue);

            if (retVal != null)
            {
                return retVal;
            }

            foreach (var idAttr in idAttrs)
            {
                retVal = xmlDocument.SelectSingleNode("//*[@" + idAttr + "=\"" + idValue + "\"]") as XmlElement;
                if (retVal != null)
                {
                    break;
                }
            }
        }

        return retVal;
    }

    /// <summary>
    /// Add a XAdES object to the signature
    /// </summary>
    /// <param name="xadesObject">XAdES object to add to signature</param>
    internal void AddXadesObject(XadesObject xadesObject)
    {
        if (SignatureStandard != KnownSignatureStandard.Xades)
        {
            var dataObject = new DataObject();
            dataObject.Id = xadesObject.Id;
            dataObject.Data = xadesObject.GetXml().ChildNodes;
            AddObject(dataObject);

            var reference = new Reference();
            signedPropertiesIdBuffer = xadesObject.QualifyingProperties.SignedProperties.Id;
            reference.Uri = "#" + signedPropertiesIdBuffer;
            reference.Type = SignedPropertiesType;
            AddReference(reference);

            cachedXadesObjectDocument = new XmlDocument();
            var bufferXmlElement = xadesObject.GetXml();

            SetPrefix("ds", bufferXmlElement);

            cachedXadesObjectDocument.PreserveWhitespace = true;
            cachedXadesObjectDocument.LoadXml(bufferXmlElement.OuterXml);

            SignatureStandard = KnownSignatureStandard.Xades;
        }
        else
        {
            throw new CryptographicException("Can't add XAdES object, the signature already contains a XAdES object");
        }
    }

    /// <summary>
    /// Additional tests for XAdES signatures.  These tests focus on
    /// XMLDSIG verification and correct form of the XAdES XML structure
    /// (schema validation and completeness as defined by the XAdES standard).
    /// </summary>
    /// <remarks>
    /// Because of the fact that the XAdES library is intentionally
    /// independent of standards like TSP (RFC3161) or OCSP (RFC2560),
    /// these tests do NOT include any verification of timestamps nor OCSP
    /// responses.
    /// These checks are important and have to be done in the application
    /// built on top of the XAdES library.
    /// </remarks>
    /// <exception cref="System.Exception">Thrown when the signature is not
    /// a XAdES signature.  SignatureStandard should be equal to
    /// <see cref="KnownSignatureStandard.Xades">KnownSignatureStandard.Xades</see>.
    /// Use the CheckSignature method for non-XAdES signatures.</exception>
    /// <param name="xadesCheckSignatureMasks">Bitmask to indicate which
    /// tests need to be done.  This function will call a public virtual
    /// methods for each bit that has been set in this mask.
    /// See the <see cref="XadesCheckSignatureMasks">XadesCheckSignatureMasks</see>
    /// enum for the bitmask definitions.  The virtual test method associated
    /// with a bit in the mask has the same name as enum value name.</param>
    /// <returns>If the function returns true the check was OK.  If the
    /// check fails an exception with a explanatory message is thrown.</returns>
    public bool XadesCheckSignature(XadesCheckSignatureMasks xadesCheckSignatureMasks, HashAlgorithm hashAlgorithm)
    {
        var retVal = true;
        if (SignatureStandard != KnownSignatureStandard.Xades)
        {
            throw new Exception($"SignatureStandard is not XAdES.  CheckSignature returned: {CheckSignature()}");
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckXmldsigSignature) != 0)
        {
            retVal &= CheckXmldsigSignature();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.ValidateAgainstSchema) != 0)
        {
            retVal &= ValidateAgainstSchema();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckSameCertificate) != 0)
        {
            retVal &= CheckSameCertificate(hashAlgorithm);
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckAllReferencesExistInAllDataObjectsTimeStamp) != 0)
        {
            retVal &= CheckAllReferencesExistInAllDataObjectsTimeStamp();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckAllHashDataInfosInIndividualDataObjectsTimeStamp) != 0)
        {
            retVal &= CheckAllHashDataInfosInIndividualDataObjectsTimeStamp();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckCounterSignatures) != 0)
        {
            retVal &= CheckCounterSignatures(xadesCheckSignatureMasks, hashAlgorithm);
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckCounterSignaturesReference) != 0)
        {
            retVal &= CheckCounterSignaturesReference();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckObjectReferencesInCommitmentTypeIndication) != 0)
        {
            retVal &= CheckObjectReferencesInCommitmentTypeIndication();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckIfClaimedRolesOrCertifiedRolesPresentInSignerRole) != 0)
        {
            retVal &= CheckIfClaimedRolesOrCertifiedRolesPresentInSignerRole();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckHashDataInfoOfSignatureTimeStampPointsToSignatureValue) != 0)
        {
            retVal &= CheckHashDataInfoOfSignatureTimeStampPointsToSignatureValue();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckQualifyingPropertiesTarget) != 0)
        {
            retVal &= CheckQualifyingPropertiesTarget();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckQualifyingProperties) != 0)
        {
            retVal &= CheckQualifyingProperties();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckSigAndRefsTimeStampHashDataInfos) != 0)
        {
            retVal &= CheckSigAndRefsTimeStampHashDataInfos();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckRefsOnlyTimeStampHashDataInfos) != 0)
        {
            retVal &= CheckRefsOnlyTimeStampHashDataInfos();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckArchiveTimeStampHashDataInfos) != 0)
        {
            retVal &= CheckArchiveTimeStampHashDataInfos();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckXadesCIsXadesT) != 0)
        {
            retVal &= CheckXadesCIsXadesT();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckXadesXLIsXadesX) != 0)
        {
            retVal &= CheckXadesXLIsXadesX();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckCertificateValuesMatchCertificateRefs) != 0)
        {
            retVal &= CheckCertificateValuesMatchCertificateRefs();
        }
        if ((xadesCheckSignatureMasks & XadesCheckSignatureMasks.CheckRevocationValuesMatchRevocationRefs) != 0)
        {
            retVal &= CheckRevocationValuesMatchRevocationRefs();
        }

        return retVal;
    }


    private X509Certificate2 GetSigningCertificate()
    {
        var keyXml = KeyInfo.GetXml().GetElementsByTagName("X509Certificate", XmlDsigNamespaceUrl)[0];

        if (keyXml is null)
        {
            throw new Exception("Could not obtain signing certificate.");
        }

        return new X509Certificate2(Convert.FromBase64String(keyXml.InnerText));
    }

    /// <summary>
    /// Check the signature of the underlying XMLDSIG signature
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckXmldsigSignature()
    {
        var retVal = false;
        IEnumerable<XmlAttribute> namespaces = GetAllNamespaces(GetSignatureElement());

        if (KeyInfo is null)
        {
            var keyInfo = new KeyInfo();
            X509Certificate xmldsigCert = GetSigningCertificate();
            keyInfo.AddClause(new KeyInfoX509Data(xmldsigCert));
            KeyInfo = keyInfo;
        }

        if (CryptoConfig.CreateFromName(SignedInfo.SignatureMethod) is not SignatureDescription description)
        {
            if (SignedInfo.SignatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")
            {
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            }
            else if (SignedInfo.SignatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512")
            {
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA512SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512");
            }
        }

        foreach (Reference reference in SignedInfo.References)
        {
            foreach (System.Security.Cryptography.Xml.Transform transform in reference.TransformChain)
            {
                if (transform.GetType() == typeof(XmlDsigXPathTransform))
                {
                    var transform_Type = typeof(XmlDsigXPathTransform);
                    var nsm_FieldInfo = transform_Type.GetField("_nsm", BindingFlags.NonPublic | BindingFlags.Instance);
                    var nsm = (XmlNamespaceManager)nsm_FieldInfo.GetValue(transform);

                    foreach (var ns in namespaces)
                    {
                        nsm.AddNamespace(ns.LocalName, ns.Value);
                    }
                }
            }
        }

        retVal = CheckDigestedReferences();

        if (retVal == false)
        {
            throw new CryptographicException("CheckXmldsigSignature() failed");
        }

        var key = GetPublicKey();
        retVal = CheckSignedInfo(key);

        if (retVal == false)
        {
            throw new CryptographicException("CheckXmldsigSignature() failed");
        }

        return true;
    }

    /// <summary>
    /// Validate the XML representation of the signature against the XAdES and XMLDSIG schemas
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool ValidateAgainstSchema()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var schemaSet = new XmlSchemaSet();

        validationErrorOccurred = false;
        validationErrorDescription = "";

        try
        {
            var schemaStream = assembly.GetManifestResourceStream("Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades.xmldsig-core-schema.xsd");
            var xmlSchema = XmlSchema.Read(schemaStream, SchemaValidationHandler);
            schemaSet.Add(xmlSchema);
            schemaStream.Close();


            schemaStream = assembly.GetManifestResourceStream("Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades.XAdES.xsd");
            xmlSchema = XmlSchema.Read(schemaStream, SchemaValidationHandler);
            schemaSet.Add(xmlSchema);
            schemaStream.Close();

            if (validationErrorOccurred)
            {
                throw new CryptographicException("Schema read validation error: " + validationErrorDescription);
            }
        }
        catch (Exception exception)
        {
            throw new CryptographicException("Problem during access of validation schemas", exception);
        }

        var xmlReaderSettings = new XmlReaderSettings();
        xmlReaderSettings.ValidationEventHandler += XmlValidationHandler;
        xmlReaderSettings.ValidationType = ValidationType.Schema;
        xmlReaderSettings.Schemas = schemaSet;
        xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;

        var xadesNameTable = new NameTable();
        var xmlNamespaceManager = new XmlNamespaceManager(xadesNameTable);
        xmlNamespaceManager.AddNamespace("xsd", XadesNamespaceUri);

        var xmlParserContext = new XmlParserContext(null, xmlNamespaceManager, null, XmlSpace.None);

        var txtReader = new XmlTextReader(GetXml().OuterXml, XmlNodeType.Element, xmlParserContext);
        var reader = XmlReader.Create(txtReader, xmlReaderSettings);
        try
        {
            while (reader.Read())
            {
            }

            if (validationErrorOccurred)
            {
                throw new CryptographicException($"Schema validation error: {validationErrorDescription}");
            }
        }
        catch (Exception exception)
        {
            throw new CryptographicException("Schema validation error", exception);
        }
        finally
        {
            reader.Close();
        }
        return true;
    }

    /// <summary>
    /// Check to see if first XMLDSIG certificate has same hashvalue as first XAdES SignatureCertificate
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckSameCertificate(HashAlgorithm hashAlgorithm)
    {
        var xadesSigningCertificateCollection = XadesObject.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.CertCollection;
        var xmldsigCert = GetSigningCertificate();
        var xmldsigCertHash = Convert.ToBase64String(hashAlgorithm.ComputeHash(xmldsigCert.GetRawCertData()));

        if (xadesSigningCertificateCollection.Count <= 0)
        {
            throw new CryptographicException("Certificate not found in SigningCertificate element while doing CheckSameCertificate()");
        }
        var xadesCertHash = Convert.ToBase64String(xadesSigningCertificateCollection[0].CertDigest.DigestValue);


        if (string.Compare(xmldsigCertHash, xadesCertHash, true, CultureInfo.InvariantCulture) != 0)
        {
            throw new CryptographicException("Certificate in XMLDSIG signature doesn't match certificate in SigningCertificate element");
        }
        return true;
    }

    /// <summary>
    /// Check if there is a HashDataInfo for each reference if there is a AllDataObjectsTimeStamp
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckAllReferencesExistInAllDataObjectsTimeStamp()
    {
        var allHashDataInfosExist = true;
        var allDataObjectsTimeStampCollection = XadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties.AllDataObjectsTimeStampCollection;
        if (allDataObjectsTimeStampCollection.Count > 0)
        {
            int timeStampCounter;
            for (timeStampCounter = 0; allHashDataInfosExist && (timeStampCounter < allDataObjectsTimeStampCollection.Count); timeStampCounter++)
            {
                var timeStamp = allDataObjectsTimeStampCollection[timeStampCounter];
                allHashDataInfosExist &= CheckHashDataInfosForTimeStamp(timeStamp);
            }
            if (!allHashDataInfosExist)
            {
                throw new CryptographicException("At least one HashDataInfo is missing in AllDataObjectsTimeStamp element");
            }
        }
        return true;
    }

    /// <summary>
    /// Check if the HashDataInfo of each IndividualDataObjectsTimeStamp points to existing Reference
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckAllHashDataInfosInIndividualDataObjectsTimeStamp()
    {
        var hashDataInfoExists = true;
        var individualDataObjectsTimeStampCollection = XadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties.IndividualDataObjectsTimeStampCollection;
        if (individualDataObjectsTimeStampCollection.Count > 0)
        {
            int timeStampCounter;
            for (timeStampCounter = 0; hashDataInfoExists && (timeStampCounter < individualDataObjectsTimeStampCollection.Count); timeStampCounter++)
            {
                var timeStamp = individualDataObjectsTimeStampCollection[timeStampCounter];
                hashDataInfoExists &= CheckHashDataInfosExist(timeStamp);
            }
            if (hashDataInfoExists == false)
            {
                throw new CryptographicException("At least one HashDataInfo is pointing to non-existing reference in IndividualDataObjectsTimeStamp element");
            }
        }
        return true;
    }

    /// <summary>
    /// Perform XAdES checks on contained counter signatures.  If couter signature is XMLDSIG, only XMLDSIG check (CheckSignature()) is done.
    /// </summary>
    /// <param name="counterSignatureMask">Check mask applied to counter signatures</param>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckCounterSignatures(XadesCheckSignatureMasks counterSignatureMask, HashAlgorithm hashAlgorithm)
    {
        var retVal = true;
        var counterSignatureCollection = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.CounterSignatureCollection;
        for (var counterSignatureCounter = 0; retVal && (counterSignatureCounter < counterSignatureCollection.Count); counterSignatureCounter++)
        {
            var counterSignature = counterSignatureCollection[counterSignatureCounter];
            if (counterSignature.SignatureStandard == KnownSignatureStandard.Xades)
            {
                retVal &= counterSignature.XadesCheckSignature(counterSignatureMask, hashAlgorithm);
            }
            else
            {
                retVal &= counterSignature.CheckSignature();
            }
        }
        if (retVal == false)
        {
            throw new CryptographicException("XadesCheckSignature() failed on at least one counter signature");
        }
        return true;
    }

    /// <summary>
    /// Counter signatures should all contain a reference to the parent signature SignatureValue element
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckCounterSignaturesReference()
    {
        var retVal = true;
        var parentSignatureValueChain = new ArrayList();
        parentSignatureValueChain.Add("#" + SignatureValueId);
        var counterSignatureCollection = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.CounterSignatureCollection;
        for (var counterSignatureCounter = 0; retVal && (counterSignatureCounter < counterSignatureCollection.Count); counterSignatureCounter++)
        {
            var counterSignature = counterSignatureCollection[counterSignatureCounter];
            var referenceToParentSignatureFound = false;
            for (var referenceCounter = 0; referenceToParentSignatureFound == false && (referenceCounter < counterSignature.SignedInfo.References.Count); referenceCounter++)
            {
                var referenceUri = ((Reference)counterSignature.SignedInfo.References[referenceCounter]).Uri;
                if (parentSignatureValueChain.BinarySearch(referenceUri) >= 0)
                {
                    referenceToParentSignatureFound = true;
                }
                parentSignatureValueChain.Add("#" + counterSignature.SignatureValueId);
                parentSignatureValueChain.Sort();
            }
            retVal = referenceToParentSignatureFound;
        }
        if (retVal == false)
        {
            throw new CryptographicException("CheckCounterSignaturesReference() failed on at least one counter signature");
        }
        return true;
    }

    /// <summary>
    /// Check if each ObjectReference in CommitmentTypeIndication points to Reference element
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckObjectReferencesInCommitmentTypeIndication()
    {
        var retVal = true;
        var commitmentTypeIndicationCollection = XadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties.CommitmentTypeIndicationCollection;
        if (commitmentTypeIndicationCollection.Count > 0)
        {
            for (var commitmentTypeIndicationCounter = 0; retVal && (commitmentTypeIndicationCounter < commitmentTypeIndicationCollection.Count); commitmentTypeIndicationCounter++)
            {
                var commitmentTypeIndication = commitmentTypeIndicationCollection[commitmentTypeIndicationCounter];
                var objectReferenceOK = true;
                foreach (ObjectReference objectReference in commitmentTypeIndication.ObjectReferenceCollection)
                {
                    objectReferenceOK &= CheckObjectReference(objectReference);
                }
                retVal = objectReferenceOK;
            }
            if (retVal == false)
            {
                throw new CryptographicException("At least one ObjectReference in CommitmentTypeIndication did not point to a Reference");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if at least ClaimedRoles or CertifiedRoles present in SignerRole
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckIfClaimedRolesOrCertifiedRolesPresentInSignerRole()
    {
        var retVal = false;
        var signerRole = XadesObject.QualifyingProperties.SignedProperties.SignedSignatureProperties.SignerRole;
        if (signerRole != null)
        {
            if (signerRole.CertifiedRoles != null)
            {
                retVal = (signerRole.CertifiedRoles.CertifiedRoleCollection.Count > 0);
            }
            if (retVal == false)
            {
                if (signerRole.ClaimedRoles != null)
                {
                    retVal = (signerRole.ClaimedRoles.ClaimedRoleCollection.Count > 0);
                }
            }
            if (retVal == false)
            {
                throw new CryptographicException("SignerRole element must contain at least one CertifiedRole or ClaimedRole element");
            }
        }
        else
        {
            retVal = true;
        }

        return true;
    }

    /// <summary>
    /// Check if HashDataInfo of SignatureTimeStamp points to SignatureValue
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckHashDataInfoOfSignatureTimeStampPointsToSignatureValue()
    {
        var hashDataInfoPointsToSignatureValue = true;
        var signatureTimeStampCollection = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.SignatureTimeStampCollection;
        if (signatureTimeStampCollection.Count > 0)
        {
            int timeStampCounter;
            for (timeStampCounter = 0; hashDataInfoPointsToSignatureValue && (timeStampCounter < signatureTimeStampCollection.Count); timeStampCounter++)
            {
                var timeStamp = signatureTimeStampCollection[timeStampCounter];
                hashDataInfoPointsToSignatureValue &= CheckHashDataInfoPointsToSignatureValue(timeStamp);
            }
            if (hashDataInfoPointsToSignatureValue == false)
            {
                throw new CryptographicException("HashDataInfo of SignatureTimeStamp doesn't point to signature value element");
            }
        }
        return true;
    }

    /// <summary>
    /// Check if the QualifyingProperties Target attribute points to the signature element
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckQualifyingPropertiesTarget()
    {
        var retVal = true;
        var qualifyingPropertiesTarget = XadesObject.QualifyingProperties.Target;
        if (Signature.Id == null)
        {
            retVal = false;
        }
        else
        {
            if (qualifyingPropertiesTarget != ("#" + Signature.Id))
            {
                retVal = false;
            }
        }
        if (retVal == false)
        {
            throw new CryptographicException("Qualifying properties target doesn't point to signature element or signature element doesn't have an Id");
        }

        return true;
    }

    /// <summary>
    /// Check that QualifyingProperties occur in one Object, check that there is only one QualifyingProperties and that signed properties occur in one QualifyingProperties element
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckQualifyingProperties()
    {
        var signatureElement = GetXml();
        var xmlNamespaceManager = new XmlNamespaceManager(signatureElement.OwnerDocument.NameTable);
        xmlNamespaceManager.AddNamespace("ds", XmlDsigNamespaceUrl);
        xmlNamespaceManager.AddNamespace("xsd", XadesNamespaceUri);
        var xmlNodeList = signatureElement.SelectNodes("ds:Object/xsd:QualifyingProperties", xmlNamespaceManager);
        if (xmlNodeList.Count > 1)
        {
            throw new CryptographicException("More than one Object contains a QualifyingProperties element");
        }

        return true;
    }

    /// <summary>
    /// Check if all required HashDataInfos are present on SigAndRefsTimeStamp
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckSigAndRefsTimeStampHashDataInfos()
    {
        var signatureTimeStampCollection = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.SigAndRefsTimeStampCollection;
        if (signatureTimeStampCollection.Count > 0)
        {
            var allRequiredhashDataInfosFound = true;
            for (var timeStampCounter = 0; allRequiredhashDataInfosFound && (timeStampCounter < signatureTimeStampCollection.Count); timeStampCounter++)
            {
                var timeStamp = signatureTimeStampCollection[timeStampCounter];
                allRequiredhashDataInfosFound &= CheckHashDataInfosOfSigAndRefsTimeStamp(timeStamp);
            }
            if (allRequiredhashDataInfosFound == false)
            {
                throw new CryptographicException("At least one required HashDataInfo is missing in a SigAndRefsTimeStamp element");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if all required HashDataInfos are present on RefsOnlyTimeStamp
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckRefsOnlyTimeStampHashDataInfos()
    {
        var signatureTimeStampCollection = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.RefsOnlyTimeStampCollection;
        if (signatureTimeStampCollection.Count > 0)
        {
            var allRequiredhashDataInfosFound = true;
            for (var timeStampCounter = 0; allRequiredhashDataInfosFound && (timeStampCounter < signatureTimeStampCollection.Count); timeStampCounter++)
            {
                var timeStamp = signatureTimeStampCollection[timeStampCounter];
                allRequiredhashDataInfosFound &= CheckHashDataInfosOfRefsOnlyTimeStamp(timeStamp);
            }
            if (allRequiredhashDataInfosFound == false)
            {
                throw new CryptographicException("At least one required HashDataInfo is missing in a RefsOnlyTimeStamp element");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if all required HashDataInfos are present on ArchiveTimeStamp
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckArchiveTimeStampHashDataInfos()
    {
        var signatureTimeStampCollection = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties.ArchiveTimeStampCollection;
        if (signatureTimeStampCollection.Count > 0)
        {
            var allRequiredhashDataInfosFound = true;
            for (var timeStampCounter = 0; allRequiredhashDataInfosFound && (timeStampCounter < signatureTimeStampCollection.Count); timeStampCounter++)
            {
                var timeStamp = signatureTimeStampCollection[timeStampCounter];
                allRequiredhashDataInfosFound &= CheckHashDataInfosOfArchiveTimeStamp(timeStamp);
            }
            if (allRequiredhashDataInfosFound == false)
            {
                throw new CryptographicException("At least one required HashDataInfo is missing in a ArchiveTimeStamp element");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if a XAdES-C signature is also a XAdES-T signature
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckXadesCIsXadesT()
    {
        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;
        if (((unsignedSignatureProperties.CompleteCertificateRefs != null) && (unsignedSignatureProperties.CompleteCertificateRefs.HasChanged()))
            || ((unsignedSignatureProperties.CompleteCertificateRefs != null) && (unsignedSignatureProperties.CompleteCertificateRefs.HasChanged())))
        {
            if (unsignedSignatureProperties.SignatureTimeStampCollection.Count == 0)
            {
                throw new CryptographicException("XAdES-C signature should also contain a SignatureTimeStamp element");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if a XAdES-XL signature is also a XAdES-X signature
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckXadesXLIsXadesX()
    {
        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;
        if (((unsignedSignatureProperties.CertificateValues != null) && (unsignedSignatureProperties.CertificateValues.HasChanged()))
            || ((unsignedSignatureProperties.RevocationValues != null) && (unsignedSignatureProperties.RevocationValues.HasChanged())))
        {
            if ((unsignedSignatureProperties.SigAndRefsTimeStampCollection.Count == 0) && (unsignedSignatureProperties.RefsOnlyTimeStampCollection.Count == 0))
            {
                throw new CryptographicException("XAdES-XL signature should also contain a XAdES-X element");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if CertificateValues match CertificateRefs
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckCertificateValuesMatchCertificateRefs()
    {
        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;
        if ((unsignedSignatureProperties.CompleteCertificateRefs != null) && (unsignedSignatureProperties.CompleteCertificateRefs.CertRefs != null) &&
            (unsignedSignatureProperties.CertificateValues != null))
        {
            var certDigests = new ArrayList();
            foreach (Cert cert in unsignedSignatureProperties.CompleteCertificateRefs.CertRefs.CertCollection)
            {
                certDigests.Add(Convert.ToBase64String(cert.CertDigest.DigestValue));
            }
            certDigests.Sort();
            foreach (EncapsulatedX509Certificate encapsulatedX509Certificate in unsignedSignatureProperties.CertificateValues.EncapsulatedX509CertificateCollection)
            {
                var certDigest = SHA1.HashData(encapsulatedX509Certificate.PkiData);
                var index = certDigests.BinarySearch(Convert.ToBase64String(certDigest));
                if (index >= 0)
                {
                    certDigests.RemoveAt(index);
                }
            }
            if (certDigests.Count != 0)
            {
                throw new CryptographicException("Not all CertificateRefs correspond to CertificateValues");
            }
        }

        return true;
    }

    /// <summary>
    /// Check if RevocationValues match RevocationRefs
    /// </summary>
    /// <returns>If the function returns true the check was OK</returns>
    private bool CheckRevocationValuesMatchRevocationRefs()
    {
        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;
        if ((unsignedSignatureProperties.CompleteRevocationRefs != null) && (unsignedSignatureProperties.CompleteRevocationRefs.CRLRefs != null) &&
            (unsignedSignatureProperties.RevocationValues != null))
        {
            var crlDigests = new ArrayList();
            foreach (CRLRef crlRef in unsignedSignatureProperties.CompleteRevocationRefs.CRLRefs.CRLRefCollection)
            {
                crlDigests.Add(Convert.ToBase64String(crlRef.CertDigest.DigestValue));
            }
            crlDigests.Sort();
            foreach (CRLValue crlValue in unsignedSignatureProperties.RevocationValues.CRLValues.CRLValueCollection)
            {
                var crlDigest = SHA1.HashData(crlValue.PkiData);
                var index = crlDigests.BinarySearch(Convert.ToBase64String(crlDigest));
                if (index >= 0)
                {
                    crlDigests.RemoveAt(index);
                }
            }
            if (crlDigests.Count != 0)
            {
                throw new CryptographicException("Not all RevocationRefs correspond to RevocationValues");
            }
        }

        return true;
    }

    private static void SetPrefix(string prefix, XmlNode node)
    {
        if (node.NamespaceURI == XmlDsigNamespaceUrl)
        {
            node.Prefix = prefix;
        }

        foreach (XmlNode child in node.ChildNodes)
        {
            SetPrefix(prefix, child);
        }
    }


    private SignatureDescription GetSignatureDescription()
    {
        if (CryptoConfig.CreateFromName(SignedInfo.SignatureMethod) is not SignatureDescription description)
        {
            if (SignedInfo.SignatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")
            {
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            }
            else if (SignedInfo.SignatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512")
            {
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA512SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512");
            }

            description = CryptoConfig.CreateFromName(SignedInfo.SignatureMethod) as SignatureDescription;
        }

        return description;
    }

    public new void ComputeSignature()
    {

        BuildDigestedReferences();

        var signingKey = SigningKey;
        if (signingKey == null)
        {
            throw new CryptographicException("Cryptography_Xml_LoadKeyFailed");
        }
        if (SignedInfo.SignatureMethod == null)
        {
            if (!(signingKey is DSA))
            {
                if (!(signingKey is RSA))
                {
                    throw new CryptographicException("Cryptography_Xml_CreatedKeyFailed");
                }
                if (SignedInfo.SignatureMethod == null)
                {
                    SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
                }
            }
            else
            {
                SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
            }
        }

        var description = GetSignatureDescription();
        if (description == null)
        {
            throw new CryptographicException("Cryptography_Xml_SignatureDescriptionNotCreated");
        }

        var hash = description.CreateDigest();
        if (hash == null)
        {
            throw new CryptographicException("Cryptography_Xml_CreateHashAlgorithmFailed");
        }

        GetC14NDigest(hash, "ds");

        m_signature.SignatureValue = description.CreateFormatter(signingKey).CreateSignature(hash);
    }

    private Reference GetContentReference()
    {
        XadesObject xadesObject;

        if (cachedXadesObjectDocument != null)
        {
            xadesObject = new XadesObject();
            xadesObject.LoadXml(cachedXadesObjectDocument.DocumentElement, null);
        }
        else
        {
            xadesObject = XadesObject;
        }

        if (xadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties.DataObjectFormatCollection.Count > 0)
        {
            var referenceId = xadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties.DataObjectFormatCollection[0].ObjectReferenceAttribute.Substring(1);

            foreach (var reference in SignedInfo.References)
            {
                if (((Reference)reference).Id == referenceId)
                {
                    return (Reference)reference;
                }
            }
        }

        return (Reference)SignedInfo.References[0];
    }

    private void FindContentElement()
    {
        var contentRef = GetContentReference();

        if (!string.IsNullOrEmpty(contentRef.Uri) &&
            contentRef.Uri.StartsWith("#"))
        {
            ContentElement = GetIdElement(signatureDocument, contentRef.Uri.Substring(1));
        }
        else
        {
            ContentElement = signatureDocument.DocumentElement;
        }
    }

    public XmlElement GetSignatureElement()
    {
        var signatureElement = GetIdElement(signatureDocument, Signature.Id);

        if (signatureElement != null)
        {
            return signatureElement;
        }

        if (SignatureNodeDestination != null)
        {
            return SignatureNodeDestination;
        }

        if (ContentElement == null)
        {
            return null;
        }

        if (ContentElement.ParentNode.NodeType != XmlNodeType.Document)
        {
            return (XmlElement)ContentElement.ParentNode;
        }

        return ContentElement;
    }


    public List<XmlAttribute> GetAllNamespaces(XmlElement fromElement)
    {
        var namespaces = new List<XmlAttribute>();

        if (fromElement != null && fromElement.ParentNode.NodeType == XmlNodeType.Document)
        {
            foreach (XmlAttribute attr in fromElement.Attributes)
            {
                if (attr.Name.StartsWith("xmlns") && !namespaces.Exists(f => f.Name == attr.Name))
                {
                    namespaces.Add(attr);
                }
            }

            return namespaces;
        }

        XmlNode currentNode = fromElement;

        while (currentNode != null && currentNode.NodeType != XmlNodeType.Document)
        {
            foreach (XmlAttribute attr in currentNode.Attributes)
            {
                if (attr.Name.StartsWith("xmlns") && !namespaces.Exists(f => f.Name == attr.Name))
                {
                    namespaces.Add(attr);
                }
            }

            currentNode = currentNode.ParentNode;
        }

        return namespaces;
    }

    /// <summary>
    /// Copy of System.Security.Cryptography.Xml.SignedXml.BuildDigestedReferences() which will add a "ds"
    /// namespace prefix to all XmlDsig nodes
    /// </summary>
    private void BuildDigestedReferences()
    {
        var references = SignedInfo.References;

        var SignedXml_Type = typeof(SignedXml);
        var SignedXml_m_refProcessed = SignedXml_Type.GetField("_refProcessed", BindingFlags.NonPublic | BindingFlags.Instance);
        SignedXml_m_refProcessed.SetValue(this, new bool[references.Count]);

        var SignedXml_m_refLevelCache = SignedXml_Type.GetField("_refLevelCache", BindingFlags.NonPublic | BindingFlags.Instance);
        SignedXml_m_refLevelCache.SetValue(this, new int[references.Count]);

        var System_Security_Assembly = Assembly.Load("System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
        var cripXmlAssembly = Assembly.Load("System.Security.Cryptography.Xml");
        var ReferenceLevelSortOrder_Type = System_Security_Assembly.GetType("System.Security.Cryptography.Xml.SignedXml+ReferenceLevelSortOrder");
        var ReferenceLevelSortOrder_Constructor = ReferenceLevelSortOrder_Type.GetConstructor(Array.Empty<Type>());
        var comparer = ReferenceLevelSortOrder_Constructor.Invoke(null);

        var ReferenceLevelSortOrder_References = ReferenceLevelSortOrder_Type.GetProperty("References", BindingFlags.Public | BindingFlags.Instance);
        ReferenceLevelSortOrder_References.SetValue(comparer, references, null);

        var list2 = new ArrayList();
        foreach (Reference reference in references)
        {
            list2.Add(reference);
        }

        list2.Sort((IComparer)comparer);

        var CanonicalXmlNodeList_Type = cripXmlAssembly.GetType("System.Security.Cryptography.Xml.CanonicalXmlNodeList");
        var CanonicalXmlNodeList_Constructor = CanonicalXmlNodeList_Type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);

        var refList = CanonicalXmlNodeList_Constructor.Invoke(null);

        var CanonicalXmlNodeList_Add = CanonicalXmlNodeList_Type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);

        var SignedXml_m_containingDocument = SignedXml_Type.GetField("_containingDocument", BindingFlags.NonPublic | BindingFlags.Instance);
        var Reference_Type = typeof(Reference);
        var Reference_UpdateHashValue = Reference_Type.GetMethod("UpdateHashValue", BindingFlags.NonPublic | BindingFlags.Instance);

        var m_containingDocument = SignedXml_m_containingDocument.GetValue(this);

        if (ContentElement == null)
        {
            FindContentElement();
        }

        var signatureParentNodeNameSpaces = GetAllNamespaces(GetSignatureElement());

        if (AddXadesNamespace)
        {
            var attr = signatureDocument.CreateAttribute("xmlns:xades");
            attr.Value = XadesNamespaceUri;

            signatureParentNodeNameSpaces.Add(attr);
        }

        XmlDocument xmlDoc = null;
        var addSignatureNamespaces = false;
        foreach (Reference reference2 in list2)
        {
            if (reference2.Uri.StartsWith("#KeyInfoId-"))
            {
                var keyInfoXml = KeyInfo.GetXml();
                SetPrefix(XmlDSigPrefix, keyInfoXml);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(keyInfoXml.OuterXml);

                addSignatureNamespaces = true;
            }
            else if (reference2.Type == SignedPropertiesType)
            {
                xmlDoc = (XmlDocument)cachedXadesObjectDocument.Clone();

                addSignatureNamespaces = true;
            }
            else if (reference2.Type == XmlDsigObjectType)
            {
                var dataObjectId = reference2.Uri.Substring(1);
                XmlElement dataObjectXml = null;

                foreach (DataObject dataObject in m_signature.ObjectList)
                {
                    if (dataObjectId == dataObject.Id)
                    {
                        dataObjectXml = dataObject.GetXml();

                        SetPrefix(XmlDSigPrefix, dataObjectXml);

                        addSignatureNamespaces = true;

                        xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(dataObjectXml.OuterXml);

                        break;
                    }
                }

                if (dataObjectXml == null)
                {
                    dataObjectXml = GetIdElement(signatureDocument, dataObjectId);

                    if (dataObjectXml != null)
                    {
                        xmlDoc = new XmlDocument();
                        xmlDoc.PreserveWhitespace = true;
                        xmlDoc.LoadXml(dataObjectXml.OuterXml);
                    }
                    else
                    {
                        throw new Exception("No reference target found");
                    }
                }
            }
            else
            {
                xmlDoc = (XmlDocument)m_containingDocument;
            }


            if (addSignatureNamespaces)
            {
                foreach (var attr in signatureParentNodeNameSpaces)
                {
                    var newAttr = xmlDoc.CreateAttribute(attr.Name);
                    newAttr.Value = attr.Value;

                    xmlDoc.DocumentElement.Attributes.Append(newAttr);
                }
            }

            if (xmlDoc != null)
            {
                CanonicalXmlNodeList_Add.Invoke(refList, new object[] { xmlDoc.DocumentElement });
            }

            Reference_UpdateHashValue.Invoke(reference2, new object[] { xmlDoc, refList });

            if (reference2.Id != null)
            {
                var xml = reference2.GetXml();

                SetPrefix(XmlDSigPrefix, xml);
            }
        }
    }


    private new AsymmetricAlgorithm GetPublicKey()
    {
        var SignedXml_Type = typeof(SignedXml);

        var SignedXml_Type_GetPublicKey = SignedXml_Type.GetMethod("GetPublicKey", BindingFlags.NonPublic | BindingFlags.Instance);

        return SignedXml_Type_GetPublicKey.Invoke(this, null) as AsymmetricAlgorithm;
    }


    private bool CheckDigestedReferences()
    {
        var references = m_signature.SignedInfo.References;

        var System_Security_Assembly = Assembly.Load("System.Security.Cryptography.Xml");
        var CanonicalXmlNodeList_Type = System_Security_Assembly.GetType("System.Security.Cryptography.Xml.CanonicalXmlNodeList");
        var CanonicalXmlNodeList_Constructor = CanonicalXmlNodeList_Type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);

        var CanonicalXmlNodeList_Add = CanonicalXmlNodeList_Type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
        var refList = CanonicalXmlNodeList_Constructor.Invoke(null);

        CanonicalXmlNodeList_Add.Invoke(refList, new object[] { signatureDocument });

        var Reference_Type = typeof(Reference);
        var Reference_CalculateHashValue = Reference_Type.GetMethod("CalculateHashValue", BindingFlags.NonPublic | BindingFlags.Instance);

        for (var i = 0; i < references.Count; ++i)
        {
            var digestedReference = (Reference)references[i];

            var calculatedHash = (byte[])Reference_CalculateHashValue.Invoke(digestedReference, new object[] { signatureDocument, refList });

            if (calculatedHash.Length != digestedReference.DigestValue.Length)
                return false;

            var rgb1 = calculatedHash;
            var rgb2 = digestedReference.DigestValue;
            for (var j = 0; j < rgb1.Length; ++j)
            {
                if (rgb1[j] != rgb2[j]) return false;
            }
        }

        return true;
    }


    private bool CheckSignedInfo(AsymmetricAlgorithm key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (CryptoConfig.CreateFromName(SignatureMethod) is not SignatureDescription signatureDescription)
        {
            throw new CryptographicException("signature description can't be created");
        }

        var hashAlgorithm = signatureDescription.CreateDigest();
        if (hashAlgorithm == null)
        {
            throw new CryptographicException("signature description can't be created");
        }

        var hashval = GetC14NDigest(hashAlgorithm, "ds");
        var asymmetricSignatureDeformatter = signatureDescription.CreateDeformatter(key);
        return asymmetricSignatureDeformatter.VerifySignature(hashval, m_signature.SignatureValue);
    }

    /// <summary>
    /// Copy of System.Security.Cryptography.Xml.SignedXml.GetC14NDigest() which will add a
    /// namespace prefix to all XmlDsig nodes
    /// </summary>
    private byte[] GetC14NDigest(HashAlgorithm hash, string prefix)
    {
        var SignedXml_Type = typeof(SignedXml);
        var SignedXml_bCacheValid = SignedXml_Type.GetField("_bCacheValid", BindingFlags.NonPublic | BindingFlags.Instance);
        var bCacheValid = (bool)SignedXml_bCacheValid.GetValue(this);
        var SignedInfo_Type = typeof(SignedInfo);
        var SignedInfo_CacheValid = SignedInfo_Type.GetProperty("CacheValid", BindingFlags.NonPublic | BindingFlags.Instance);
        var CacheValid = (bool)SignedInfo_CacheValid.GetValue(SignedInfo, null);

        var SignedXml__digestedSignedInfo = SignedXml_Type.GetField("_digestedSignedInfo", BindingFlags.NonPublic | BindingFlags.Instance);

        if (!bCacheValid || !CacheValid)
        {
            var SignedXml_m_containingDocument = SignedXml_Type.GetField("_containingDocument", BindingFlags.NonPublic | BindingFlags.Instance);
            var m_containingDocument = (XmlDocument)SignedXml_m_containingDocument.GetValue(this);
            var securityUrl = (m_containingDocument == null) ? null : m_containingDocument.BaseURI;

            var SignedXml_m_bResolverSet = SignedXml_Type.GetField("_bResolverSet", BindingFlags.NonPublic | BindingFlags.Instance);
            var m_bResolverSet = (bool)SignedXml_m_bResolverSet.GetValue(this);
            var SignedXml_m_xmlResolver = SignedXml_Type.GetField("_xmlResolver", BindingFlags.NonPublic | BindingFlags.Instance);
            var m_xmlResolver = (XmlResolver)SignedXml_m_xmlResolver.GetValue(this);
            var xmlResolver = m_bResolverSet ? m_xmlResolver : XmlResolver.ThrowingResolver;

            var System_Security_Assembly = Assembly.Load("System.Security.Cryptography.Xml");
            var Utils_Type = System_Security_Assembly.GetType("System.Security.Cryptography.Xml.Utils");
            var Utils_PreProcessElementInput = Utils_Type.GetMethod("PreProcessElementInput", BindingFlags.NonPublic | BindingFlags.Static);

            var xml = SignedInfo.GetXml();
            SetPrefix(prefix, xml);

            var document = (XmlDocument)Utils_PreProcessElementInput.Invoke(null, new object[] { xml, xmlResolver, securityUrl });

            var docNamespaces = GetAllNamespaces(GetSignatureElement());

            if (AddXadesNamespace)
            {
                var attr = signatureDocument.CreateAttribute("xmlns:xades");
                attr.Value = XadesNamespaceUri;

                docNamespaces.Add(attr);
            }


            foreach (var attr in docNamespaces)
            {
                var newAttr = document.CreateAttribute(attr.Name);
                newAttr.Value = attr.Value;

                document.DocumentElement.Attributes.Append(newAttr);
            }

            var SignedXml_m_context = SignedXml_Type.GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            var Utils_GetPropagatedAttributes = Utils_Type.GetMethod("GetPropagatedAttributes", BindingFlags.NonPublic | BindingFlags.Static);
            var m_context = SignedXml_m_context.GetValue(this);
            var namespaces = (m_context == null) ? null : Utils_GetPropagatedAttributes.Invoke(null, new object[] { m_context });


            var CanonicalXmlNodeList_Type = System_Security_Assembly.GetType("System.Security.Cryptography.Xml.CanonicalXmlNodeList");
            var Utils_AddNamespaces = Utils_Type.GetMethod("AddNamespaces", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(XmlElement), CanonicalXmlNodeList_Type }, null);
            Utils_AddNamespaces.Invoke(null, new object[] { document.DocumentElement, namespaces });

            var canonicalizationMethodObject = SignedInfo.CanonicalizationMethodObject;

            canonicalizationMethodObject.Resolver = xmlResolver;

            var Transform_Type = typeof(System.Security.Cryptography.Xml.Transform);
            var Transform_BaseURI = Transform_Type.GetProperty("BaseURI", BindingFlags.NonPublic | BindingFlags.Instance);
            Transform_BaseURI.SetValue(canonicalizationMethodObject, securityUrl, null);

            canonicalizationMethodObject.LoadInput(document);

            SignedXml__digestedSignedInfo.SetValue(this, canonicalizationMethodObject.GetDigestedOutput(hash));

            SignedXml_bCacheValid.SetValue(this, true);
        }

        var _digestedSignedInfo = (byte[])SignedXml__digestedSignedInfo.GetValue(this);
        return _digestedSignedInfo;
    }

    private XmlElement GetXadesObjectElement(XmlElement signatureElement)
    {
        XmlElement retVal = null;

        var xmlNamespaceManager = new XmlNamespaceManager(signatureElement.OwnerDocument.NameTable);
        xmlNamespaceManager.AddNamespace("ds", XmlDsigNamespaceUrl);
        xmlNamespaceManager.AddNamespace("xades", XadesNamespaceUri);

        var xmlNodeList = signatureElement.SelectNodes("ds:Object/xades:QualifyingProperties", xmlNamespaceManager);
        if (xmlNodeList.Count > 0)
        {
            retVal = (XmlElement)xmlNodeList.Item(0).ParentNode;
        }

        return retVal;
    }

    private void SetSignatureStandard(XmlElement signatureElement)
    {
        SignatureStandard = GetXadesObjectElement(signatureElement) != null ? KnownSignatureStandard.Xades : KnownSignatureStandard.XmlDsig;
    }

    private DataObject GetXadesDataObject()
    {
        DataObject retVal = null;

        for (var dataObjectCounter = 0; dataObjectCounter < (Signature.ObjectList.Count); dataObjectCounter++)
        {
            var dataObject = (DataObject)Signature.ObjectList[dataObjectCounter];
            var dataObjectXmlElement = dataObject.GetXml();
            var xmlNamespaceManager = new XmlNamespaceManager(dataObjectXmlElement.OwnerDocument.NameTable);
            xmlNamespaceManager.AddNamespace("xades", XadesNamespaceUri);
            var xmlNodeList = dataObjectXmlElement.SelectNodes("xades:QualifyingProperties", xmlNamespaceManager);
            if (xmlNodeList.Count != 0)
            {
                retVal = dataObject;

                break;
            }
        }

        return retVal;
    }

    private void SchemaValidationHandler(object sender, ValidationEventArgs validationEventArgs)
    {
        validationErrorOccurred = true;
        validationErrorDescription += "Validation error:\n";
        validationErrorDescription += "\tSeverity: " + validationEventArgs.Severity + "\n";
        validationErrorDescription += "\tMessage: " + validationEventArgs.Message + "\n";
    }

    private void XmlValidationHandler(object sender, ValidationEventArgs validationEventArgs)
    {
        if (validationEventArgs.Severity != XmlSeverityType.Warning)
        {
            validationErrorOccurred = true;
            validationErrorDescription += "Validation error:\n";
            validationErrorDescription += "\tSeverity: " + validationEventArgs.Severity + "\n";
            validationErrorDescription += "\tMessage: " + validationEventArgs.Message + "\n";
        }
    }

    private bool CheckHashDataInfosForTimeStamp(TimeStamp timeStamp)
    {
        var retVal = true;

        for (var referenceCounter = 0; retVal && (referenceCounter < SignedInfo.References.Count); referenceCounter++)
        {
            var referenceId = ((Reference)SignedInfo.References[referenceCounter]).Id;
            var referenceUri = ((Reference)SignedInfo.References[referenceCounter]).Uri;
            if (referenceUri != ("#" + XadesObject.QualifyingProperties.SignedProperties.Id))
            {
                var hashDataInfoFound = false;
                for (var hashDataInfoCounter = 0; hashDataInfoFound == false && (hashDataInfoCounter < timeStamp.HashDataInfoCollection.Count); hashDataInfoCounter++)
                {
                    var hashDataInfo = timeStamp.HashDataInfoCollection[hashDataInfoCounter];
                    hashDataInfoFound = (("#" + referenceId) == hashDataInfo.UriAttribute);
                }
                retVal = hashDataInfoFound;
            }
        }

        return retVal;
    }

    private bool CheckHashDataInfosExist(TimeStamp timeStamp)
    {
        var retVal = true;

        for (var hashDataInfoCounter = 0; retVal && (hashDataInfoCounter < timeStamp.HashDataInfoCollection.Count); hashDataInfoCounter++)
        {
            var hashDataInfo = timeStamp.HashDataInfoCollection[hashDataInfoCounter];
            var referenceFound = false;
            string referenceId;

            for (var referenceCounter = 0; referenceFound == false && (referenceCounter < SignedInfo.References.Count); referenceCounter++)
            {
                referenceId = ((Reference)SignedInfo.References[referenceCounter]).Id;
                if (("#" + referenceId) == hashDataInfo.UriAttribute)
                {
                    referenceFound = true;
                }
            }
            retVal = referenceFound;
        }

        return retVal;
    }


    private bool CheckObjectReference(ObjectReference objectReference)
    {
        var retVal = false;

        for (var referenceCounter = 0; retVal == false && (referenceCounter < SignedInfo.References.Count); referenceCounter++)
        {
            var referenceId = ((Reference)SignedInfo.References[referenceCounter]).Id;
            if (("#" + referenceId) == objectReference.ObjectReferenceUri)
            {
                retVal = true;
            }
        }

        return retVal;
    }

    private bool CheckHashDataInfoPointsToSignatureValue(TimeStamp timeStamp)
    {
        var retVal = true;
        foreach (HashDataInfo hashDataInfo in timeStamp.HashDataInfoCollection)
        {
            retVal &= (hashDataInfo.UriAttribute == ("#" + SignatureValueId));
        }

        return retVal;
    }

    private bool CheckHashDataInfosOfSigAndRefsTimeStamp(TimeStamp timeStamp)
    {
        var signatureValueHashDataInfoFound = false;
        var allSignatureTimeStampHashDataInfosFound = false;
        var completeCertificateRefsHashDataInfoFound = false;
        var completeRevocationRefsHashDataInfoFound = false;

        var signatureTimeStampIds = new ArrayList();

        var retVal = true;

        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;

        foreach (TimeStamp signatureTimeStamp in unsignedSignatureProperties.SignatureTimeStampCollection)
        {
            signatureTimeStampIds.Add("#" + signatureTimeStamp.EncapsulatedTimeStamp.Id);
        }
        signatureTimeStampIds.Sort();
        foreach (HashDataInfo hashDataInfo in timeStamp.HashDataInfoCollection)
        {
            if (hashDataInfo.UriAttribute == "#" + SignatureValueId)
            {
                signatureValueHashDataInfoFound = true;
            }
            var signatureTimeStampIdIndex = signatureTimeStampIds.BinarySearch(hashDataInfo.UriAttribute);
            if (signatureTimeStampIdIndex >= 0)
            {
                signatureTimeStampIds.RemoveAt(signatureTimeStampIdIndex);
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CompleteCertificateRefs.Id)
            {
                completeCertificateRefsHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CompleteRevocationRefs.Id)
            {
                completeRevocationRefsHashDataInfoFound = true;
            }
        }
        if (signatureTimeStampIds.Count == 0)
        {
            allSignatureTimeStampHashDataInfosFound = true;
        }
        retVal = signatureValueHashDataInfoFound && allSignatureTimeStampHashDataInfosFound && completeCertificateRefsHashDataInfoFound && completeRevocationRefsHashDataInfoFound;

        return retVal;
    }

    private bool CheckHashDataInfosOfRefsOnlyTimeStamp(TimeStamp timeStamp)
    {
        var completeCertificateRefsHashDataInfoFound = false;
        var completeRevocationRefsHashDataInfoFound = false;
        var retVal = true;

        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;
        foreach (HashDataInfo hashDataInfo in timeStamp.HashDataInfoCollection)
        {
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CompleteCertificateRefs.Id)
            {
                completeCertificateRefsHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CompleteRevocationRefs.Id)
            {
                completeRevocationRefsHashDataInfoFound = true;
            }
        }
        retVal = completeCertificateRefsHashDataInfoFound && completeRevocationRefsHashDataInfoFound;

        return retVal;
    }

    private bool CheckHashDataInfosOfArchiveTimeStamp(TimeStamp timeStamp)
    {
        var allReferenceHashDataInfosFound = false;
        var signedInfoHashDataInfoFound = false;
        var signedPropertiesHashDataInfoFound = false;
        var signatureValueHashDataInfoFound = false;
        var allSignatureTimeStampHashDataInfosFound = false;
        var completeCertificateRefsHashDataInfoFound = false;
        var completeRevocationRefsHashDataInfoFound = false;
        var certificatesValuesHashDataInfoFound = false;
        var revocationValuesHashDataInfoFound = false;
        var allSigAndRefsTimeStampHashDataInfosFound = false;
        var allRefsOnlyTimeStampHashDataInfosFound = false;
        var allArchiveTimeStampHashDataInfosFound = false;
        var allOlderArchiveTimeStampsFound = false;

        var referenceIds = new ArrayList();
        var signatureTimeStampIds = new ArrayList();
        var sigAndRefsTimeStampIds = new ArrayList();
        var refsOnlyTimeStampIds = new ArrayList();
        var archiveTimeStampIds = new ArrayList();

        var retVal = true;

        var unsignedSignatureProperties = XadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties;
        var signedProperties = XadesObject.QualifyingProperties.SignedProperties;

        foreach (Reference reference in Signature.SignedInfo.References)
        {
            if (reference.Uri != "#" + signedProperties.Id)
            {
                referenceIds.Add(reference.Uri);
            }
        }
        referenceIds.Sort();
        foreach (TimeStamp signatureTimeStamp in unsignedSignatureProperties.SignatureTimeStampCollection)
        {
            signatureTimeStampIds.Add("#" + signatureTimeStamp.EncapsulatedTimeStamp.Id);
        }
        signatureTimeStampIds.Sort();
        foreach (TimeStamp sigAndRefsTimeStamp in unsignedSignatureProperties.SigAndRefsTimeStampCollection)
        {
            sigAndRefsTimeStampIds.Add("#" + sigAndRefsTimeStamp.EncapsulatedTimeStamp.Id);
        }
        sigAndRefsTimeStampIds.Sort();
        foreach (TimeStamp refsOnlyTimeStamp in unsignedSignatureProperties.RefsOnlyTimeStampCollection)
        {
            refsOnlyTimeStampIds.Add("#" + refsOnlyTimeStamp.EncapsulatedTimeStamp.Id);
        }
        refsOnlyTimeStampIds.Sort();
        allOlderArchiveTimeStampsFound = false;
        for (var archiveTimeStampCounter = 0; !allOlderArchiveTimeStampsFound && (archiveTimeStampCounter < unsignedSignatureProperties.ArchiveTimeStampCollection.Count); archiveTimeStampCounter++)
        {
            var archiveTimeStamp = unsignedSignatureProperties.ArchiveTimeStampCollection[archiveTimeStampCounter];
            if (archiveTimeStamp.EncapsulatedTimeStamp.Id == timeStamp.EncapsulatedTimeStamp.Id)
            {
                allOlderArchiveTimeStampsFound = true;
            }
            else
            {
                archiveTimeStampIds.Add("#" + archiveTimeStamp.EncapsulatedTimeStamp.Id);
            }
        }

        archiveTimeStampIds.Sort();
        foreach (HashDataInfo hashDataInfo in timeStamp.HashDataInfoCollection)
        {
            var index = referenceIds.BinarySearch(hashDataInfo.UriAttribute);
            if (index >= 0)
            {
                referenceIds.RemoveAt(index);
            }
            if (hashDataInfo.UriAttribute == "#" + signedInfoIdBuffer)
            {
                signedInfoHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + signedProperties.Id)
            {
                signedPropertiesHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + SignatureValueId)
            {
                signatureValueHashDataInfoFound = true;
            }
            index = signatureTimeStampIds.BinarySearch(hashDataInfo.UriAttribute);
            if (index >= 0)
            {
                signatureTimeStampIds.RemoveAt(index);
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CompleteCertificateRefs.Id)
            {
                completeCertificateRefsHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CompleteRevocationRefs.Id)
            {
                completeRevocationRefsHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.CertificateValues.Id)
            {
                certificatesValuesHashDataInfoFound = true;
            }
            if (hashDataInfo.UriAttribute == "#" + unsignedSignatureProperties.RevocationValues.Id)
            {
                revocationValuesHashDataInfoFound = true;
            }
            index = sigAndRefsTimeStampIds.BinarySearch(hashDataInfo.UriAttribute);
            if (index >= 0)
            {
                sigAndRefsTimeStampIds.RemoveAt(index);
            }
            index = refsOnlyTimeStampIds.BinarySearch(hashDataInfo.UriAttribute);
            if (index >= 0)
            {
                refsOnlyTimeStampIds.RemoveAt(index);
            }
            index = archiveTimeStampIds.BinarySearch(hashDataInfo.UriAttribute);
            if (index >= 0)
            {
                archiveTimeStampIds.RemoveAt(index);
            }
        }
        if (referenceIds.Count == 0)
        {
            allReferenceHashDataInfosFound = true;
        }
        if (signatureTimeStampIds.Count == 0)
        {
            allSignatureTimeStampHashDataInfosFound = true;
        }
        if (sigAndRefsTimeStampIds.Count == 0)
        {
            allSigAndRefsTimeStampHashDataInfosFound = true;
        }
        if (refsOnlyTimeStampIds.Count == 0)
        {
            allRefsOnlyTimeStampHashDataInfosFound = true;
        }
        if (archiveTimeStampIds.Count == 0)
        {
            allArchiveTimeStampHashDataInfosFound = true;
        }

        retVal = allReferenceHashDataInfosFound && signedInfoHashDataInfoFound && signedPropertiesHashDataInfoFound &&
                 signatureValueHashDataInfoFound && allSignatureTimeStampHashDataInfosFound && completeCertificateRefsHashDataInfoFound &&
                 completeRevocationRefsHashDataInfoFound && certificatesValuesHashDataInfoFound && revocationValuesHashDataInfoFound &&
                 allSigAndRefsTimeStampHashDataInfosFound && allRefsOnlyTimeStampHashDataInfosFound && allArchiveTimeStampHashDataInfosFound;

        return retVal;
    }
}