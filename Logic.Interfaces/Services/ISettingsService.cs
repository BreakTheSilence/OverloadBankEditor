using Models;

namespace Logic.Interfaces.Services;

public interface ISettingsService
{
    void SaveSettings(Settings settings);
    Settings LoadSettings();
}