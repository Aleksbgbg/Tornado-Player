namespace Tornado.Player.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistEditorViewModel : ViewModelBase, IPlaylistEditorViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;

        private readonly IContentManagerService _contentManagerService;

        public PlaylistEditorViewModel(IViewModelFactory viewModelFactory, IContentManagerService contentManagerService, IPlaylistCollectionViewModel playlistCollectionViewModel)
        {
            _viewModelFactory = viewModelFactory;
            _contentManagerService = contentManagerService;

            Playlists = playlistCollectionViewModel.Playlists;
        }

        public IObservableCollection<IPlaylistViewModel> Playlists { get; }

        private IPlaylistViewModel _selectedPlaylist;
        public IPlaylistViewModel SelectedPlaylist
        {
            get => _selectedPlaylist;

            set
            {
                if (_selectedPlaylist == value) return;

                _selectedPlaylist = value;
                NotifyOfPropertyChange(() => SelectedPlaylist);

                TrackBucket.Clear();
                PlaylistBucket.Clear();

                if (_selectedPlaylist != null)
                {
                    PlaylistBucket.AddRange(_selectedPlaylist.Tracks.Select(track => _viewModelFactory.MakeViewModel<IEditTrackViewModel>(track)));

                    Track[] playlistTracks = _selectedPlaylist.Playlist.Tracks.Select(playlistTrack => playlistTrack.Track).ToArray();

                    TrackBucket.AddRange
                    (
                             _contentManagerService.RetrieveTracks()
                                                   .Where(track => !playlistTracks.Contains(track))
                                                   .Select(track => _viewModelFactory.MakeViewModel<IEditTrackViewModel>
                                                                   (
                                                                        _viewModelFactory.MakeViewModel<ITrackViewModel>(new PlaylistTrack(0, track))
                                                                   )
                                                          )
                    );
                }
            }
        }

        public IObservableCollection<IEditTrackViewModel> TrackBucket { get; } = new BindableCollection<IEditTrackViewModel>();

        public IObservableCollection<IEditTrackViewModel> PlaylistBucket { get; } = new BindableCollection<IEditTrackViewModel>();

        public void AddSelectedTracks()
        {
            SwapSelectedItems(TrackBucket, PlaylistBucket);
        }

        public void RemoveSelectedTracks()
        {
            SwapSelectedItems(PlaylistBucket, TrackBucket);
        }

        public void AddTrack(IEditTrackViewModel track)
        {
            TrackBucket.Remove(track);
            PlaylistBucket.Add(track);
        }

        public void RemoveTrack(IEditTrackViewModel track)
        {
            PlaylistBucket.Remove(track);
            TrackBucket.Add(track);
        }

        private static void SwapSelectedItems(IObservableCollection<IEditTrackViewModel> source, IObservableCollection<IEditTrackViewModel> target)
        {
            IEditTrackViewModel[] selectedItems = source.Where(track => track.IsSelected).ToArray();

            source.RemoveRange(selectedItems);
            target.AddRange(selectedItems);
        }
    }
}