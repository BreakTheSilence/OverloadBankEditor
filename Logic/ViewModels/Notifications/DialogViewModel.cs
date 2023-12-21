using CommunityToolkit.Mvvm.ComponentModel;
using Logic.Interfaces;

namespace Logic.ViewModels.Notifications;

public class DialogViewModel : ObservableObject
{
    private DialogViewModelAbstract _dialogViewModelAbstract;

    public DialogViewModelAbstract DialogViewModelAbstract
    {
        get => _dialogViewModelAbstract;
        set => SetProperty(ref _dialogViewModelAbstract, value);
    }

    public DialogViewModel(DialogViewModelAbstract dialogViewModel)
    {
        DialogViewModelAbstract = dialogViewModel;
    }
}