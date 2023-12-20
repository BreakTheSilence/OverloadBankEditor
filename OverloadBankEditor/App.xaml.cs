using System.Windows;
using Logic.Interfaces.Services;
using Logic.Services;
using Logic.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Application = System.Windows.Application;

namespace OverloadBankEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();

        InitializeComponent();
    }
    
    public new static App Current => (App)Application.Current;
    
    public IServiceProvider Services { get; }
    
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<ISettingsService, SettingsService>();
        services.AddTransient<IBankLoaderService, BankLoaderService>();
        services.AddTransient<IBankManagingService, BankManagingService>();

        return services.BuildServiceProvider();
    }
}