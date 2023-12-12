using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

public sealed class RSAPKCS1SHA512SignatureDescription : SignatureDescription
{
    public RSAPKCS1SHA512SignatureDescription()
    {
        KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
        DigestAlgorithm = typeof(SHA512).FullName;
        FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
        DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
    }

    public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
    {
        ArgumentNullException.ThrowIfNull(key);

        var deformatter = new RSAPKCS1SignatureDeformatter(key);
        deformatter.SetHashAlgorithm("SHA512");
        return deformatter;
    }

    public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
    {
        ArgumentNullException.ThrowIfNull(key);

        var formatter = new RSAPKCS1SignatureFormatter(key);
        formatter.SetHashAlgorithm("SHA512");
        return formatter;
    }
}