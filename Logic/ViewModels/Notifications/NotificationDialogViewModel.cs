using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels.Notifications;

public class NotificationDialogViewModel : DialogViewModelAbstract
{
    public NotificationDialogViewModel(string notificationTitle, string notificationContent)
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
    
    public override string OkAnswer => "Ok";
    public override string CancelAnswer => "Cancel";

    public override void SendCloseAction(Action closeAction)
    {
        throw new NotImplementedException();
    }
}