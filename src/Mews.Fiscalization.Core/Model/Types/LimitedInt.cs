namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedInt : ValueWrapper<int, RangeLimitation<int>>
    {
        protected LimitedInt(int value, RangeLimitation<int> limitation)
            : base(value, limitation)
        {
        }
    }
}