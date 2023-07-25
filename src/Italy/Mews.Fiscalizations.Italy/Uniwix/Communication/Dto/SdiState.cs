namespace Mews.Fiscalizations.Italy.Uniwix.Communication.Dto;

public enum SdiState
{
    Pending = 0,
    Delivered = 1,
    DeliveryFailed = 2,
    RejectedBySdi = 3,
    DeliveryImpossible = 4,
    DeadlinePassed = 5,
    Processed = 6,
    AcceptedByClient = 7,
    RejectedByClient = 8
}