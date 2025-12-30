namespace ContractManagement.Domain.Entity
{
    public class Notification(Guid id, string message, string action)
    {
        public Guid Id { get; private set; } = id;
        public string Message { get; private set; } = message;
        public string Action { get; private set; } = action;
    }
}
