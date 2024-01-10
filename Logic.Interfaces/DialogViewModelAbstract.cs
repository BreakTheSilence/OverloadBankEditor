using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Logic.Interfaces;

public abstract class DialogViewModelAbstract : ObservableObject
{
    public string WindowTitle { get; set; }
    public string NotificationTitle { get; set; }
    public string NotificationContent { get; set; }
    public abstract string OkAnswer { get; }
    public abstract string CancelAnswer { get; }

    public RelayCommand OkCommand { get; set; }
    public RelayCommand CancelCommand { get; set; }
    public abstract void SendCloseAction(Action closeAction);
}