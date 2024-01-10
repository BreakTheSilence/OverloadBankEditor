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
    private readonly Action<BankViewModel> _presetsCollectionUpdateAction;
    private readonly bool _isDeleteEnabled;
    private PresetViewModel _selectedPreset;
    public ObservableCollection<PresetViewModel> PresetViewModels { get; } = [];
    public BankViewModel? DisplayedBank { get; }

    public PresetViewModel SelectedPreset
    {
        get => _selectedPreset;
        set => SetProperty(ref _selectedPreset, value);
    }
    
    public RelayCommand SortByNameCommand { get; }
    public RelayCommand RemoveContentDuplicatesCommand { get; }
    public RelayCommand AddNumberingCommand { get; }
    public RelayCommand RemoveNumberingCommand { get; }

    public PresetListViewModel(BankViewModel bank, IDialogService dialogService,
        Action<BankViewModel, PresetViewModel> presetDeletedFromBank, Action<BankViewModel> presetsCollectionUpdateAction,
        bool isDeleteEnabled = true)
    {
        _dialogService = dialogService;
        _presetDeletedFromBank = presetDeletedFromBank;
        _presetsCollectionUpdateAction = presetsCollectionUpdateAction;
        _isDeleteEnabled = isDeleteEnabled;
        DisplayedBank = bank;
        SortByNameCommand = new RelayCommand(SortByName);
        RemoveContentDuplicatesCommand = new RelayCommand(RemoveContentDuplicates);
        AddNumberingCommand = new RelayCommand(AddNumbering);
        RemoveNumberingCommand = new RelayCommand(RemoveNumbering);
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
        _presetsCollectionUpdateAction.Invoke(DisplayedBank);
    }
    
    private void RemoveContentDuplicates()
    {
        var presets = DisplayedBank.Bank.Preset.ToList();
        var result = presets.GroupBy(p => p.Content)
            .Select(group => group.First())
            .ToList();
        DisplayedBank.Bank.Preset = result;
        _presetsCollectionUpdateAction.Invoke(DisplayedBank);
    }

    private void AddNumbering()
    {
        
    }
    private void RemoveNumbering()
    {
        
    }

    private void UpdateObservableCollection(IEnumerable<Preset> presets)
    {
        PresetViewModels.Clear();
        foreach (var preset in presets)
        {
            var presetViewModel = new PresetViewModel(preset, _dialogService, DeletePresetAction, _isDeleteEnabled);
            PresetViewModels.Add(presetViewModel);
        }
    }
}