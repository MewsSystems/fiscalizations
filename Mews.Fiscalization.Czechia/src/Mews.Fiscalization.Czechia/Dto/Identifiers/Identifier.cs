namespace Mews.Eet.Dto.Identifiers
{
    public abstract class Identifier<T>
    {
        protected Identifier(T value)
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
