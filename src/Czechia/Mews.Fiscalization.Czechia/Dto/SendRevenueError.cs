namespace Mews.Eet.Dto
{
    public class SendRevenueError
    {
        public SendRevenueError(Fault reason)
        {
            Reason = reason;
        }

        public Fault Reason { get; }
    }
}
