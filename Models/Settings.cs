using System.Xml.Serialization;

namespace Models;

[XmlRoot(ElementName="settings")]
public class Settings
{
    [XmlAttribute(AttributeName="workingDirectoryPath")]
    public string WorkingDirectoryPath { get; set; }
}