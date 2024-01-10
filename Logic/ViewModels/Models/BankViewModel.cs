using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

namespace Logic.ViewModels.Models;

public class BankViewModel : ObservableObject
{
    private readonly Action<BankViewModel> _bankDeleteRequested;

    public RelayCommand DeleteBankCommand { get; }
    public RelayCommand<Preset> DeletePresetCommand { get; }
    public bool IsDeleteEnabled { get; }

    public BankViewModel(Bank bank, Action<BankViewModel> bankDeleteRequested, bool isDeleteEnabled)
    {
        Bank = bank;
        _bankDeleteRequested = bankDeleteRequested;
        IsDeleteEnabled = isDeleteEnabled;
        DeleteBankCommand = new RelayCommand(DeleteBank);
        DeletePresetCommand = new RelayCommand<Preset>(DeletePreset);
    }

    public string Name
    {
        get => Bank.Name;
        set => SetProperty(Bank.Name, value, Bank, (u, n) => u.Name = n);
    }

    public string BankFilePath => Bank.FilePath;
    public int PresetsCount => Bank.Preset.Count;
    public Bank Bank { get; }

    private void DeleteBank()
    {
        _bankDeleteRequested(this);
    }

    private void DeletePreset(Preset? preset)
    {
        if (preset is not null)
        {
            Bank.Preset.Remove(preset);    
        }
        OnPropertyChanged(nameof(PresetsCount));
    }
}