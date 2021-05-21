using System;
using Mews.Eet.Dto.Identifiers;

namespace Mews.Eet.Dto
{
    public class Identification
    {
        public Identification(TaxIdentifier taxPayerIdentifier, RegistryIdentifier registryIdentifier, PremisesIdentifier premisesIdentifier, Certificate certificate)
            : this(taxPayerIdentifier, null, registryIdentifier, premisesIdentifier, certificate)
        {
        }

        public Identification(TaxIdentifier taxPayerIdentifier, TaxIdentifier mandatingTaxPayerIdentifier, RegistryIdentifier registryIdentifier, PremisesIdentifier premisesIdentifier, MandationType mandationType, Certificate certificate)
            : this(mandatingTaxPayerIdentifier, mandationType == Dto.MandationType.Section9Paragraph1 ? taxPayerIdentifier : null, registryIdentifier, premisesIdentifier, certificate, mandationType)
        {
        }

        private Identification(TaxIdentifier taxPayerIdentifier, TaxIdentifier mandantingTaxPayerIdentifier, RegistryIdentifier registryIdentifier, PremisesIdentifier premisesIdentifier, Certificate certificate, MandationType? mandationType = null)
        {
            if (taxPayerIdentifier == null)
            {
                throw new ArgumentException("The taxpayer identifier is required.");
            }

            if (registryIdentifier == null)
            {
                throw new ArgumentException("Registry identifier is required.");
            }

            if (premisesIdentifier == null)
            {
                throw new ArgumentException("Premises identifier is required.");
            }

            if (certificate == null)
            {
                throw new ArgumentException("The certificate cannot be null.");
            }

            TaxPayerIdentifier = taxPayerIdentifier;
            MandantingTaxPayerIdentifier = mandantingTaxPayerIdentifier;
            RegistryIdentifier = registryIdentifier;
            PremisesIdentifier = premisesIdentifier;
            MandationType = mandationType;
            Certificate = certificate;
        }

        public TaxIdentifier TaxPayerIdentifier { get; }

        public TaxIdentifier MandantingTaxPayerIdentifier { get; }

        public RegistryIdentifier RegistryIdentifier { get; }

        public PremisesIdentifier PremisesIdentifier { get; }

        public MandationType? MandationType { get; }

        public Certificate Certificate { get; }
    }
}
