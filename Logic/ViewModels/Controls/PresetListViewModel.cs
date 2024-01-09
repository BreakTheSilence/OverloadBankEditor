using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces.Services;
using Logic.ViewModels.Models;
using Models;

namespace Logic.ViewModels.Controls;

public class PresetListViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly Action<BankViewModel, PresetViewModel> _presetDeletedFromBank;
    private readonly Action<BankViewModel> _presetsSortedAction;
    private PresetViewModel _selectedPreset;
    public ObservableCollection<PresetViewModel> PresetViewModels { get; } = [];
    public BankViewModel DisplayedBank { get; }

    public PresetViewModel SelectedPreset
    {
        get => _selectedPreset;
        set => SetProperty(ref _selectedPreset, value);
    }
    
    public RelayCommand SortByNameCommand { get; }

    public PresetListViewModel(BankViewModel bank, IDialogService dialogService,
        Action<BankViewModel, PresetViewModel> presetDeletedFromBank, Action<BankViewModel> presetsSortedAction)
    {
        _dialogService = dialogService;
        _presetDeletedFromBank = presetDeletedFromBank;
        _presetsSortedAction = presetsSortedAction;
        DisplayedBank = bank;
        SortByNameCommand = new RelayCommand(SortByName);
        var presets = DisplayedBank.Bank.Preset.ToList();
        UpdateObservableCollection(presets);
    }

    private void DeletePresetAction(PresetViewModel preset)
    {
        _presetDeletedFromBank(DisplayedBank, preset);
    }

    private void SortByName()
    {
        var presets = DisplayedBank.Bank.Preset.OrderBy(x => x.Name).ToList();
        UpdateObservableCollection(presets);
        DisplayedBank.Bank.Preset = presets;
        _presetsSortedAction.Invoke(DisplayedBank);
    }

    private void UpdateObservableCollection(IEnumerable<Preset> presets)
    {
        PresetViewModels.Clear();
        foreach (var preset in presets)
        {
            var presetViewModel = new PresetViewModel(preset, _dialogService, DeletePresetAction);
            PresetViewModels.Add(presetViewModel);
        }
    }
}