using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels.Notifications;

public class YesNoDialogViewModel : DialogViewModelAbstract
{
    private Action _closeAction;

    public override string OkAnswer => "Yes";
    public override string CancelAnswer => "No";
    public bool UserResponse { get; private set; }


    public YesNoDialogViewModel(string dialogQuestion, string dialogDescription)
    {
        NotificationTitle = dialogQuestion;
        NotificationContent = dialogDescription;
        OkCommand = new RelayCommand(Yes);
        CancelCommand = new RelayCommand(No);
    }
    
    private void Yes()
    {
        UserResponse = true;
        _closeAction();
    }

    private void No()
    {
        UserResponse = false;
        _closeAction();
    }
    public override void SendCloseAction(Action closeAction)
    {
        _closeAction = closeAction;
    }
}