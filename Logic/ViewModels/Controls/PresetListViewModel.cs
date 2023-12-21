using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Logic.Interfaces.Services;
using Logic.ViewModels.Models;
using Models;

namespace Logic.ViewModels.Controls;

public class PresetListViewModel : ObservableObject
{
    private readonly Action<BankViewModel, PresetViewModel> _presetDeletedFromBank;
    private PresetViewModel _selectedPreset;
    public ObservableCollection<PresetViewModel> PresetViewModels { get; } = [];
    public BankViewModel DisplayedBank { get; }

    public PresetViewModel SelectedPreset
    {
        get => _selectedPreset;
        set => SetProperty(ref _selectedPreset, value);
    }

    public PresetListViewModel(BankViewModel bank, IDialogService dialogService,
        Action<BankViewModel, PresetViewModel> presetDeletedFromBank)
    {
        _presetDeletedFromBank = presetDeletedFromBank;
        DisplayedBank = bank;
        foreach (var preset in bank.Bank.Preset)
        {
            var presetViewModel = new PresetViewModel(preset, dialogService, DeletePresetAction);
            PresetViewModels.Add(presetViewModel);
        }
    }

    private void DeletePresetAction(PresetViewModel preset)
    {
        _presetDeletedFromBank(DisplayedBank, preset);
    }
}