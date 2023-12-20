using System.Windows;
using Logic.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace OverloadBankEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        var viewModel = App.Current.Services.GetRequiredService<MainWindowViewModel>();
        viewModel.SetupMvvmLogic(SelectBankFile);
        DataContext = viewModel;
        InitializeComponent();
    }
    
    private string SelectBankFile()
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "OVB Files (*.ovb)|*.ovb";
        dialog.Title = "Select a bank";
        var result = dialog.ShowDialog();
        return result != System.Windows.Forms.DialogResult.OK ? string.Empty : dialog.FileName;
    }
}