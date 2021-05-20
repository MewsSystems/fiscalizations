using Mews.Fiscalization.Greece.Mapper;
using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Result;
using System.Threading.Tasks;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece
{
    public sealed class AadeClient
    {
        public string UserId { get; }
        public string SubscriptionKey { get; }
        private AadeLogger Logger { get; }
        private RestClient RestClient { get; }

        public AadeClient(string userId, string subscriptionKey, AadeEnvironment environment = AadeEnvironment.Production, AadeLogger logger = null)
        {
            UserId = userId;
            SubscriptionKey = subscriptionKey;
            Logger = logger;
            RestClient = new RestClient(userId, subscriptionKey, environment, logger);
        }

        public async Task<SendInvoicesResult> SendInvoicesAsync(ISequenceStartingWithOne<Invoice> invoices)
        {
            var responseDoc = await RestClient.SendRequestAsync(InvoiceDocumentMapper.GetInvoiceDoc(invoices));

            return new SendInvoicesResult(responseDoc);
        }
        
        public Task<CheckUserCredentialsResult> CheckUserCredentialsAsync()
        {
            return RestClient.CheckUserCredentialsAsync();
        }
    }
}
