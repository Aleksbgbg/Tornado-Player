namespace Tornado.Player.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistViewModel : Conductor<ITrackViewModel>.Collection.OneActive, IPlaylistViewModel, IHandle<Shortcut>
    {
        private readonly IViewModelFactory _viewModelFactory;

        private readonly IContentManagerService _contentManagerService;

        private readonly IMusicPlayerService _musicPlayerService;

        private readonly ICollectionView _tracksView;

        public PlaylistViewModel(IViewModelFactory viewModelFactory, IEventAggregator eventAggregator, IContentManagerService contentManagerService, IMusicPlayerService musicPlayerService, Playlist playlist)
        {
            _viewModelFactory = viewModelFactory;
            _contentManagerService = contentManagerService;
            _musicPlayerService = musicPlayerService;
            _tracksView = CollectionViewSource.GetDefaultView(Items);

            DisplayName = playlist.Name;
            Playlist = playlist;
            Items.AddRange(playlist.Tracks.Select(track => viewModelFactory.MakeViewModel<ITrackViewModel>(track)));

            _tracksView.SortDescriptions.Add(new SortDescription(nameof(ITrackViewModel.PlaylistTrack), ListSortDirection.Ascending));

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
                Items.Remove(Items.Single(item => item.PlaylistTrack.Track == oldTrack.Track));
            }
        }

        public void Play()
        {
            if (Items.Count == 0 || Playlist.SelectedTrackIndex < 0)
            {
                return;
            }

            ActivateItem(Items[Playlist.SelectedTrackIndex]);
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

            _tracksView.Filter = item => ((ITrackViewModel)item).PlaylistTrack.Track.MatchesSearch(searchText);
        }

        protected override void OnActivationProcessed(ITrackViewModel item, bool success)
        {
            if (success && item != null)
            {
                SelectTrack(Items.IndexOf(item));
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

            Items[index].Play();

            if (Playlist.SelectedTrackIndex == index) // Assume this condition means that the playlist has been resumed
            {
                _musicPlayerService.Progress = Playlist.TrackProgress;
            }

            Playlist.SelectedTrackIndex = index;
        }
    }
}