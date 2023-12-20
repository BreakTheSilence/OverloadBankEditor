using System.IO;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Controls;
using Logic.ViewModels.Models;
using Models;

namespace Logic.ViewModels.Pages;

public class EditExistingBankViewModel : ContentPageViewModelAbstract
{
    private readonly Func<string> _pickBankFileFunction;
    private readonly IBankManagingService _bankLoaderService;
    private BankListViewModel _bankListViewModel;
    private PresetListViewModel _presetListViewModel;
    public override string PageTitle { get; }
    public RelayCommand AddBankFromFileCommand { get; }

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

    public EditExistingBankViewModel(Func<string> pickBankFileFunction, IBankManagingService bankLoaderService)
    {
        _pickBankFileFunction = pickBankFileFunction;
        _bankLoaderService = bankLoaderService;
        PageTitle = "Edit Existing Bank";
        AddBankFromFileCommand = new RelayCommand(AddBankFromFile);
        ReloadBankList();
    }
    
    private void AddBankFromFile()
    {
        var filePath = _pickBankFileFunction();
        if (string.IsNullOrWhiteSpace(filePath)) return;
        if (!File.Exists(filePath)) return;
        _bankLoaderService.AddBankFromPath(filePath);
        ReloadBankList();
    }

    private void ReloadBankList()
    {
        BankListViewModel = new BankListViewModel(_bankLoaderService, BankSelected, BankDeleted);
    }

    private void ReloadPresetList(BankViewModel bankViewModel)
    {
        PresetListViewModel = new PresetListViewModel(bankViewModel);
    }

    private void BankSelected(BankViewModel bankViewModel)
    {
        ReloadPresetList(bankViewModel);
    }

    private void BankDeleted(BankViewModel bankViewModel)
    {
        if (bankViewModel.Equals(PresetListViewModel.DisplayedBank))
        {
            PresetListViewModel = null!;
        }
    }
}