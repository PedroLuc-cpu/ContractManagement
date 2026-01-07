namespace ContractManagement.Infrastructure.Pagamento
{
    public class StripeOptions
    {
        public required string PubKey { get; init; }
        public required string SecretKey { get; init; }
        public required string WebhookSecret { get; init; }
        public required string FrontEndUrl { get; init; }
    }
}
