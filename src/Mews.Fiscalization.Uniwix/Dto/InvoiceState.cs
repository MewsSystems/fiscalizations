namespace Mews.Fiscalization.Uniwix.Dto
{
    public class InvoiceState
    {
        public InvoiceState(string fileId, SdiState sdiState, string message)
        {
            FileId = fileId;
            SdiState = sdiState;
            Message = message;
        }

        public string FileId { get; }

        public SdiState SdiState { get; }

        public string Message { get; }
    }
}