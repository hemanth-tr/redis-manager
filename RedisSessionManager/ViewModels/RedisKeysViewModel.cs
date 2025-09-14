using System.Collections.ObjectModel;
using RedisSessionManager.Models;
using RedisSessionManager.Services;

namespace RedisSessionManager.ViewModels
{
    public class RedisKeysViewModel : ViewModelBase
    {
        public ObservableCollection<RedisData> RedisData { get; set; }

        public double NumberOfKeys { get; set; }

        public long TotalSize { get; set; }

        private IRedisManager redisManager;

        public RedisKeysViewModel(IRedisManager redisManager)
        {
            this.RedisData = new ObservableCollection<RedisData>();
            this.redisManager = redisManager ?? throw new ArgumentNullException(nameof(redisManager));

            this.PopulateData();
            this.UpdateDashboard();
        }

        private void UpdateDashboard()
        {
            this.NumberOfKeys = this.RedisData.Count;
            this.TotalSize = this.RedisData.Sum(x => x.Size);
        }

        private void PopulateData()
        {
            var redisData = this.redisManager.GetAllData();
            this.RedisData = new ObservableCollection<RedisData>(redisData);
        }
    }
}
