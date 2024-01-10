using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces.Services;
using Models;

namespace Logic.ViewModels.Models;

public class PresetViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;

    public string Name
    {
        get => Preset.Name;
        set => SetProperty(Preset.Name, value, Preset, (u, n) => u.Name = n);
    }

    public string ShortenContent
    {
        get
        {
            var content = Preset.Content;
            if (string.IsNullOrEmpty(content) || content.Length < 2)
            {
                return Preset.Content; // Or return an empty string or null, based on your requirements
            }

            var firstTwoChars = content[..2];
            var lastTwoChars = content.Substring(content.Length - 2, 2);
            return $"\"{firstTwoChars}...{lastTwoChars}\"";
        }
    }

    public Preset Preset { get; }
    public bool IsPresetDeleteEnabled { get; }
    public RelayCommand DeletePresetCommand { get; }
    public RelayCommand OpenTextInputDialogCommand { get; }
    
    public PresetViewModel(Preset preset, IDialogService dialogService, Action<PresetViewModel> deletePresetAction,
        bool isDeleteEnabled)
    {
        Preset = preset;
        IsPresetDeleteEnabled = isDeleteEnabled;
        _dialogService = dialogService;
        DeletePresetCommand = new RelayCommand(() => deletePresetAction(this));
        OpenTextInputDialogCommand = new RelayCommand(OpenTextInputDialog);
    }

    private void OpenTextInputDialog()
    {
        var newName = _dialogService.ShowTextInputDialog("Enter new preset name");
        if (string.IsNullOrWhiteSpace(newName)) return;
        Name = newName;
    }
}