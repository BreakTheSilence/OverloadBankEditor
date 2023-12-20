using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels.Pages;

public class SettingsPageViewModel : ContentPageViewModelAbstract
{
    private Func<string> _selectFolderAction = null!;
    private readonly Action<string> _folderSelectedAction;

    public RelayCommand SelectFolderCommand { get; set; }
    
    public override string PageTitle { get; }
    
    public SettingsPageViewModel(Action<string> folderSelectedAction)
    {
        _folderSelectedAction = folderSelectedAction;
        SelectFolderCommand = new RelayCommand(SelectFolder);
        PageTitle = "Settings";
    }

    public void SetupFolderSelection(Func<string> selectFolder)
    {
        _selectFolderAction = selectFolder;
    }

    private void SelectFolder()
    {
        var selectedFolder = _selectFolderAction();
        if (string.IsNullOrWhiteSpace(selectedFolder)) return;
        _folderSelectedAction?.Invoke(selectedFolder);
    }
}