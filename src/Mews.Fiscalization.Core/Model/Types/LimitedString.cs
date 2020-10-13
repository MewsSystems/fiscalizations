using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedString : ValueWrapper<string>
    {
        protected LimitedString(string value, StringLimitation limitation)
            : this(value, limitations: limitation.ToEnumerable())
        {
            limitation.CheckValidity(value);
        }

        protected LimitedString(string value, IEnumerable<StringLimitation> limitations)
            : base(value)
        {
            foreach (var limitation in limitations)
            {
                limitation.CheckValidity(value);
            }
        }

        protected static bool IsValid(string value, StringLimitation limitation)
        {
            return IsValid(value, limitation.ToEnumerable());
        }

        protected static bool IsValid(string value, IEnumerable<StringLimitation> limitations)
        {
            return limitations.All(l => l.IsValid(value));
        }
    }
}
