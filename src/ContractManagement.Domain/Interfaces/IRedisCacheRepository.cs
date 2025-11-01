namespace ContractManagement.Domain.Interfaces
{
    public interface IRedisCacheRepository
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T value);
    }
}
