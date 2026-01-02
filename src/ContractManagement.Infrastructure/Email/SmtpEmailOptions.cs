namespace ContractManagement.Infrastructure.Email
{
    public class SmtpEmailOptions
    {
        public required string Host { get; init; }
        public required int Port { get; init; }
        public required string User { get; init; }
        public required string Password { get; init; }
        public required string FromEmail { get; init; }
        public bool EnableSsl { get; init; } = true;
    }
}
