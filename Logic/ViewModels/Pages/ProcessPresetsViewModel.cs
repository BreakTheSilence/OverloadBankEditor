using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.ViewModels.Controls;

namespace Logic.ViewModels.Pages;

public class ProcessPresetsViewModel : ContentPageViewModelAbstract
{
    private readonly Func<string> _pickBankFileFunction;
    private readonly IBankManagingService _bankManagingService;
    private readonly IDialogService _dialogService;
    
    private BankListViewModel _readonlybankListViewModel;
    private BankListViewModel _editablebankListViewModel;
    
    private PresetListViewModel _readonlypresetListViewModel;
    private PresetListViewModel _editablepresetListViewModel;
    public override string PageTitle { get; }

    public ProcessPresetsViewModel(Func<string> pickBankFileFunction, IBankManagingService bankManagingService,
        IDialogService dialogService)
    {
        _pickBankFileFunction = pickBankFileFunction;
        _bankManagingService = bankManagingService;
        _dialogService = dialogService;
        PageTitle = "Process Presets";
    }
    
    
}