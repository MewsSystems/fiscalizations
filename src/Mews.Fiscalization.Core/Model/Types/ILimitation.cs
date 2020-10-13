namespace Mews.Fiscalization.Core.Model
{
    public interface ILimitation<T>
    {
        bool IsValid(T value);
        void CheckValidity(T value);
    }
}