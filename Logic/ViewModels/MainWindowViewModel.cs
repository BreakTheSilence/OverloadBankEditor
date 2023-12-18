using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels;

public class MainWindowViewModel : ViewModelAbstract
{
    private ViewModelAbstract _currentContent;
    private string _workingDirectoryPath;
    
    public RelayCommand LoadedCommand { get; set; }
    public RelayCommand TetsCommand { get; set; }

    public ViewModelAbstract CurrentContent
    {
        get => _currentContent;
        set => SetProperty(ref _currentContent, value);
    }

    public MainWindowViewModel()
    {
        CurrentContent = new StartViewModel();
        TetsCommand = new RelayCommand(Tets);
    }

    private void Tets()
    {
        CurrentContent = new SelectFolderViewModel();
    }
}