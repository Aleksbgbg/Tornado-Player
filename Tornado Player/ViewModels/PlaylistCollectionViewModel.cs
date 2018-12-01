namespace Tornado.Player.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistCollectionViewModel : Conductor<IPlaylistViewModel>.Collection.OneActive, IPlaylistCollectionViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public PlaylistCollectionViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IDataService dataService)
        {
            _eventAggregator = eventAggregator;

            Track[] tracks = dataService.Load("Tracks", () => new Track[0]);
            Dictionary<ulong, Track> trackRepository = tracks.ToDictionary(track => track.Id, track => track);

            Playlist[] playlists = dataService.Load("Playlists", () => new Playlist[0]);

            foreach (Playlist playlist in playlists)
            {
                playlist.Load(trackRepository);
                Items.Add(viewModelFactory.MakeViewModel<IPlaylistViewModel>(playlist));
            }

            ScreenExtensions.TryActivate(this);
        }

        protected override void OnActivationProcessed(IPlaylistViewModel item, bool success)
        {
            if (success)
            {
                _eventAggregator.BeginPublishOnUIThread(item);
            }
        }

        protected override void OnViewAttached(object view, object context)
        {
            ActivateItem(Items[0]);
        }
    }
}