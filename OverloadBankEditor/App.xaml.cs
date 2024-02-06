using Logic.Interfaces.Services;
using Logic.Services;
using Logic.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Ookii.Dialogs.Wpf;
using OverloadBankEditor.Services;
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
    
    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<ISettingsService, SettingsService>();
        services.AddTransient<IBankLoaderService, BankLoaderService>();
        services.AddTransient<IBankManagingService, BankManagingService>();
        services.AddTransient<IDialogService>(_ => new DialogService(SelectFolder, ShowOpenFileDialog));

        return services.BuildServiceProvider();
    }

    private string SelectFolder()
    {
        var dialog = new VistaFolderBrowserDialog
        {
            Description = "Please select a folder.",
            UseDescriptionForTitle = true // This applies to the Vista style dialog only, not the old dialog.
        };

        if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
        {
            MessageBox.Show("Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
        }

        dialog.ShowDialog();

        return dialog.SelectedPath;
    }
    
    private string ShowOpenFileDialog()
    {
        // As of .Net 3.5 SP1, WPF's Microsoft.Win32.OpenFileDialog class still uses the old style
        var dialog = new VistaOpenFileDialog
        {
            Filter = "OVB Files (*.ovb)|*.ovb"
        };
        if( !VistaFileDialog.IsVistaFileDialogSupported )
            MessageBox.Show("Because you are not using Windows Vista or later, the regular open file dialog will be used. Please use Windows Vista to see the new dialog.", "Sample open file dialog");

        dialog.ShowDialog();

        return dialog.FileName;
    }
}