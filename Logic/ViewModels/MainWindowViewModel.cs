using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Models;

namespace Logic.ViewModels;

public class MainWindowViewModel : ViewModelAbstract
{
    private readonly ISettingsService _settingsService;
    private ViewModelAbstract _currentContent = null!;

    public RelayCommand CreateNewBankCommand { get; }
    public RelayCommand EditExistingBankCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    
    public ViewModelAbstract CurrentContent
    {
        get => _currentContent;
        set => SetProperty(ref _currentContent, value);
    }

    public MainWindowViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        CreateNewBankCommand = new RelayCommand(CreateNewBank);
        EditExistingBankCommand = new RelayCommand(EditExistingBank);
        OpenSettingsCommand = new RelayCommand(OpenSettings);
        
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

    private void CreateNewBank()
    {
        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath)) return;
        CurrentContent = new CreateNewBankViewModel();
    }

    private void EditExistingBank()
    {
        CurrentContent = new EditExistingBankViewModel();
    }

    private void OpenSettings()
    {
        CurrentContent = new SelectFolderViewModel(FolderSelected);
    }
}