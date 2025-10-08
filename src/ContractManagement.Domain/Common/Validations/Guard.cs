
using ContractManagement.Domain.Common.Exceptions;

namespace ContractManagement.Domain.Common.Validations
{
    internal static class Guard
    {
        public static void AgainstEmptyGuid(Guid id, string paramName)
        {
            if (id == Guid.Empty)
            {
                throw new DomainException($"{paramName} não pode ser Guid.Empty.");
            }
        }
        public static void AgaintNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new DomainException($"{paramName} não pode ser nulo.");
            }
        }
        public static void Againts<TException>(bool condition, string message) where TException : Exception
        {
            if (condition) throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }
    }
}
