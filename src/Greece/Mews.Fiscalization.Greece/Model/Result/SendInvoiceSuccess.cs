namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceSuccess
    {
        public SendInvoiceSuccess(string invoiceIdentifier, long invoiceRegistrationNumber, bool invoiceRegistrationNumberSpecified)
        {
            InvoiceIdentifier = invoiceIdentifier;
            if (invoiceRegistrationNumberSpecified)
            {
                InvoiceRegistrationNumber = invoiceRegistrationNumber;
            }
        }

        public string InvoiceIdentifier { get; }

        public long? InvoiceRegistrationNumber { get; }
    }
}
