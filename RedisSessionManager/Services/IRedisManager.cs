using RedisSessionManager.Models;

namespace RedisSessionManager.Services
{
    public interface IRedisManager
    {
        string Get(string key);
        IEnumerable<string> GetAllKeys();
        IEnumerable<RedisData> GetAllData();
    }
}
