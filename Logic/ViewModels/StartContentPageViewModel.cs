using Logic.Interfaces;

namespace Logic.ViewModels;

public class StartContentPageViewModel : ContentPageViewModelAbstract
{
    public StartContentPageViewModel()
    {
        PageTitle = "Start Page";
    }

    public override string PageTitle { get; }
}