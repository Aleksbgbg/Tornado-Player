namespace Tornado.Player.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistViewModel : Conductor<ITrackViewModel>.Collection.OneActive, IPlaylistViewModel, IHandle<Shortcut>
    {
        private readonly IViewModelFactory _viewModelFactory;

        private readonly IContentManagerService _contentManagerService;

        private readonly ICollectionView _tracksView;

        public PlaylistViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IContentManagerService contentManagerService, Playlist playlist)
        {
            _viewModelFactory = viewModelFactory;
            _contentManagerService = contentManagerService;
            _tracksView = CollectionViewSource.GetDefaultView(Items);

            DisplayName = playlist.Name;
            Playlist = playlist;
            Items.AddRange(playlist.Tracks.Select(track => viewModelFactory.MakeViewModel<ITrackViewModel>(track)));
            ActivateItem(Items[playlist.SelectedTrackIndex]);

            _tracksView.SortDescriptions.Add(new SortDescription(nameof(ITrackViewModel.Track), ListSortDirection.Ascending));

            eventAggregator.Subscribe(this);

            Playlist.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Playlist.IsShuffled))
                {
                    _tracksView.Refresh();
                }
            };
        }

        public Playlist Playlist { get; }

        private bool _isSearching;
        public bool IsSearching
        {
            get => _isSearching;

            set
            {
                if (_isSearching == value) return;

                _isSearching = value;
                NotifyOfPropertyChange(() => IsSearching);

                if (!_isSearching)
                {
                    _tracksView.Filter = null;
                }
            }
        }

        public IEnumerable<ITrackViewModel> Tracks => Items;

        public void Add(IEnumerable<Track> tracks)
        {
            foreach (Track track in tracks)
            {
                PlaylistTrack newTrack = _contentManagerService.AddTrackToPlaylist(Playlist, track);
                Items.Add(_viewModelFactory.MakeViewModel<ITrackViewModel>(newTrack));
            }
        }

        public void Remove(IEnumerable<Track> tracks)
        {
            foreach (Track track in tracks)
            {
                PlaylistTrack oldTrack = _contentManagerService.RemoveTrackFromPlaylist(Playlist, track);
                Items.Remove(Items.Single(item => item.Track.Track.Equals(oldTrack.Track)));
            }
        }

        public void SelectPrevious()
        {
            SelectTrack(Playlist.SelectedTrackIndex - 1);
        }

        public void SelectNext()
        {
            SelectTrack(Playlist.SelectedTrackIndex + 1);
        }

        public void Handle(Shortcut message)
        {
            if (message == Shortcut.Search)
            {
                IsSearching = !IsSearching;
            }
        }

        public void Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                _tracksView.Filter = null;
                return;
            }

            _tracksView.Filter = item => ((ITrackViewModel)item).Track.Track.MatchesSearch(searchText);
        }

        protected override void OnActivationProcessed(ITrackViewModel item, bool success)
        {
            if (success && item != null)
            {
                Playlist.SelectedTrackIndex = Items.IndexOf(item);
            }
        }

        private void SelectTrack(int index)
        {
            if (index < 0)
            {
                index = Items.Count - ((-index) % Items.Count);
            }
            else if (index >= Items.Count)
            {
                index = index % Items.Count;
            }

            ActivateItem(Items[index]);
        }
    }
}