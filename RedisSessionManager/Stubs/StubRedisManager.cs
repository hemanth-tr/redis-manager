using RedisSessionManager.Models;
using RedisSessionManager.Services;

namespace RedisSessionManager.Stubs
{
    public class StubRedisManager : IRedisManager
    {
        private List<RedisData> _redisDatas;

        public StubRedisManager()
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

        public Task<IEnumerable<RedisData>> GetAllDataAsync()
        {
            return Task.FromResult<IEnumerable<RedisData>>(_redisDatas);
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

            foreach (var data in _redisDatas)
            {
                data.Size = data.Value.Length;
            }
        }
    }
}
