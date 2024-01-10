using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Logic.Interfaces.Services;
using Logic.ViewModels.Notifications;
using OverloadBankEditor.Views;

namespace OverloadBankEditor.Services;

public class DialogService : IDialogService
{
    public string ShowTextInputDialog(string dialogTitle)
    {
        var activeView = GetActiveView();
        if (activeView is null) return string.Empty;

        var viewModel = new TextInputDialogViewModel(dialogTitle);
        var dialogViewModel = new DialogViewModel(viewModel);

        var dialog = new DialogView
        {
            DataContext = dialogViewModel,
            Owner = activeView
        };

        dialog.ShowDialog();

        return viewModel.UserInput;
    }

    public void ShowOkDialog(string dialogTitle, string dialogContent)
    {
        throw new NotImplementedException();
    }

    public void ShowYesNoDialog(string dialogQuestion, string dialogDescription)
    {
        throw new NotImplementedException();
    }

    private Window? GetActiveView()
    {
        var active = GetActiveWindow();
        return App.Current.Windows.OfType<Window>()
            .SingleOrDefault(window => new WindowInteropHelper(window).Handle == active);
    }
    
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();
}