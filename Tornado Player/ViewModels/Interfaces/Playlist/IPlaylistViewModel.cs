namespace Tornado.Player.ViewModels.Interfaces.Playlist
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Tornado.Player.Models.Player;

    internal interface IPlaylistViewModel : IViewModelBase, IConductor
    {
        Playlist Playlist { get; }

        IEnumerable<ITrackViewModel> Tracks { get; }

        void Add(IEnumerable<Track> tracks);

        void Remove(IEnumerable<Track> tracks);

        void Play();

        void SelectPrevious();

        void SelectNext();
    }
}