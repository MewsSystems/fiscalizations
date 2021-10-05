using System;

namespace Mews.Fiscalizations.Germany.V2.Model
{
    public enum TssState
    {
        Created,
        Uninitialized,
        Initialized,
        Disabled,
        Deleted
    }

    [Flags]
    public enum TssStates
    {
        Created = 1 << 0,
        Uninitialized = 1 << 1,
        Initialized = 1 << 2,
        Disabled = 1 << 3,
        Deleted = 1 << 4
    }
}