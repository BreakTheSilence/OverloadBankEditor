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
        DataContext = App.Current.Services.GetRequiredService<MainWindowViewModel>();
        InitializeComponent();
    }
}