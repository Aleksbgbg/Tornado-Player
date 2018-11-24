namespace Tornado.Player.ViewModels
{
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

    using Tornado.Player.Factories.Interfaces;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistViewModel : ViewModelBase, IPlaylistViewModel, IHandle<Shortcut>
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private readonly CollectionView _tracksView;

        public PlaylistViewModel(ITrackFactory trackFactory, IEventAggregator eventAggregator, IMusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;
            Tracks = new BindableCollection<ITrackViewModel>(_musicPlayerService.Tracks.Select(trackFactory.MakeTrackViewModel));
            _tracksView = (CollectionView)CollectionViewSource.GetDefaultView(Tracks);

            eventAggregator.Subscribe(this);

            _musicPlayerService.PlaylistLoaded += (sender, e) =>
            {
                Tracks.Clear();
                Tracks.AddRange(e.Tracks.Select(trackFactory.MakeTrackViewModel));
            };
            _musicPlayerService.TrackChanged += (sender, e) => NotifyOfPropertyChange(() => SelectedTrackIndex);
        }

        public IObservableCollection<ITrackViewModel> Tracks { get; }

        public int SelectedTrackIndex
        {
            get => _musicPlayerService.TrackIndex;

            set
            {
                if (value == -1) return;

                _musicPlayerService.SelectTrack(value);
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
    }
}