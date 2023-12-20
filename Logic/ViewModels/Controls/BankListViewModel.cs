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
    private BankViewModel _selectedItem;

    public ObservableCollection<BankViewModel> BankViewModels { get; } = [];

    public BankViewModel SelectedItem
    {
        get => _selectedItem;
        set
        {
            // SetProperty(ref _selectedItem, value);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (value is null) return;
            _bankSelectedAction(value);
        }
    }

    public BankListViewModel(IBankManagingService bankManagingService, Action<BankViewModel> bankSelectedAction)
    {
        _bankManagingService = bankManagingService;
        _bankSelectedAction = bankSelectedAction;
        LoadBanks();
    }

    private void LoadBanks()
    {
        BankViewModels.Clear();
        var banks = _bankManagingService.GetAllBanks();
        foreach (var bank in banks)
        {
            var bankViewModel = new BankViewModel(bank, BankDeleteRequested);
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
    }
}