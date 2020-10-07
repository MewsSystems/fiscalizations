using System;

namespace Mews.Fiscalization.Core.Model
{
    public class DecimalLimitation : Limitation<decimal>
    {
        public DecimalLimitation(decimal? minimum = null, decimal? maximum = null, int? maxDecimalPlaces = null)
            : base(minimum, maximum)
        {
            MaxDecimalPlaces = maxDecimalPlaces;
        }

        public int? MaxDecimalPlaces { get; }

        public override bool IsValid(decimal value)
        {
            return base.IsValid(value) && PrecisionIsValid(value);
        }

        internal void CheckValidity(decimal value)
        {
           CheckValidity(value, label: "value");
        }

        internal override void CheckValidity(decimal value, string label)
        {
            base.CheckValidity(value, label);
            if (!PrecisionIsValid(value))
            {
                throw new ArgumentException($"Highest possible precision is {MaxDecimalPlaces} decimal places.");
            }
        }

        private bool PrecisionIsValid(decimal value)
        {
            if (MaxDecimalPlaces.HasValue)
            {
                var divisor = (decimal)Math.Pow(0.1, MaxDecimalPlaces.Value);
                return value % divisor == 0;
            }

            return true;
        }
    }
}