namespace Tornado.Player.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackSinkViewModel : ViewModelBase, ITrackSinkViewModel
    {
        private readonly HashSet<Track> _addedTracks = new HashSet<Track>();

        private readonly HashSet<Track> _releasedTracks = new HashSet<Track>();

        public TrackSinkViewModel(IEnumerable<ITrackViewModel> tracks)
        {
            TrackSink.AddRange(tracks);
        }

        public event EventHandler<TracksReleasedEventArgs> TracksReleased;

        public IObservableCollection<ITrackViewModel> TrackSink { get; } = new BindableCollection<ITrackViewModel>();

        public IObservableCollection<ITrackViewModel> SelectedTracks { get; } = new BindableCollection<ITrackViewModel>();

        public IEnumerable<Track> ReleasedTracks => _releasedTracks;

        public int ReleasedTracksCount => _releasedTracks.Count;

        public void AddTracks(IEnumerable<ITrackViewModel> tracks)
        {
            foreach (ITrackViewModel trackViewModel in tracks)
            {
                TrackSink.Add(trackViewModel);

                Track track = trackViewModel.Track.Track;

                _addedTracks.Add(track);
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
            if (SelectedTracks.Count == 0)
            {
                return;
            }

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

            Track track = trackViewModel.Track.Track;

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