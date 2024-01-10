using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Controls;
using Logic.ViewModels.Models;

namespace Logic.ViewModels.Pages;

public class ProcessPresetsViewModel : ContentPageViewModelAbstract
{
    private readonly Func<string> _pickBankFileFunction;
    private readonly IBankManagingService _bankManagingService;
    private readonly IDialogService _dialogService;
    
    private BankListViewModel? _readonlyBankListViewModel;
    private BankListViewModel _editableBankListViewModel;
    private PresetListViewModel? _readonlyPresetListViewModel;
    private PresetListViewModel _editablePresetListViewModel;
    private string _readonlyInfoString;

    public override string PageTitle => "Process Presets";

    public BankListViewModel? ReadonlyBankListViewModel
    {
        get => _readonlyBankListViewModel;
        set
        {
            if (Equals(value, _readonlyBankListViewModel)) return;
            _readonlyBankListViewModel = value;
            OnPropertyChanged();
        }
    }
    public BankListViewModel EditableBankListViewModel
    {
        get => _editableBankListViewModel;
        set
        {
            if (Equals(value, _editableBankListViewModel)) return;
            _editableBankListViewModel = value;
            OnPropertyChanged();
        }
    }
    public PresetListViewModel? ReadonlyPresetListViewModel
    {
        get => _readonlyPresetListViewModel;
        set
        {
            if (Equals(value, _readonlyPresetListViewModel)) return;
            _readonlyPresetListViewModel = value;
            OnPropertyChanged();
        }
    }
    public PresetListViewModel EditablePresetListViewModel
    {
        get => _editablePresetListViewModel;
        set
        {
            if (Equals(value, _editablePresetListViewModel)) return;
            _editablePresetListViewModel = value;
            OnPropertyChanged();
        }
    }

    public string ReadonlyInfoString
    {
        get => _readonlyInfoString;
        set
        {
            if (value == _readonlyInfoString) return;
            _readonlyInfoString = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ReselectReadonlyBankCommand { get; }

    public ProcessPresetsViewModel(Func<string> pickBankFileFunction, IBankManagingService bankManagingService,
        IDialogService dialogService)
    {
        _pickBankFileFunction = pickBankFileFunction;
        _bankManagingService = bankManagingService;
        _dialogService = dialogService;

        ReselectReadonlyBankCommand = new RelayCommand(ReselectReadonlyBank);
        
        LoadReadonlyBankList();
    }
    
    private void ReadonlyBankSelected(BankViewModel bankViewModel)
    {
        ReadonlyBankListViewModel = null;
        ReadonlyPresetListViewModel = new PresetListViewModel(bankViewModel, _dialogService, 
            ReadonlyPresetDeletedFromBank, ReadonlyPresetsUpdated, false);
        ReadonlyInfoString = $"Presets: {ReadonlyPresetListViewModel.PresetViewModels.Count}/256";
    }
    
    private void ReadonlyPresetDeletedFromBank(BankViewModel bank, PresetViewModel deletedPreset)
    {
        // ignored
    }
    
    private void ReadonlyPresetsUpdated(BankViewModel bankViewModel)
    {
        // ignored
    }

    private void ReselectReadonlyBank()
    {
        LoadReadonlyBankList();
    }

    private void LoadReadonlyBankList()
    {
        ReadonlyPresetListViewModel = null;
        ReadonlyBankListViewModel = new BankListViewModel(_bankManagingService, ReadonlyBankSelected);
        ReadonlyInfoString = $"Banks: {ReadonlyBankListViewModel.BankViewModels.Count}";
    }
}