namespace Tornado.Player.ViewModels.Editing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Editing;

    internal class TrackSinkViewModel : ViewModelBase, ITrackSinkViewModel
    {
        private readonly ICollectionView _trackSinkView;

        private readonly HashSet<Track> _addedTracks = new HashSet<Track>();

        private readonly HashSet<Track> _releasedTracks = new HashSet<Track>();

        public TrackSinkViewModel(IEnumerable<ITrackViewModel> tracks, string releaseImage)
        {
            TrackSink.AddRange(tracks);
            ReleaseImage = releaseImage;

            SelectedTracks.CollectionChanged += (sender, e) => NotifyOfPropertyChange(nameof(CanReleaseSelectedTracks));

            _trackSinkView = CollectionViewSource.GetDefaultView(TrackSink);

            _trackSinkView.SortDescriptions.Add(new SortDescription(string.Join(".", nameof(ITrackViewModel.PlaylistTrack), nameof(ITrackViewModel.PlaylistTrack.Track)), ListSortDirection.Ascending));
        }

        public event EventHandler<TracksReleasedEventArgs> TracksReleased;

        public IObservableCollection<ITrackViewModel> TrackSink { get; } = new BindableCollection<ITrackViewModel>();

        public IObservableCollection<ITrackViewModel> SelectedTracks { get; } = new BindableCollection<ITrackViewModel>();

        public string ReleaseImage { get; }

        public IEnumerable<Track> ReleasedTracks => _releasedTracks;

        public int ReleasedTracksCount => _releasedTracks.Count;

        public bool CanReleaseSelectedTracks => SelectedTracks.Count > 0;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;

            set
            {
                if (_searchText == value) return;

                _searchText = value;
                NotifyOfPropertyChange(nameof(SearchText));

                if (string.IsNullOrWhiteSpace(_searchText))
                {
                    _trackSinkView.Filter = null;
                }
                else
                {
                    _trackSinkView.Filter = item => ((ITrackViewModel)item).PlaylistTrack.Track.MatchesSearch(_searchText);
                }
            }
        }

        public void AddTracks(IEnumerable<ITrackViewModel> tracks)
        {
            foreach (ITrackViewModel trackViewModel in tracks)
            {
                TrackSink.Add(trackViewModel);

                Track track = trackViewModel.PlaylistTrack.Track;

                if (!_releasedTracks.Contains(track))
                {
                    _addedTracks.Add(track);
                }

                _releasedTracks.Remove(track);
            }
        }

        public void Reset()
        {
            _releasedTracks.Clear();
        }

        public void ReleaseTrack(ITrackViewModel trackViewModel)
        {
            ReleaseTrackNoNotification(trackViewModel);
            OnTracksReleased(new[] { trackViewModel });
        }

        public void ReleaseSelectedTracks()
        {
            ITrackViewModel[] releasedTracks = SelectedTracks.ToArray();

            foreach (ITrackViewModel trackViewModel in releasedTracks)
            {
                ReleaseTrackNoNotification(trackViewModel);
            }

            OnTracksReleased(releasedTracks);
        }

        private void ReleaseTrackNoNotification(ITrackViewModel trackViewModel)
        {
            TrackSink.Remove(trackViewModel);

            Track track = trackViewModel.PlaylistTrack.Track;

            if (!_addedTracks.Contains(track))
            {
                _releasedTracks.Add(track);
            }

            _addedTracks.Remove(track);
        }

        private void OnTracksReleased(IEnumerable<ITrackViewModel> tracks)
        {
            TracksReleased?.Invoke(this, new TracksReleasedEventArgs(tracks));
        }
    }
}