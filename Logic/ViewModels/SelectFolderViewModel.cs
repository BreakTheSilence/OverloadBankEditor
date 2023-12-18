using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels;

public class SelectFolderViewModel : ViewModelAbstract
{
    private Func<string> _selectFolderAction = null!;
    private readonly Action<string> _folderSelectedAction;

    public RelayCommand SelectFolderCommand { get; set; }
    public SelectFolderViewModel(Action<string> folderSelectedAction)
    {
        _folderSelectedAction = folderSelectedAction;
        SelectFolderCommand = new RelayCommand(SelectFolder);
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