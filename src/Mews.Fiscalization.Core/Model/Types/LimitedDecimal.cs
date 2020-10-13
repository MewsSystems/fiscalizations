using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedDecimal : ValueWrapper<decimal, DecimalLimitation>
    {
        protected LimitedDecimal(decimal value, DecimalLimitation limitation)
            : base(value, limitation)
        {
        }

        protected LimitedDecimal(decimal value, IEnumerable<DecimalLimitation> limitations)
            : base(value, limitations)
        {
        }
    }
}