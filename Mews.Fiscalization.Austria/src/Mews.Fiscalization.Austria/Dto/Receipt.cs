using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Mews.Fiscalization.Austria.Dto.Identifiers;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class Receipt
    {
        public Receipt(
            ReceiptNumber number,
            RegisterIdentifier registerIdentifier,
            TaxData taxData,
            CurrencyValue turnover,
            CertificateSerialNumber certificateSerialNumber,
            JwsRepresentation previousJwsRepresentation,
            byte[] key,
            LocalDateTime created = null)
            : this(number, registerIdentifier, taxData, certificateSerialNumber, key, created)
        {
            PreviousJwsRepresentation = previousJwsRepresentation;
            ChainValue = ComputeChainValue();
            Turnover = turnover ?? throw new ArgumentException("The turnover has to be specified.");
            EncryptedTurnover = EncryptTurnover();
        }

        public Receipt(
            ReceiptNumber number,
            RegisterIdentifier registerIdentifier,
            TaxData taxData,
            EncryptedTurnover encryptedTurnover,
            CertificateSerialNumber certificateSerialNumber,
            ChainValue chainValue,
            byte[] key,
            LocalDateTime created = null)
            : this(number, registerIdentifier, taxData, certificateSerialNumber, key, created)
        {
            EncryptedTurnover = encryptedTurnover;
            Turnover = DecryptTurnover();
            ChainValue = chainValue;
        }

        private Receipt(ReceiptNumber number,
            RegisterIdentifier registerIdentifier,
            TaxData taxData,
            CertificateSerialNumber certificateSerialNumber,
            byte[] key,
            LocalDateTime created = null)
        {
            Number = number ?? throw new ArgumentException("The receipt number has to be specified.");
            RegisterIdentifier = registerIdentifier ?? throw new ArgumentException("The register identifier has to be specified.");
            TaxData = taxData ?? throw new ArgumentException("The tax data have to be specified.");
            CertificateSerialNumber = certificateSerialNumber ?? throw new ArgumentException("The certificate serial number has to be specified.");
            Created = created ?? LocalDateTime.Now;
            Suite = "R1-AT1";
            Key = key;
        }

        public ReceiptNumber Number { get; }

        public RegisterIdentifier RegisterIdentifier { get; }

        public LocalDateTime Created { get; }

        public TaxData TaxData { get; }

        public CurrencyValue Turnover { get; }

        public CertificateSerialNumber CertificateSerialNumber { get; }

        public JwsRepresentation PreviousJwsRepresentation { get; }

        public ChainValue ChainValue { get; }

        public EncryptedTurnover EncryptedTurnover { get; }

        public string Suite { get; }

        private byte[] Key { get; }

        private EncryptedTurnover EncryptTurnover()
        {
            var sum = TaxData.Sum();
            var counter = (Turnover.Value + sum) * 100;
            var valueBytes = GetValueBytes((long)counter);
            var encryptedValue = AesCtr(valueBytes, encrypt: true);

            return new EncryptedTurnover(encryptedValue);
        }

        private CurrencyValue DecryptTurnover()
        {
            var valueBytes = AesCtr(EncryptedTurnover.Value, encrypt: false);
            var decryptedValue = GetValuesFromBytes(valueBytes);

            return new CurrencyValue(decryptedValue / 100m);
        }

        private byte[] AesCtr(byte[] value, bool encrypt)
        {
            var registryReceiptId = RegisterIdentifier.Value + Number.Value;
            var initializationVectorBytes = Encoding.UTF8.GetBytes(registryReceiptId);
            var registryReceiptIdHash = Sha256(initializationVectorBytes);
            var initializationVector = GetEncryptInitializationVector(registryReceiptIdHash);

            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(forEncryption: encrypt, parameters: new ParametersWithIV(new KeyParameter(Key), initializationVector));
            cipher.ProcessBytes(value);
            return cipher.DoFinal();
        }

        private byte[] GetEncryptInitializationVector(byte[] hash)
        {
            return hash.Take(16).ToArray();
        }

        private byte[] GetValueBytes(long value)
        {
            var originalBytes = BitConverter.GetBytes(value);
            var bytesSubset = originalBytes.Take(8);
            var orderedBytes = BitConverter.IsLittleEndian ? bytesSubset.Reverse() : bytesSubset;
            return orderedBytes.ToArray();
        }

        private long GetValuesFromBytes(byte[] bytes)
        {
            var orderedBytes = BitConverter.IsLittleEndian ? bytes.Reverse() : bytes;
            return BitConverter.ToInt64(orderedBytes.ToArray(), startIndex: 0);
        }

        private ChainValue ComputeChainValue()
        {
            var input = PreviousJwsRepresentation?.Value ?? RegisterIdentifier.Value;
            var hash = Sha256(Encoding.UTF8.GetBytes(input));
            return new ChainValue(hash.Take(8).ToArray());
        }

        private byte[] Sha256(byte[] toBeHashed)
        {
            return new SHA256Managed().ComputeHash(toBeHashed);
        }
    }
}
