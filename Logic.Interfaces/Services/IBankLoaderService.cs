using Models;

namespace Logic.Interfaces.Services;

public interface IBankLoaderService
{
    Bank LoadBankFromFile(string filePath);
    void SaveBankToFile(Bank bank, string filePath);
}