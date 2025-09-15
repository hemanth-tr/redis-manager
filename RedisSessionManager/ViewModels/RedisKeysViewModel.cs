using System.Collections.ObjectModel;
using System.Windows.Input;
using RedisSessionManager.Models;
using RedisSessionManager.Services;
using RedisSessionManager.Utils;

namespace RedisSessionManager.ViewModels
{
    public class RedisKeysViewModel : ViewModelBase
    {
        private ObservableCollection<RedisData> _redisData;
        private long _totalSize;
        private double _numberOfKeys;
        public ObservableCollection<RedisData> RedisData
        {
            get => _redisData;
            set
            {
                _redisData = value;
                OnPropertyChanged(nameof(RedisData));
                this.UpdateDashboard();
            }
        }

        public double NumberOfKeys
        {
            get => _numberOfKeys;
            set
            {
                _numberOfKeys = value;
                OnPropertyChanged(nameof(NumberOfKeys));
            }
        }

        public long TotalSize
        {
            get => _totalSize;
            set
            {
                _totalSize = value;
                OnPropertyChanged(nameof(TotalSize));
            }
        }

        public string SearchPattern { get; set; }

        public bool SearchInCurrentList { get; set; }

        public ICommand SearchCommand { get; set; }

        private IRedisManager redisManager;

        public RedisKeysViewModel(IRedisManager redisManager)
        {
            this.RedisData = new ObservableCollection<RedisData>();
            this.redisManager = redisManager ?? throw new ArgumentNullException(nameof(redisManager));
            SearchCommand = new RelayCommand(this.ExecuteSearch);

            this.PopulateData();
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

        private void ExecuteSearch(object param)
        {
            var pattern = this.SearchPattern;
            if (this.SearchInCurrentList)
            {
                var filteredData = this.RedisData.Where(x => x.Key.Contains(pattern, StringComparison.OrdinalIgnoreCase)).ToList();
                this.RedisData = new ObservableCollection<RedisData>(filteredData);
                return;
            }

            var redisData = this.redisManager.GetDataByPattern($"*{pattern}*");
            if (redisData == null || !redisData.Any())
            {
                return;
            }

            this.RedisData = new ObservableCollection<RedisData>(redisData);
        }
    }
}
