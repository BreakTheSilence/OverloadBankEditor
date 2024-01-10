using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using Logic.Interfaces.Services;
using Logic.ViewModels.Models;

namespace Logic.ViewModels.Controls;

public class BankListViewModel : ObservableObject
{
    private readonly IBankManagingService _bankManagingService;
    private readonly Action<BankViewModel> _bankSelectedAction;
    private readonly Action<BankViewModel>? _bankDeletedAction;
    private BankViewModel _selectedItem;

    public ObservableCollection<BankViewModel> BankViewModels { get; } = [];

    public BankViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value is null) return;
            _bankSelectedAction(value);
        }
    }

    public bool IsDeleteEnabled { get; }

    public BankListViewModel(IBankManagingService bankManagingService, Action<BankViewModel> bankSelectedAction,
        Action<BankViewModel> bankDeletedAction)
    {
        _bankManagingService = bankManagingService;
        _bankSelectedAction = bankSelectedAction;
        _bankDeletedAction = bankDeletedAction;

        IsDeleteEnabled = true;
        LoadBanks();
    }

    public BankListViewModel(IBankManagingService bankManagingService, Action<BankViewModel> bankSelectedAction)
    {
        _bankManagingService = bankManagingService;
        _bankSelectedAction = bankSelectedAction;

        IsDeleteEnabled = false;
        LoadBanks();
    }

    private void LoadBanks()
    {
        BankViewModels.Clear();
        var banks = _bankManagingService.GetAllBanks();
        foreach (var bank in banks)
        {
            var bankViewModel = new BankViewModel(bank, BankDeleteRequested, IsDeleteEnabled);
            BankViewModels.Add(bankViewModel);
        }
    }

    private void BankDeleteRequested(BankViewModel bankViewModel)
    {
        if (File.Exists(bankViewModel.BankFilePath))
        {
            File.Delete(bankViewModel.BankFilePath);
        }

        LoadBanks();
        _bankDeletedAction?.Invoke(bankViewModel);
    }
}