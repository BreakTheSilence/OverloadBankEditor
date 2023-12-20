using System.IO;
using System.Xml.Serialization;
using Logic.Interfaces.Services;
using Models;

namespace Logic.Services;

public class BankLoaderService : IBankLoaderService
{
    public Bank LoadBankFromFile(string filePath)
    {
        var serializer = new XmlSerializer(typeof(Bank));
        try
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            return (Bank)serializer.Deserialize(fileStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public void SaveBankToFile(Bank bank, string filePath)
    {
        throw new NotImplementedException();
    }
}