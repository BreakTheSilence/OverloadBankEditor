using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

namespace Logic.ViewModels.Models;

public class PresetViewModel : ObservableObject
{
    private readonly Preset _preset;

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

    public RelayCommand DeletePresetCommand { get; }
    
    public PresetViewModel(Preset preset)
    {
        _preset = preset;
        DeletePresetCommand = new RelayCommand(DeletePreset);
    }

    private void DeletePreset()
    {
        
    }
}