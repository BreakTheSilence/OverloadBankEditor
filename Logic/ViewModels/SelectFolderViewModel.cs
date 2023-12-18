using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels;

public class SelectFolderViewModel : ViewModelAbstract
{
    private Func<string> _selectFolderAction;

    public RelayCommand SelectFolderCommand { get; set; }
    public SelectFolderViewModel()
    {
        SelectFolderCommand = new RelayCommand(SelectFolder);
    }

    public void SetupFolderSelection(Func<string> selectFolder)
    {
        _selectFolderAction = selectFolder;
    }

    private void SelectFolder()
    {
        var selectedFolder = _selectFolderAction();
    }
}