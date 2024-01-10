using Logic.Interfaces;

namespace Logic.ViewModels.Notifications;

public class YesNoDialogViewModel : DialogViewModelAbstract
{
    public override string OkAnswer => "Yes";
    public override string CancelAnswer => "No";
    public override void SendCloseAction(Action closeAction)
    {
        throw new NotImplementedException();
    }
}