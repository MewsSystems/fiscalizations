using System;
using System.Globalization;
using Mews.Fiscalization.Austria.Dto.Identifiers;

namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class QrData
    {
        public QrData(Receipt receipt, CultureInfo culture, TimeZoneInfo timeZone)
            : this(receipt, ComputeValue(receipt, culture, timeZone), culture, timeZone)
        {
        }

        public QrData(string value, byte[] key, CultureInfo culture, TimeZoneInfo timeZone)
            : this(ParseReceipt(value, key, culture, timeZone), value, culture, timeZone)
        {
        }

        private QrData(Receipt receipt, string value, CultureInfo culture, TimeZoneInfo timeZone)
        {
            Receipt = receipt;
            Value = value;
            Culture = culture;
            TimeZone = timeZone;
        }

        public Receipt Receipt { get; }

        public string Value { get; }

        public CultureInfo Culture { get; }

        public TimeZoneInfo TimeZone { get; }

        public override string ToString()
        {
            return Value;
        }

        private static string ComputeValue(Receipt receipt, CultureInfo culture, TimeZoneInfo timeZone)
        {
            var fieldValues = new[]
            {
                receipt.Suite,
                receipt.RegisterIdentifier.Value,
                receipt.Number.Value,
                receipt.Created.ToString(timeZone),
                receipt.TaxData.StandardRate.FormatValue(culture),
                receipt.TaxData.ReducedRate.FormatValue(culture),
                receipt.TaxData.LowerReducedRate.FormatValue(culture),
                receipt.TaxData.ZeroRate.FormatValue(culture),
                receipt.TaxData.SpecialRate.FormatValue(culture),
                receipt.EncryptedTurnover.ToBase64String(),
                receipt.CertificateSerialNumber.Value,
                receipt.ChainValue.ToBase64String()
            };

            return $"_{String.Join("_", fieldValues)}";
        }

        private static Receipt ParseReceipt(string value, byte[] key, CultureInfo culture, TimeZoneInfo timeZone)
        {
            const int expectedValueCount = 12;
            var values = value.Trim('_').Split('_');
            if (values.Length < expectedValueCount)
            {
                throw new ArgumentException("Value does not contain valid QR data.");
            }

            return new Receipt(
                registerIdentifier: new RegisterIdentifier(values[1]),
                number: new ReceiptNumber(values[2]),
                created: LocalDateTime.Parse(values[3], timeZone),
                taxData: new TaxData(
                    CurrencyValue.Parse(values[4], culture),
                    CurrencyValue.Parse(values[5], culture),
                    CurrencyValue.Parse(values[6], culture),
                    CurrencyValue.Parse(values[7], culture),
                    CurrencyValue.Parse(values[8], culture)
                ),
                encryptedTurnover: new EncryptedTurnover(values[9]),
                certificateSerialNumber: new CertificateSerialNumber(values[10]),
                chainValue: new ChainValue(values[11]),
                key: key
            );
        }
    }
}
