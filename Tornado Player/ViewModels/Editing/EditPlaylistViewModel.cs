namespace Tornado.Player.ViewModels.Editing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro.Wrapper;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Editing;

    internal sealed class EditPlaylistViewModel : ViewModelBase, IEditPlaylistViewModel
    {
        private readonly IPlaylistViewModel _playlistViewModel;

        public EditPlaylistViewModel(IViewModelFactory viewModelFactory, IContentManagerService contentManagerService, IPlaylistViewModel playlistViewModel)
        {
            _playlistViewModel = playlistViewModel;
            DisplayName = playlistViewModel.Playlist.Name;

            HashSet<Track> playlistTracks = new HashSet<Track>(playlistViewModel.Playlist.Tracks.Select(playlistTrack => playlistTrack.Track));

            TrackSource = viewModelFactory.MakeViewModel<ITrackSinkViewModel>(contentManagerService.RetrieveTracks()
                                                                                                   .Where(track => !playlistTracks.Contains(track))
                                                                                                   .Select(track => viewModelFactory.MakeViewModel<ITrackViewModel>(new PlaylistTrack(0, track))),
                                                                              "Plus");
            PlaylistTarget = viewModelFactory.MakeViewModel<ITrackSinkViewModel>(playlistViewModel.Tracks, "Minus");

            EventHandler<TracksReleasedEventArgs> MakeAddTracksEventHandler(ITrackSinkViewModel target)
            {
                return (sender, e) =>
                {
                    target.AddTracks(e.Tracks);
                    ModifiedTrackCountChanged();
                };
            }

            TrackSource.TracksReleased += MakeAddTracksEventHandler(PlaylistTarget);
            PlaylistTarget.TracksReleased += MakeAddTracksEventHandler(TrackSource);
        }

        public ITrackSinkViewModel TrackSource { get; }

        public ITrackSinkViewModel PlaylistTarget { get; }

        public int ModifiedTrackCount => TrackSource.ReleasedTracksCount + PlaylistTarget.ReleasedTracksCount;

        public bool CanApply => ModifiedTrackCount > 0;

        public void Apply()
        {
            _playlistViewModel.Remove(PlaylistTarget.ReleasedTracks);
            _playlistViewModel.Add(TrackSource.ReleasedTracks);

            TrackSource.Reset();
            PlaylistTarget.Reset();

            ModifiedTrackCountChanged();
        }

        private void ModifiedTrackCountChanged()
        {
            NotifyOfPropertyChange(() => ModifiedTrackCount);
            NotifyOfPropertyChange(() => CanApply);
        }
    }
}