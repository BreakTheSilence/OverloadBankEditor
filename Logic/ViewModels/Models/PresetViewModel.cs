﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces.Services;
using Models;

namespace Logic.ViewModels.Models;

public class PresetViewModel : ObservableObject
{
    private readonly Preset _preset;
    private readonly IDialogService _dialogService;
    private readonly Action<PresetViewModel> _deletePresetAction;

    public string Name
    {
        get => _preset.Name;
        set => SetProperty(_preset.Name, value, _preset, (u, n) => u.Name = n);
    }

    public string ShortenContent
    {
        get
        {
            var content = _preset.Content;
            if (string.IsNullOrEmpty(content) || content.Length < 2)
            {
                return _preset.Content; // Or return an empty string or null, based on your requirements
            }

            var firstTwoChars = content[..2];
            var lastTwoChars = content.Substring(content.Length - 2, 2);
            return $"\"{firstTwoChars}...{lastTwoChars}\"";
        }
    }

    public Preset Preset => _preset;

    public RelayCommand DeletePresetCommand { get; }
    public RelayCommand OpenTextInputDialogCommand { get; }
    
    public PresetViewModel(Preset preset, IDialogService dialogService, Action<PresetViewModel> deletePresetAction)
    {
        _preset = preset;
        _dialogService = dialogService;
        _deletePresetAction = deletePresetAction;
        DeletePresetCommand = new RelayCommand(DeletePreset);
        OpenTextInputDialogCommand = new RelayCommand(OpenTextInputDialog);
    }

    private void DeletePreset()
    {
        _deletePresetAction(this);
    }

    private void OpenTextInputDialog()
    {
        var newName = _dialogService.ShowTextInputDialog("Enter new preset name");
        if (string.IsNullOrWhiteSpace(newName)) return;
        Name = newName;
    }
}