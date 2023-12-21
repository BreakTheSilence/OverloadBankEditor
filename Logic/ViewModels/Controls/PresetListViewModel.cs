using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Logic.Interfaces.Services;
using Logic.ViewModels.Models;
using Models;

namespace Logic.ViewModels.Controls;

public class PresetListViewModel : ObservableObject
{
    private PresetViewModel _selectedPreset;
    public ObservableCollection<PresetViewModel> PresetViewModels { get; } = [];
    public BankViewModel DisplayedBank { get; }

    public PresetViewModel SelectedPreset
    {
        get => _selectedPreset;
        set => SetProperty(ref _selectedPreset, value);
    }

    public PresetListViewModel(BankViewModel bank, IDialogService dialogService)
    {
        DisplayedBank = bank;
        foreach (var preset in bank.Presets)
        {
            var presetViewModel = new PresetViewModel(preset, dialogService);
            PresetViewModels.Add(presetViewModel);
        }
    }
}