using LabApp.Views;
using LabLibrary.Contexts;
using LabLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LabApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        
        public App()
        {
            var services = new ServiceCollection();

            services.AddSingleton<PractWorkDbContext>();
            services.AddSingleton<UserService>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<UserDetailsWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }

}
