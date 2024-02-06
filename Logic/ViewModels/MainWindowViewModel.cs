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
    private readonly IDialogService _dialogService;
    private ContentPageViewModelAbstract _currentContent = null!;
    private string _titleText;
    private Func<string> _pickBankFileFunction;

    public RelayCommand ProcessPresetsCommand { get; }
    public RelayCommand EditExistingBankCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    public RelayCommand ExportBankCommand { get; }
    
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


    public MainWindowViewModel(ISettingsService settingsService, 
        IBankManagingService bankManagingService, IDialogService dialogService)
    {
        _settingsService = settingsService;
        _bankManagingService = bankManagingService;
        _dialogService = dialogService;
        ProcessPresetsCommand = new RelayCommand(ProcessPresets);
        EditExistingBankCommand = new RelayCommand(EditExistingBank);
        OpenSettingsCommand = new RelayCommand(OpenSettings);
        ExportBankCommand = new RelayCommand(ExportBank);
        
        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath))
        {
            CurrentContent = new SettingsPageViewModel(FolderSelected, _dialogService);
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

    private void ProcessPresets()
    {
        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath)) return;
        CurrentContent = new ProcessPresetsViewModel(_pickBankFileFunction, _bankManagingService, _dialogService);
    }

    private void EditExistingBank()
    {
        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath)) return;
        CurrentContent = new EditExistingBankViewModel(_pickBankFileFunction, _bankManagingService, _dialogService);
    }

    private void OpenSettings()
    {
        CurrentContent = new SettingsPageViewModel(FolderSelected, _dialogService);
    }

    private void ExportBank()
    {
        if (string.IsNullOrWhiteSpace(_settingsService.LoadSettings().WorkingDirectoryPath)) return;
        CurrentContent = new ExportBankViewModel(_bankManagingService, _dialogService);
    }
}