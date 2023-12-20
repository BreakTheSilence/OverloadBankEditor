using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

namespace Logic.ViewModels.Models;

public class BankViewModel : ObservableObject
{
    private readonly Bank _bank;
    private readonly Action<BankViewModel> _bankDeleteRequested;

    public RelayCommand DeleteBankCommand { get; set; }

    public BankViewModel(Bank bank, Action<BankViewModel> bankDeleteRequested)
    {
        _bank = bank;
        _bankDeleteRequested = bankDeleteRequested;
        DeleteBankCommand = new RelayCommand(DeleteBank);
    }

    public string Name
    {
        get => _bank.Name;
        set => SetProperty(_bank.Name, value, _bank, (u, n) => u.Name = n);
    }

    public string BankFilePath => _bank.FilePath;

    public int PresetsCount => _bank.Preset.Count;

    private void DeleteBank()
    {
        _bankDeleteRequested(this);
    }
}