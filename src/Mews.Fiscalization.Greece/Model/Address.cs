using System;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public class Address
    {
        public Address(NonEmptyString postalCode, NonEmptyString city, string street = null, string number = null)
        {
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Street = street;
            Number = number;
        }

        public string Street { get; }

        public string Number { get; }

        public NonEmptyString PostalCode { get; }

        public NonEmptyString City { get; }
    }
}
