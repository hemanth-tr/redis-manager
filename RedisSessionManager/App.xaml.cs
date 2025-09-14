using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using RedisSessionManager.Services;
using RedisSessionManager.ViewModels;

namespace RedisSessionManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Register services and viewmodels
            services.AddSingleton<IRedisManager, RedisManager>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<RedisKeysViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
            };
            mainWindow.Show();

            base.OnStartup(e);
        }
    }

}
