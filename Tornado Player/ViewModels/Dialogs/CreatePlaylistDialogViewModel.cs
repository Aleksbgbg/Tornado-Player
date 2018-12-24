namespace Tornado.Player.ViewModels.Dialogs
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Dialogs;
    using Tornado.Player.ViewModels.Interfaces.Dialogs;

    internal sealed class CreatePlaylistDialogViewModel : DialogViewModel<PlaylistCreation>, ICreatePlaylistDialogViewModel
    {
        public CreatePlaylistDialogViewModel(PlaylistCreation data) : base(data)
        {
            DisplayName = "New Playlist";
        }

        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;

            set
            {
                if (_playlistName == value) return;

                _playlistName = value;
                NotifyOfPropertyChange(() => PlaylistName);
                NotifyOfPropertyChange(() => CanOk);
            }
        }

        public bool CanOk => !string.IsNullOrWhiteSpace(PlaylistName);

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
            Data.Name = PlaylistName?.Trim();

            TryClose();
        }
    }
}