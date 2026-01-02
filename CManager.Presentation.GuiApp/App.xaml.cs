using CManager.Presentation.GuiApp.ViewModels;
using CManager.Presentation.GuiApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CManager.Presentation.GuiApp;

    public partial class App : Application
    {
        private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ActivitiesViewModel>();
                services.AddTransient<ActivitiesView>();

                services.AddTransient<AddActivityViewModel>();
                services.AddTransient<AddActivityView>();

            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
