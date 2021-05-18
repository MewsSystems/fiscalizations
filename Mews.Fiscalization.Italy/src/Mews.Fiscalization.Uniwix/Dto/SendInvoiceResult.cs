namespace Mews.Fiscalization.Uniwix.Dto
{
    public class SendInvoiceResult
    {
        public SendInvoiceResult(string fileId, string message)
        {
            FileId = fileId;
            Message = message;
        }

        public string FileId { get; }

        public string Message { get; }
    }
}