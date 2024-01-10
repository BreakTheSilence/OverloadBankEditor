using System.Xml.Serialization;

namespace Models;

[XmlRoot(ElementName="bank")]
public class Bank
{
    [XmlElement(ElementName="preset")]
    public List<Preset> Preset { get; set; }
    
    [XmlAttribute(AttributeName="manager_format_version")]
    public string Manager_format_version { get; set; }
    
    [XmlAttribute(AttributeName="name")]
    public string Name { get; set; }
    
    [XmlAttribute(AttributeName="product_name")]
    public string Product_name { get; set; }
    
    [XmlAttribute(AttributeName="created_with")]
    public string Created_with { get; set; }

    public string FilePath { get; set; }

    public static Bank GetEmptyBank()
    {
        return new Bank
        {
            Manager_format_version = "1",
            Product_name = "TH-U",
            Created_with = "65536",
        };
    }
}