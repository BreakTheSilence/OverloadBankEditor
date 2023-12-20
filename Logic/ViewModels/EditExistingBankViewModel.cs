using Logic.Interfaces;

namespace Logic.ViewModels;

public class EditExistingBankViewModel : ContentPageViewModelAbstract
{
    public EditExistingBankViewModel()
    {
        PageTitle = "Edit Existing Bank";
    }
    public override string PageTitle { get; }
}