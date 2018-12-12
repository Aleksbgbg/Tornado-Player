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

        private readonly HashSet<Track> _modifiedTracks = new HashSet<Track>();

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

        public int ModifiedTrackCount => _modifiedTracks.Count;

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

            UpdateModifiedTracks(track.Target.Track.Track);
        }

        public void RemoveTrack(IEditTrackViewModel track)
        {
            PlaylistBucket.Remove(track);
            TrackBucket.Add(track);

            UpdateModifiedTracks(track.Target.Track.Track);
        }

        private void UpdateModifiedTracks(Track track)
        {
            if (_modifiedTracks.Contains(track))
            {
                _modifiedTracks.Remove(track);
            }
            else
            {
                _modifiedTracks.Add(track);
            }

            NotifyOfPropertyChange(() => ModifiedTrackCount);
        }
    }
}