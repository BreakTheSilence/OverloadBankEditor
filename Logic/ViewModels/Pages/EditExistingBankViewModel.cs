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
    private readonly IBankManagingService _bankLoaderService;
    private BankListViewModel _bankListViewModel;
    public override string PageTitle { get; }
    public RelayCommand AddBankFromFileCommand { get; }

    public BankListViewModel BankListViewModel
    {
        get => _bankListViewModel;
        set => SetProperty(ref _bankListViewModel, value);
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
        BankListViewModel = new BankListViewModel(_bankLoaderService, BankSelected);
    }

    private void BankSelected(BankViewModel bankViewModel)
    {
        
    }
}