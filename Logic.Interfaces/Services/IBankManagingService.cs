using Models;

namespace Logic.Interfaces.Services;

public interface IBankManagingService
{
    List<Bank> GetAllBanks();
    void AddBankFromPath(string bankFilePath);
    void UpdateBank(Bank bank);
    Bank CreateNewBank(string bankName);
}