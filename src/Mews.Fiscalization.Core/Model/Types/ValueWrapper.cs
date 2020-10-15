using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Core.Model
{
    public abstract class ValueWrapper<T, TLimitation>
        where TLimitation : class, ILimitation<T>
    {
        protected ValueWrapper(T value, TLimitation limitation)
            : this(value, limitation.ToEnumerable())
        {
        }

        protected ValueWrapper(T value, IEnumerable<TLimitation> limitations)
        {
            Value = value;

            foreach (var limitation in limitations)
            {
                limitation.CheckValidity(value);
            }
        }

        protected static bool IsValid(T value, TLimitation limitation)
        {
            return limitation.IsValid(value);
        }

        protected static bool IsValid(T value, IEnumerable<TLimitation> limitations)
        {
            return limitations.All(l => l.IsValid(value));
        }

        public T Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ValueWrapper<T, ILimitation<T>> other && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}