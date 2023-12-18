using System.Xml.Serialization;

namespace Models;

[XmlRoot(ElementName="preset")]
public class Preset
{
    [XmlAttribute(AttributeName="manager_format_version")]
    public string Manager_format_version { get; set; }
    [XmlAttribute(AttributeName="name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName="flag")]
    public string Flag { get; set; }
    [XmlAttribute(AttributeName="product_name")]
    public string Product_name { get; set; }
    [XmlAttribute(AttributeName="content")]
    public string Content { get; set; }
    [XmlAttribute(AttributeName="index")]
    public string Index { get; set; }
}