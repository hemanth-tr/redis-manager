using System.Configuration;
using StackExchange.Redis;

namespace RedisSessionManager.Services
{
    public class RedisClient : IRedisClient
    {
        private ConnectionMultiplexer _redis;
        private IDatabase _db;
        private IServer _server;
        private string _connectionString;
        private string _localhost = "localhost:6379";

        public RedisClient()
        {
            this.Initialize();
        }

        public IDatabase GetDatabase()
        {
            return this._db;
        }

        public IServer GetServer()
        {
            return this._server;
        }

        private void Initialize()
        {
            _connectionString = this.GetConnectionString();
            _redis = ConnectionMultiplexer.Connect(_connectionString);
            _db = _redis.GetDatabase();
            _server = _redis.GetServer(_connectionString);
        }

        private string GetConnectionString()
        {
            if (_connectionString != null)
            {
                return _connectionString;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["redis"]?.ToString() ?? _localhost;
            return connectionString;
        }
    }
}
