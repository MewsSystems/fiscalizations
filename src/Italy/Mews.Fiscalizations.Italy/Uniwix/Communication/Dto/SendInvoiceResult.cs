namespace Mews.Fiscalizations.Italy.Uniwix.Communication.Dto
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