namespace Core.Services.Storage
{
    public interface IStorage
    {
        T Get<T>(string key, T defaultValue = default);
        void Set<T>(string key, T value);
        bool HasKey(string key);
        void DeleteKey(string key);
        void DeleteAll();
        void Save();
    }
}
