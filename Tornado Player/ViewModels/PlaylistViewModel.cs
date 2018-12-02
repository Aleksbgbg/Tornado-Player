namespace Tornado.Player.ViewModels
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

    using Tornado.Player.Factories.Interfaces;
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class PlaylistViewModel : Conductor<ITrackViewModel>.Collection.OneActive, IPlaylistViewModel, IHandle<Shortcut>
    {
        private readonly ICollectionView _tracksView;

        public PlaylistViewModel(ITrackFactory trackFactory, IEventAggregator eventAggregator, Playlist playlist)
        {
            _tracksView = CollectionViewSource.GetDefaultView(Items);

            DisplayName = playlist.Name;
            Playlist = playlist;
            Items.AddRange(playlist.Tracks.Select(trackFactory.MakeTrackViewModel));
            ActivateItem(Items[playlist.SelectedTrackIndex]);

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

            _tracksView.Filter = item => ((ITrackViewModel)item).Track.MatchesSearch(searchText);
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