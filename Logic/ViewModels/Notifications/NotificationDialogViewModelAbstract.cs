using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels.Notifications;

public class NotificationDialogViewModelAbstract : DialogViewModelAbstract
{
    public NotificationDialogViewModelAbstract(string notificationTitle, string notificationContent)
    {
        NotificationTitle = notificationTitle;
        NotificationContent = notificationContent;
        OkCommand = new RelayCommand(Ok);
        CancelCommand = new RelayCommand(Cancel);
    }
    
    private void Ok()
    {
        
    }

    private void Cancel()
    {
        
    }

    public override void SendCloseAction(Action closeAction)
    {
        throw new NotImplementedException();
    }
}