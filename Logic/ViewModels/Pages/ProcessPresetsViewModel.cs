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
    private BankListViewModel? _editableBankListViewModel;
    private PresetListViewModel? _readonlyPresetListViewModel;
    private PresetListViewModel? _editablePresetListViewModel;
    private string _readonlyInfoString;
    private string _editableInfoString;

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
    public BankListViewModel? EditableBankListViewModel
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
    public PresetListViewModel? EditablePresetListViewModel
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
    public string EditableInfoString
    {
        get => _editableInfoString;
        set
        {
            if (value == _editableInfoString) return;
            _editableInfoString = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ReselectReadonlyBankCommand { get; }
    public RelayCommand ReselectEditableBankCommand { get; }

    public ProcessPresetsViewModel(Func<string> pickBankFileFunction, IBankManagingService bankManagingService,
        IDialogService dialogService)
    {
        _pickBankFileFunction = pickBankFileFunction;
        _bankManagingService = bankManagingService;
        _dialogService = dialogService;

        ReselectReadonlyBankCommand = new RelayCommand(ReselectReadonlyBank);
        ReselectEditableBankCommand = new RelayCommand(ReselectEditableBank);
        
        LoadReadonlyBankList();
        LoadEditableBankList();
    }
    
    private void ReadonlyBankSelected(BankViewModel bankViewModel)
    {
        ReadonlyBankListViewModel = null;
        ReadonlyPresetListViewModel = new PresetListViewModel(bankViewModel, _dialogService, 
            ReadonlyPresetDeletedFromBank, ReadonlyPresetsUpdated, false);
        ReadonlyInfoString = $"Presets: {ReadonlyPresetListViewModel.PresetViewModels.Count}/256";
    }
    
    private void EditableBankSelected(BankViewModel bankViewModel)
    {
        EditableBankListViewModel = null;
        
        EditablePresetListViewModel = new PresetListViewModel(bankViewModel, _dialogService, 
            EditablePresetDeletedFromBank, EditablePresetsUpdated);
        
        EditableInfoString = $"Presets: {EditablePresetListViewModel.PresetViewModels.Count}/256";
    }
    
    private void ReadonlyPresetDeletedFromBank(BankViewModel bank, PresetViewModel deletedPreset)
    {
        // ignored
    }
    private void EditablePresetDeletedFromBank(BankViewModel bank, PresetViewModel deletedPreset)
    {
        bank.DeletePresetCommand.Execute(deletedPreset.Preset);
        _bankManagingService.UpdateBank(bank.Bank);
        EditableBankSelected(bank);
    }
    
    private void ReadonlyPresetsUpdated(BankViewModel bankViewModel)
    {
        // ignored
    }
    private void EditablePresetsUpdated(BankViewModel bankViewModel)
    {
        throw new NotImplementedException();
    }

    private void ReselectReadonlyBank()
    {
        LoadReadonlyBankList();
    }

    private void ReselectEditableBank()
    {
        LoadEditableBankList();
    }

    private void LoadReadonlyBankList()
    {
        ReadonlyPresetListViewModel = null;
        ReadonlyBankListViewModel = new BankListViewModel(_bankManagingService, ReadonlyBankSelected);
        ReadonlyInfoString = $"Banks: {ReadonlyBankListViewModel.BankViewModels.Count}";
    }

    private void LoadEditableBankList()
    {
        EditablePresetListViewModel = null;
        EditableBankListViewModel = new BankListViewModel(_bankManagingService, EditableBankSelected);
        EditableInfoString = $"Banks: {EditableBankListViewModel.BankViewModels.Count}";
    }

    private List<PresetViewModel> GetSelectedPresets()
    {
        
        
        return null;
    }
}