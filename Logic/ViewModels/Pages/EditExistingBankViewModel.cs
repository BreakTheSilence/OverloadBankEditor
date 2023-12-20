using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Controls;

namespace Logic.ViewModels.Pages;

public class EditExistingBankViewModel : ContentPageViewModelAbstract
{
    private readonly Func<string> _pickBankFileFunction;
    private readonly IBankLoaderService _bankLoaderService;
    private BankListViewModel _bankListViewModel = new();
    public override string PageTitle { get; }
    public RelayCommand AddBankFromFileCommand { get; }

    public BankListViewModel BankListViewModel
    {
        get => _bankListViewModel;
        set => SetProperty(ref _bankListViewModel, value);
    }

    public EditExistingBankViewModel(Func<string> pickBankFileFunction, IBankLoaderService bankLoaderService)
    {
        _pickBankFileFunction = pickBankFileFunction;
        _bankLoaderService = bankLoaderService;
        PageTitle = "Edit Existing Bank";
        AddBankFromFileCommand = new RelayCommand(AddBankFromFile);
        LoadBanks();
    }

    private void LoadBanks()
    {
        BankListViewModel = new BankListViewModel();
    }

    private void AddBankFromFile()
    {
        var filePath = _pickBankFileFunction();
        var bank = _bankLoaderService.LoadBankFromFile(filePath);
    }
}