using CommunityToolkit.Mvvm.ComponentModel;

namespace Logic.Interfaces;

public abstract class ContentPageViewModelAbstract : ObservableObject
{
    public abstract string PageTitle { get; }
}