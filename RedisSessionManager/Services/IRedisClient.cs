using StackExchange.Redis;

namespace RedisSessionManager.Services
{
    public interface IRedisClient
    {
        IDatabase GetDatabase();

        IServer GetServer();
    }
}
