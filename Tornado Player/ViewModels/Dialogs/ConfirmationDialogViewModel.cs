namespace Tornado.Player.ViewModels.Dialogs
{
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal sealed class ConfirmationDialogViewModel : DialogViewModel<Confirmation>, IConfirmationDialogViewModel
    {
        public ConfirmationDialogViewModel(Confirmation data) : base(data)
        {
            DisplayName = "Are you sure?";
        }

        public void Confirm()
        {
            CloseWithConfirmation(true);
        }

        public void Cancel()
        {
            CloseWithConfirmation(false);
        }

        private void CloseWithConfirmation(bool confirmed)
        {
            Data.Confirmed = confirmed;

            TryClose();
        }
    }
}