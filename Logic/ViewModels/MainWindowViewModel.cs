using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Models;

namespace Logic.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private ContentPageViewModelAbstract _currentContent = null!;
    private string _titleText;

    public RelayCommand CreateNewBankCommand { get; }
    public RelayCommand EditExistingBankCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    
    public ContentPageViewModelAbstract CurrentContent
    {
        get => _currentContent;
        set
        {
            SetProperty(ref _currentContent, value);
            TitleText = CurrentContent.PageTitle;
        }
    }

    public string TitleText
    {
        get => _titleText;
        set => SetProperty(ref _titleText, value);
    }


    public MainWindowViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        CreateNewBankCommand = new RelayCommand(CreateNewBank);
        EditExistingBankCommand = new RelayCommand(EditExistingBank);
        OpenSettingsCommand = new RelayCommand(OpenSettings);
        
        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath))
        {
            CurrentContent = new SettingsPageViewModel(FolderSelected);
        }
        else
        {
            CurrentContent = new StartContentPageViewModel();
        }
    }

    private void FolderSelected(string folderPath)
    {
        _settingsService.SaveSettings(new Settings()
        {
            WorkingDirectoryPath = folderPath
        });
        CurrentContent = new StartContentPageViewModel();
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
        CurrentContent = new SettingsPageViewModel(FolderSelected);
    }
}