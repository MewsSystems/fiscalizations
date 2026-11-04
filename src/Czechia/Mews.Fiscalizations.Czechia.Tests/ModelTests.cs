namespace Mews.Fiscalizations.Czechia.Tests;

public class ModelTests
{
    [TestCase("CZ00000019")]
    [TestCase("CZ683555118")]
    [TestCase("CZ8551015704")]
    public void ValidTaxIdentifiers(string value)
    {
        Assert.That(TaxIdentifier.Create(value).IsSuccess, Is.True);
    }

    [TestCase("00000019")]
    [TestCase("CZ1234567")]
    [TestCase("CZ12345678901")]
    [TestCase("cz00000019")]
    [TestCase("SK00000019")]
    [TestCase("CZ00000019\n")]
    [TestCase("")]
    [TestCase(null)]
    public void InvalidTaxIdentifiers(string? value)
    {
        Assert.That(TaxIdentifier.Create(value!).IsError, Is.True);
    }

    [TestCase(1, true)]
    [TestCase(243, true)]
    [TestCase(999_999_999, true)]
    [TestCase(0, false)]
    [TestCase(-5, false)]
    [TestCase(1_000_000_000, false)]
    public void RegistrationUnitIdentifierRange(int value, bool isValid)
    {
        Assert.That(RegistrationUnitIdentifier.Create(value).IsSuccess, Is.EqualTo(isValid));
    }

    [TestCase("5a/A-q/5:22d_2", true)]
    [TestCase("12345678901234567890", true)]
    [TestCase("123456789012345678901", false)]
    [TestCase("pokladna-č1", false)]
    [TestCase("POS-1\n", false)]
    [TestCase("", false)]
    public void RegistryIdentifierPattern(string value, bool isValid)
    {
        Assert.That(RegistryIdentifier.Create(value).IsSuccess, Is.EqualTo(isValid));
    }

    [TestCase("#25/c-12/1A_2/2027", true)]
    [TestCase("1234567890123456789012345", true)]
    [TestCase("12345678901234567890123456", false)]
    [TestCase("bill+1", false)]
    [TestCase("123\n", false)]
    public void BillNumberPattern(string value, bool isValid)
    {
        Assert.That(BillNumber.Create(value).IsSuccess, Is.EqualTo(isValid));
    }

    [TestCase(250, "250.00")]
    [TestCase(-187.2, "-187.20")]
    [TestCase(0.56, "0.56")]
    [TestCase(0, "0.00")]
    [TestCase(-0.01, "-0.01")]
    [TestCase(20.45, "20.45")]
    [TestCase(99_999_999.99, "99999999.99")]
    [TestCase(-99_999_999.99, "-99999999.99")]
    public void CurrencyValueCanonicalFormat(decimal value, string expected)
    {
        Assert.That(CurrencyValue.CreateUnsafe(value).ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void NegativeZeroIsFormattedWithoutSign()
    {
        Assert.That(CurrencyValue.CreateUnsafe(-0.00m).ToString(), Is.EqualTo("0.00"));
        Assert.That(CurrencyValue.CreateUnsafe(10.500m).ToString(), Is.EqualTo("10.50"));
    }

    [TestCase(100_000_000)]
    [TestCase(-100_000_000)]
    [TestCase(1.005)]
    public void InvalidCurrencyValues(decimal value)
    {
        Assert.That(CurrencyValue.Create(value).IsError, Is.True);
    }

    [Test]
    public void CertificateIsLoadedFromPkcs12()
    {
        var (x509, key) = TestCertificates.CreateSelfSigned();
        var certificate = Certificate.Create(TestCertificates.CreatePkcs12(x509, key, "Secret123"), "Secret123");

        Assert.That(certificate.IsSuccess, Is.True);
        Assert.That(certificate.Success.Get().X509Certificate.RawData, Is.EqualTo(x509.RawData));
    }

    [Test]
    public void CertificateWithWrongPasswordIsRejected()
    {
        var (x509, key) = TestCertificates.CreateSelfSigned();
        Assert.That(Certificate.Create(TestCertificates.CreatePkcs12(x509, key, "Secret123"), "wrong").IsError, Is.True);
        Assert.That(Certificate.Create(new byte[] { 1, 2, 3 }, "Secret123").IsError, Is.True);
    }

    [Test]
    public void CertificateWithoutMatchingPrivateKeyIsRejected()
    {
        var (x509, _) = TestCertificates.CreateSelfSigned();
        var (_, otherKey) = TestCertificates.CreateSelfSigned();

        Assert.That(Certificate.Create(x509).IsError, Is.True);
        Assert.That(Certificate.Create(x509, otherKey).IsError, Is.True);
        Assert.That(Certificate.Create(TestCertificates.CreatePkcs12(x509, otherKey, "Secret123"), "Secret123").IsError, Is.True);
    }
}
