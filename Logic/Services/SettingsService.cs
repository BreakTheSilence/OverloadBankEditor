using System.IO;
using System.Xml.Serialization;
using Logic.Interfaces.Services;
using Models;

namespace Logic.Services;

public class SettingsService : ISettingsService
{
    private const string SettingsFilePath = "appSettings.xml";
    

    public void SaveSettings(Settings settings)
    {
        var serializer = new XmlSerializer(typeof(Settings));

        using TextWriter writer = new StreamWriter(SettingsFilePath);
        serializer.Serialize(writer, settings);
    }

    public Settings LoadSettings()
    {
        if (!File.Exists(SettingsFilePath)) return new Settings();
        var serializer = new XmlSerializer(typeof(Settings));

        using TextReader reader = new StreamReader(SettingsFilePath);
        return (Settings)serializer.Deserialize(reader)!;

    }
}