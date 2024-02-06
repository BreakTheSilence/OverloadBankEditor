using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Controls;
using Logic.ViewModels.Models;

namespace Logic.ViewModels.Pages;

public class ExportBankViewModel : ContentPageViewModelAbstract
{
    private readonly IBankManagingService _bankManagingService;
    private readonly IDialogService _dialogService;
    private BankListViewModel _bankListViewModel;
    private BankViewModel _selectedBank;
    public override string PageTitle => "Export Bank";

    public RelayCommand ExportBankCommand { get; }
    
    public BankListViewModel BankListViewModel
    {
        get => _bankListViewModel;
        private set => SetProperty(ref _bankListViewModel, value);
    }

    public BankViewModel SelectedBank
    {
        get => _selectedBank;
        set => SetProperty(ref _selectedBank, value);
    }

    public ExportBankViewModel(IBankManagingService bankManagingService, IDialogService dialogService)
    {
        _bankManagingService = bankManagingService;
        _dialogService = dialogService;

        ReloadBankList();
        ExportBankCommand = new RelayCommand(ExportBank);
    }
    
    private void ReloadBankList()
    {
        BankListViewModel = new BankListViewModel(_bankManagingService, BankSelected, BankDeleted);
    }
    
    private void BankSelected(BankViewModel bankViewModel)
    {
        SelectedBank = bankViewModel;
    }

    private void BankDeleted(BankViewModel bankViewModel)
    {
    }

    private void ExportBank()
    {
        if (SelectedBank is null) return;
        var folder = _dialogService.ShowFolderSelectDialog();
        if (string.IsNullOrWhiteSpace(folder)) return;
        var bank = SelectedBank.Bank;
        for (var i = 0; i < bank.Preset.Count; i++)
        {
            bank.Preset[i].Index = i.ToString();
        }
        _bankManagingService.ExportBank(SelectedBank.Bank, folder, SelectedBank.Name);
        _dialogService.ShowOkDialog("Save completed", "Bank was saved successfully");
    }
}