using System.Windows;
using Logic.ViewModels.Notifications;

namespace OverloadBankEditor.Views;

public partial class DialogView : Window
{
    public DialogView()
    {
        InitializeComponent();
    }

    private void DialogView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not DialogViewModel viewModel) return;
        viewModel.DialogViewModelAbstract.SendCloseAction(Close);
    }
}