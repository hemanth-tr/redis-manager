using RedisSessionManager.Models;
using StackExchange.Redis;

namespace RedisSessionManager.Services
{
    public class RedisManager : IRedisManager
    {
        private IRedisClient _redisClient;
        private IDatabase _database;
        private IServer _server;

        public RedisManager(IRedisClient redisClient)
        {
            this._redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            this.Initialize();
        }

        public string Get(string key)
        {
            return this._database.StringGet(key);
        }

        public IEnumerable<RedisData> GetAllData()
        {
            var result = new List<RedisData>();

            foreach (var key in _server.Keys(pattern: "*"))
            {
                var value = _database.StringGet(key);
                long size = this.GetSize(key);

                result.Add(new RedisData { Key = key, Value = value, Size = size });
            }

            return result;
        }

        public async Task<IEnumerable<RedisData>> GetAllDataAsync()
        {
            var result = new List<RedisData>();

            foreach (var key in _server.Keys(pattern: "*"))
            {
                var value = await _database.StringGetAsync(key);
                long size = this.GetSize(key);

                result.Add(new RedisData { Key = key, Value = value, Size = size });
            }

            return result;
        }

        public IEnumerable<string> GetAllKeys()
        {
            throw new NotImplementedException();
        }

        private long GetSize(string key)
        {
            long size = -1;
            RedisResult usageResult = null;
            try
            {
                usageResult = _database.Execute("MEMORY", "USAGE", key);
                size = (long)usageResult;
            }
            catch (Exception)
            {

            }

            return size;
        }

        private void Initialize()
        {
            this._database = this._redisClient.GetDatabase();
            this._server = this._redisClient.GetServer();
        }
    }
}
