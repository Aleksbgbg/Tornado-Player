namespace Tornado.Player.ViewModels.Interfaces
{
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Tornado.Player.Models;

    internal interface IPlaylistViewModel : IViewModelBase, IConductor
    {
        Playlist Playlist { get; }

        IEnumerable<ITrackViewModel> Tracks { get; }

        void SelectPrevious();

        void SelectNext();
    }
}