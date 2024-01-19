using System.IO;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Controls;
using Logic.ViewModels.Models;

namespace Logic.ViewModels.Pages;

public class EditExistingBankViewModel : ContentPageViewModelAbstract
{
    private readonly Func<string> _pickBankFileFunction;
    private readonly IBankManagingService _bankManagingService;
    private readonly IDialogService _dialogService;
    private BankListViewModel _bankListViewModel;
    private PresetListViewModel _presetListViewModel;
    public override string PageTitle { get; }
    public RelayCommand AddBankFromFileCommand { get; }
    public RelayCommand CreateNewBankCommand { get; }
    public RelayCommand DeleteAllPresetsCommand { get; }
    public RelayCommand DeleteSelectedPresetsCommand { get; }

    public BankListViewModel BankListViewModel
    {
        get => _bankListViewModel;
        private set => SetProperty(ref _bankListViewModel, value);
    }

    public PresetListViewModel PresetListViewModel
    {
        get => _presetListViewModel;
        private set => SetProperty(ref _presetListViewModel, value);
    }

    public EditExistingBankViewModel(Func<string> pickBankFileFunction, IBankManagingService bankManagingService,
        IDialogService dialogService)
    {
        _pickBankFileFunction = pickBankFileFunction;
        _bankManagingService = bankManagingService;
        _dialogService = dialogService;
        PageTitle = "Edit Existing Bank";
        AddBankFromFileCommand = new RelayCommand(AddBankFromFile);
        CreateNewBankCommand = new RelayCommand(CreateNewBank);
        DeleteAllPresetsCommand = new RelayCommand(DeleteAllPresets);
        DeleteSelectedPresetsCommand = new RelayCommand(DeleteSelectedPresets);
        ReloadBankList();
    }
    
    private void AddBankFromFile()
    {
        var filePath = _pickBankFileFunction();
        if (string.IsNullOrWhiteSpace(filePath)) return;
        if (!File.Exists(filePath)) return;
        _bankManagingService.AddBankFromPath(filePath);
        ReloadBankList();
    }

    private void CreateNewBank()
    {
        var bankName = _dialogService.ShowTextInputDialog("Enter a name for the new bank");
        if (string.IsNullOrWhiteSpace(bankName)) return;
        _bankManagingService.CreateNewBank(bankName);
        ReloadBankList();
    }

    private void ReloadBankList()
    {
        BankListViewModel = new BankListViewModel(_bankManagingService, BankSelected, BankDeleted);
    }

    private void ReloadPresetList(BankViewModel bankViewModel)
    {
        PresetListViewModel = new PresetListViewModel(bankViewModel, _dialogService, 
            PresetDeletedFromBank, PresetsUpdated);
    }

    private void BankSelected(BankViewModel bankViewModel)
    {
        ReloadPresetList(bankViewModel);
    }

    private void BankDeleted(BankViewModel bankViewModel)
    {
        if (PresetListViewModel?.DisplayedBank is null) return;
        if (bankViewModel.Equals(PresetListViewModel.DisplayedBank))
        {
            PresetListViewModel = null!;
        }
    }

    private void PresetDeletedFromBank(BankViewModel bank, PresetViewModel deletedPreset)
    {
        bank.DeletePresetCommand.Execute(deletedPreset.Preset);
        _bankManagingService.UpdateBank(bank.Bank);
        ReloadPresetList(bank);
    }

    private void DeleteAllPresets()
    {
        var userResponse = _dialogService.ShowYesNoDialog("Delete all presets", 
            "Are you sure you want to delete all presets from this bank?");
        if (!userResponse) return;
        var bank = PresetListViewModel.DisplayedBank;
        if (bank is null) return;
        bank.Bank.Preset.Clear();
        _bankManagingService.UpdateBank(bank.Bank);
        bank.DeletePresetCommand.Execute(null);
        PresetsUpdated(bank);
    }
    private void DeleteSelectedPresets()
    {
        throw new NotImplementedException();
    }

    private void PresetsUpdated(BankViewModel bankViewModel)
    {
        _bankManagingService.UpdateBank(bankViewModel.Bank);
        bankViewModel.DeletePresetCommand.Execute(null);
        ReloadPresetList(bankViewModel);
    }
}