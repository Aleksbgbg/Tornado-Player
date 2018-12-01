namespace Tornado.Player.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

    using Tornado.Player.Factories.Interfaces;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistViewModel : ViewModelBase, IPlaylistViewModel, IHandle<Shortcut>
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private readonly ICollectionView _tracksView;

        public PlaylistViewModel(ITrackFactory trackFactory, IEventAggregator eventAggregator, IMusicPlayerService musicPlayerService, Playlist playlist)
        {
            _musicPlayerService = musicPlayerService;

            DisplayName = playlist.Name;
            Playlist = playlist;
            Tracks = new BindableCollection<ITrackViewModel>(playlist.Tracks.Select(trackFactory.MakeTrackViewModel));

            _tracksView = CollectionViewSource.GetDefaultView(Tracks);
            _selectedTrack = Tracks[playlist.SelectedTrackIndex];

            _tracksView.SortDescriptions.Add(new SortDescription(string.Join(".", nameof(ITrackViewModel.Track), nameof(ITrackViewModel.Track.SortOrder)), ListSortDirection.Ascending));

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

        public IObservableCollection<ITrackViewModel> Tracks { get; }

        private ITrackViewModel _selectedTrack;
        public ITrackViewModel SelectedTrack
        {
            get => _selectedTrack;

            set
            {
                if (value == null || _selectedTrack == value) return;

                _selectedTrack = value;
                NotifyOfPropertyChange(() => SelectedTrack);

                _musicPlayerService.SelectTrack(_selectedTrack.Track);

                Playlist.SelectedTrackIndex = Tracks.IndexOf(_selectedTrack);
            }
        }

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

        public void PlayPrevious()
        {
            SelectTrack(Playlist.SelectedTrackIndex - 1);
        }

        public void PlayNext()
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

            _tracksView.Filter = item => ((ITrackViewModel)item).Track.MatchesSearch(searchText);
        }

        protected override void OnActivate()
        {
            _musicPlayerService.SelectTrack(SelectedTrack.Track);
        }

        private void SelectTrack(int index)
        {
            if (index < 0)
            {
                index = Tracks.Count - ((-index) % Tracks.Count);
            }
            else if (index >= Tracks.Count)
            {
                index = index % Tracks.Count;
            }

            SelectedTrack = Tracks[index];
        }
    }
}