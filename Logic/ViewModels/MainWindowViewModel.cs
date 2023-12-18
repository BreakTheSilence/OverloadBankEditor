using Logic.Interfaces;
using Logic.Interfaces.Services;
using Models;

namespace Logic.ViewModels;

public class MainWindowViewModel : ViewModelAbstract
{
    private readonly ISettingsService _settingsService;
    private ViewModelAbstract _currentContent = null!;
    
    public ViewModelAbstract CurrentContent
    {
        get => _currentContent;
        set => SetProperty(ref _currentContent, value);
    }

    public MainWindowViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;


        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath))
        {
            CurrentContent = new SelectFolderViewModel(FolderSelected);
        }
        else
        {
            CurrentContent = new StartViewModel();
        }
    }

    private void FolderSelected(string folderPath)
    {
        _settingsService.SaveSettings(new Settings()
        {
            WorkingDirectoryPath = folderPath
        });
        CurrentContent = new StartViewModel();
    }
}