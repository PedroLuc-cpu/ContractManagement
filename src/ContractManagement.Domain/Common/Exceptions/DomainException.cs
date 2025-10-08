namespace ContractManagement.Domain.Common.Exceptions
{
    public class DomainException(string message) : Exception(message)
    {
        public static void When(bool hasError, string errorMesssage)
        {
            if (hasError)
            {
                throw new DomainException(errorMesssage);
            }
        }

    }
}
