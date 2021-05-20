using Mews.Eet.Converters;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Wsdl;
using Mews.Eet.Extensions;

namespace Mews.Eet.Communication
{
    public class SendRevenueMessage
    {
        public SendRevenueMessage(RevenueRecord record, EetMode mode)
        {
            RevenueRecord = record;
            EetMode = mode;
        }

        private RevenueRecord RevenueRecord { get; }

        private EetMode EetMode { get; }

        public SendRevenueXmlMessage GetXmlMessage()
        {
            return new SendRevenueXmlMessage
            {
                Header = GetRevenueHeader(),
                Data = GetRevenueData(),
                SecurityCode = GetRevenueSecurityCode()
            };
        }

        private RevenueHeader GetRevenueHeader()
        {
            return new RevenueHeader
            {
                MessageUuid = RevenueRecord.Identifier.ToString(),
                Sent = DateTimeConverter.ToEetDateTime(DateTimeProvider.Now),
                FirstTry = RevenueRecord.IsFirstAttempt,
                Verification = EetMode == EetMode.Verification,
                VerificationSpecified = EetMode == EetMode.Verification
            };
        }

        private RevenueData GetRevenueData()
        {
            var revenue = RevenueRecord.Revenue;
            return new RevenueData
            {
                TaxPayerTaxIdentifier = RevenueRecord.Identification.TaxPayerIdentifier.Value,
                MandantingTaxPayerIdentifier = RevenueRecord.Identification.MandantingTaxPayerIdentifier?.Value,
                RegistryIdentifier = RevenueRecord.Identification.RegistryIdentifier.Value,
                PremisesIdentifier = RevenueRecord.Identification.PremisesIdentifier.Value,
                BillNumber = RevenueRecord.BillNumber.Value,
                Accepted = DateTimeConverter.ToEetDateTime(RevenueRecord.Revenue.Accepted),
                Total = RevenueRecord.Revenue.Gross.Value,

                NotTaxableNetSpecified = revenue.NotTaxable.IsDefined(),
                NotTaxableNet = revenue.NotTaxable.GetOrDefault(),

                LowerReducedRateNetSpecified = revenue.LowerReducedTaxRate.IsValueDefined(r => r.Net),
                LowerReducedRateTaxSpecified = revenue.LowerReducedTaxRate.IsValueDefined(r => r.Tax),
                LowerReducedRateNet = revenue.LowerReducedTaxRate.GetOrDefault(r => r.Net),
                LowerReducedRateTax = revenue.LowerReducedTaxRate.GetOrDefault(r => r.Tax),

                ReducedRateNetSpecified = revenue.ReducedTaxRate.IsValueDefined(r => r.Net),
                ReducedRateTaxSpecified = revenue.ReducedTaxRate.IsValueDefined(r => r.Tax),
                ReducedRateNet = revenue.ReducedTaxRate.GetOrDefault(r => r.Net),
                ReducedRateTax = revenue.ReducedTaxRate.GetOrDefault(r => r.Tax),

                StandartRateNetSpecified = revenue.StandardTaxRate.IsValueDefined(r => r.Net),
                StandartRateTaxSpecified = revenue.StandardTaxRate.IsValueDefined(r => r.Tax),
                StandartRateNet = revenue.StandardTaxRate.GetOrDefault(r => r.Net),
                StandartRateTax = revenue.StandardTaxRate.GetOrDefault(r => r.Tax),

                TravelServicesSpecified = revenue.TravelServices.IsDefined(),
                TravelServices = revenue.TravelServices.GetOrDefault(),

                LowerReducedRateGoodsSpecified = revenue.LowerReducedTaxRate.IsValueDefined(r => r.Goods),
                LowerReducedRateGoods = revenue.LowerReducedTaxRate.GetOrDefault(r => r.Goods),

                ReducedRateGoodsSpecified = revenue.ReducedTaxRate.IsValueDefined(r => r.Goods),
                ReducedRateGoods = revenue.ReducedTaxRate.GetOrDefault(r => r.Goods),

                StandardRateGoodsSpecified = revenue.StandardTaxRate.IsValueDefined(r => r.Goods),
                StandardRateGoods = revenue.StandardTaxRate.GetOrDefault(r => r.Goods),

                DepositSpecified = revenue.Deposit.IsDefined(),
                Deposit = revenue.Deposit.GetOrDefault(),

                DepositUsedSpecified = revenue.UsedDeposit.IsDefined(),
                DepositUsed = revenue.UsedDeposit.GetOrDefault()
            };
        }

        private RevenueSecurityCode GetRevenueSecurityCode()
        {
            return new RevenueSecurityCode()
            {
                SecurityCode = new SecurityCode
                {
                    Digest = SecurityCodeDigestType.Sha1,
                    Encoding = SecurityCodeEncodingType.Base16,
                    Text = new[] { RevenueRecord.SecurityCode }
                },
                Signature = new Signature
                {
                    Cipher = SignatureCipherType.Rsa2048,
                    Digest = SignatureDigestType.Sha256,
                    Encoding = SignatureEncodingType.Base64,
                    Text = new[] { RevenueRecord.Signature }
                }
            };
        }
    }
}
