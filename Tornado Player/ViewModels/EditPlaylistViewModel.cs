namespace Tornado.Player.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class EditPlaylistViewModel : ViewModelBase, IEditPlaylistViewModel
    {
        private readonly IPlaylistViewModel _playlistViewModel;

        private readonly HashSet<Track> _addedTracks = new HashSet<Track>();

        private readonly HashSet<Track> _removedTracks = new HashSet<Track>();

        public EditPlaylistViewModel(IViewModelFactory viewModelFactory, IContentManagerService contentManagerService, IPlaylistViewModel playlistViewModel)
        {
            _playlistViewModel = playlistViewModel;
            DisplayName = playlistViewModel.Playlist.Name;

            PlaylistBucket.AddRange(playlistViewModel.Tracks.Select(track => viewModelFactory.MakeViewModel<IEditTrackViewModel>(track)));

            Track[] playlistTracks = playlistViewModel.Playlist.Tracks.Select(playlistTrack => playlistTrack.Track).ToArray();

            TrackBucket.AddRange
            (
                 contentManagerService.RetrieveTracks()
                                      .Where(track => !playlistTracks.Contains(track))
                                      .Select(track => viewModelFactory.MakeViewModel<IEditTrackViewModel>
                                                       (
                                                            viewModelFactory.MakeViewModel<ITrackViewModel>(new PlaylistTrack(0, track))
                                                       )
                                              )
            );
        }

        public IObservableCollection<IEditTrackViewModel> TrackBucket { get; } = new BindableCollection<IEditTrackViewModel>();

        public IObservableCollection<IEditTrackViewModel> PlaylistBucket { get; } = new BindableCollection<IEditTrackViewModel>();

        public int ModifiedTrackCount => _addedTracks.Count + _removedTracks.Count;

        public bool CanApply => ModifiedTrackCount > 0;

        public void AddSelectedTracks()
        {
            foreach (IEditTrackViewModel editTrackViewModel in TrackBucket.Where(editTrackViewModel => editTrackViewModel.IsSelected).ToArray())
            {
                AddTrack(editTrackViewModel);
            }
        }

        public void RemoveSelectedTracks()
        {
            foreach (IEditTrackViewModel editTrackViewModel in PlaylistBucket.Where(editTrackViewModel => editTrackViewModel.IsSelected).ToArray())
            {
                RemoveTrack(editTrackViewModel);
            }
        }

        public void AddTrack(IEditTrackViewModel track)
        {
            TrackBucket.Remove(track);
            PlaylistBucket.Add(track);

            Track addedTrack = track.Target.Track.Track;

            if (_removedTracks.Contains(addedTrack))
            {
                _removedTracks.Remove(addedTrack);
            }
            else
            {
                _addedTracks.Add(addedTrack);
            }

            NotifyOfPropertyChange(() => ModifiedTrackCount);
            NotifyOfPropertyChange(() => CanApply);
        }

        public void RemoveTrack(IEditTrackViewModel track)
        {
            PlaylistBucket.Remove(track);
            TrackBucket.Add(track);

            Track removedTrack = track.Target.Track.Track;

            if (_addedTracks.Contains(removedTrack))
            {
                _addedTracks.Remove(removedTrack);
            }
            else
            {
                _removedTracks.Add(removedTrack);
            }

            NotifyOfPropertyChange(() => ModifiedTrackCount);
            NotifyOfPropertyChange(() => CanApply);
        }

        public void Apply()
        {
            _playlistViewModel.Add(_addedTracks);
            _playlistViewModel.Remove(_removedTracks);

            _addedTracks.Clear();
            _removedTracks.Clear();

            NotifyOfPropertyChange(() => ModifiedTrackCount);
            NotifyOfPropertyChange(() => CanApply);
        }
    }
}