namespace Tornado.Player.Utilities.EventAggregator
{
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistCreationMessage
    {
        public PlaylistCreationMessage(IPlaylistViewModel playlistViewModel)
        {
            PlaylistViewModel = playlistViewModel;
        }

        public IPlaylistViewModel PlaylistViewModel { get; }
    }
}