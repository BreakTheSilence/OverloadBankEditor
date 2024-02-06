using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;

namespace Logic.ViewModels.Pages;

public class SettingsPageViewModel : ContentPageViewModelAbstract
{
    private readonly Action<string> _folderSelectedAction;
    private readonly IDialogService _dialogService;

    public RelayCommand SelectFolderCommand { get; set; }
    
    public override string PageTitle { get; }
    
    public SettingsPageViewModel(Action<string> folderSelectedAction, IDialogService dialogService)
    {
        _folderSelectedAction = folderSelectedAction;
        _dialogService = dialogService;
        SelectFolderCommand = new RelayCommand(SelectFolder);
        PageTitle = "Settings";
    }

    private void SelectFolder()
    {
        var selectedFolder = _dialogService.ShowFolderSelectDialog();
        if (string.IsNullOrWhiteSpace(selectedFolder)) return;
        _folderSelectedAction?.Invoke(selectedFolder);
    }
}