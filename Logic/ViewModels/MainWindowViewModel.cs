using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Pages;
using Models;

namespace Logic.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private readonly IBankManagingService _bankManagingService;
    private ContentPageViewModelAbstract _currentContent = null!;
    private string _titleText;
    private Func<string> _pickBankFileFunction;

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


    public MainWindowViewModel(ISettingsService settingsService, IBankManagingService bankManagingService)
    {
        _settingsService = settingsService;
        _bankManagingService = bankManagingService;
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

    public void SetupMvvmLogic(Func<string> pickBankFileFunction)
    {
        _pickBankFileFunction = pickBankFileFunction;
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
        CurrentContent = new EditExistingBankViewModel(_pickBankFileFunction, _bankManagingService);
    }

    private void OpenSettings()
    {
        CurrentContent = new SettingsPageViewModel(FolderSelected);
    }
}