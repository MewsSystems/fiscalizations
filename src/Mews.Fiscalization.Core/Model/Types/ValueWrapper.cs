namespace Mews.Fiscalization.Core.Model
{
    public abstract class ValueWrapper<T>
    {
        protected ValueWrapper(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}