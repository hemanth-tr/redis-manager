using RedisSessionManager.Models;

namespace RedisSessionManager.Services
{
    public class RedisManager : IRedisManager
    {
        private List<RedisData> _redisDatas;

        public RedisManager()
        {
            _redisDatas = new List<RedisData>();
            this.PopulateData();
        }

        public string? Get(string key)
        {
            var data = _redisDatas.FirstOrDefault(x => x.Key == key);
            return data?.Value;
        }

        public IEnumerable<RedisData> GetAllData()
        {
            return _redisDatas;
        }

        public IEnumerable<string> GetAllKeys()
        {
            return _redisDatas.Select(x => x.Key);
        }

        private void PopulateData()
        {
            _redisDatas.Add(new RedisData { Key = "Name", Value = "Hemanth" });
            _redisDatas.Add(new RedisData { Key = "Age", Value = "31" });
            _redisDatas.Add(new RedisData { Key = "Company", Value = "Fiserv" });
        }
    }
}
