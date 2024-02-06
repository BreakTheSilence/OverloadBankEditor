using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Logic.Interfaces.Services;
using Logic.ViewModels.Notifications;
using OverloadBankEditor.Views;

namespace OverloadBankEditor.Services;

public class DialogService : IDialogService
{
    private readonly Func<string> _selectFolderFunction;
    private readonly Func<string> _selectFileFunction;

    public DialogService(Func<string> selectFolderFunction, Func<string> selectFileFunction)
    {
        _selectFolderFunction = selectFolderFunction;
        _selectFileFunction = selectFileFunction;
    }
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
        var activeView = GetActiveView();
        if (activeView is null) return;

        var viewModel = new NotificationDialogViewModel(dialogTitle, dialogContent);
        var dialogViewModel = new DialogViewModel(viewModel);

        var dialog = new DialogView
        {
            DataContext = dialogViewModel,
            Owner = activeView
        };

        dialog.ShowDialog();
    }

    public bool ShowYesNoDialog(string dialogQuestion, string dialogDescription)
    {
        var activeView = GetActiveView();
        if (activeView is null) return false;

        var viewModel = new YesNoDialogViewModel(dialogQuestion, dialogDescription);
        var dialogViewModel = new DialogViewModel(viewModel);

        var dialog = new DialogView
        {
            DataContext = dialogViewModel,
            Owner = activeView
        };

        dialog.ShowDialog();

        return viewModel.UserResponse;
    }

    public string ShowFilePickDialog()
    {
        var selectedFile = _selectFileFunction();
        return string.IsNullOrEmpty(selectedFile) 
            ? string.Empty 
            : selectedFile;
    }

    public string ShowFolderSelectDialog()
    {
        var selectedFolder = _selectFolderFunction();
        return string.IsNullOrEmpty(selectedFolder) 
            ? string.Empty 
            : selectedFolder;
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