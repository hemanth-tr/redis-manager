using RedisSessionManager.ViewModels;

namespace RedisSessionManager
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        public MainWindowViewModel(RedisKeysViewModel redisKeysViewModel)
        {
            this.CurrentViewModel = redisKeysViewModel;
        }
    }
}
