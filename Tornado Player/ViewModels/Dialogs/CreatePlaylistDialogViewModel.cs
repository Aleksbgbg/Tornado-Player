namespace Tornado.Player.ViewModels.Dialogs
{
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal class CreatePlaylistDialogViewModel : DialogViewModel<PlaylistCreation>, ICreatePlaylistDialogViewModel
    {
        public CreatePlaylistDialogViewModel(PlaylistCreation data) : base(data)
        {
        }

        public void Ok()
        {
            CloseWithResponse(true);
        }

        public void Cancel()
        {
            CloseWithResponse(false);
        }

        private void CloseWithResponse(bool create)
        {
            Data.Create = create;
            TryClose();
        }
    }
}