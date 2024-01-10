using CommunityToolkit.Mvvm.Input;
using Logic.Interfaces;

namespace Logic.ViewModels.Notifications;

public class TextInputDialogViewModel : DialogViewModelAbstract
{
    private string _userInput;
    private Action _closeAction;
    
    public TextInputDialogViewModel(string notificationTitle)
    {
        NotificationTitle = notificationTitle;
        OkCommand = new RelayCommand(Ok);
        CancelCommand = new RelayCommand(Cancel);
    }

    public string UserInput
    {
        get => _userInput;
        set => SetProperty(ref _userInput, value);
    }
    
    private void Ok()
    {
        _closeAction();
    }

    private void Cancel()
    {
        UserInput = string.Empty;
        _closeAction();
    }

    public override string OkAnswer => "Ok";
    public override string CancelAnswer => "Cancel";

    public override void SendCloseAction(Action closeAction)
    {
        _closeAction = closeAction;
    }
}