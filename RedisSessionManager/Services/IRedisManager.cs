using RedisSessionManager.Models;

namespace RedisSessionManager.Services
{
    public interface IRedisManager
    {
        string Get(string key);
        IEnumerable<string> GetAllKeys();
        IEnumerable<RedisData> GetAllData();
        Task<IEnumerable<RedisData>> GetAllDataAsync();
        IEnumerable<RedisData> GetDataByPattern(string pattern);
        Task<IEnumerable<RedisData>> GetDataByPatternAsync(string pattern);
    }
}
