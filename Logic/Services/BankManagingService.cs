using System.IO;
using Logic.Interfaces.Services;
using Models;

namespace Logic.Services;

public class BankManagingService : IBankManagingService
{
    private readonly IBankLoaderService _bankLoaderService;
    private readonly ISettingsService _settingsService;

    public BankManagingService(IBankLoaderService bankLoaderService, ISettingsService settingsService)
    {
        _bankLoaderService = bankLoaderService;
        _settingsService = settingsService;
    }
    
    public List<Bank> GetAllBanks()
    {
        var settings = _settingsService.LoadSettings();
        if (string.IsNullOrWhiteSpace(settings.WorkingDirectoryPath)) 
            throw new Exception("Working directory is not specified");

        var bankFilePaths = GetBankFilePaths(settings.WorkingDirectoryPath);

        var banks = new List<Bank>();
        foreach (var bankFilePath in bankFilePaths)
        {
            var bank = _bankLoaderService.LoadBankFromFile(bankFilePath);
            bank.FilePath = bankFilePath;
            banks.Add(bank);
        }

        return banks;
    }

    public void AddBankFromPath(string bankFilePath)
    {
        var bank = _bankLoaderService.LoadBankFromFile(bankFilePath);
        var settings = _settingsService.LoadSettings();
        
        _bankLoaderService.SaveBankToFile(bank, Path.Combine(settings.WorkingDirectoryPath, $"{Guid.NewGuid()}.ovb"));
    }

    public void UpdateBank(Bank bank)
    {
        _bankLoaderService.SaveBankToFile(bank, bank.FilePath);
    }

    private List<string> GetBankFilePaths(string workingDirectoryPath)
    {
        var ovbFiles = Directory.GetFiles(workingDirectoryPath, "*.ovb").ToList();
        return ovbFiles;
    }
}