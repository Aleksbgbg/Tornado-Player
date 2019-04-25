namespace Tornado.Player.ViewModels
{
    using System;

    using Caliburn.Micro;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    public class PlaybarViewModel : ViewModelBase, IPlaybarViewModel, IHandle<IPlaylistViewModel>
    {
        private readonly IDataService _dataService;

        private readonly IMusicPlayerService _musicPlayerService;

        private bool _syncPlayer = true;

        public PlaybarViewModel(IEventAggregator eventAggregator, IDataService dataService, IHotKeyService hotKeyService, IMusicPlayerService musicPlayerService)
        {
            _dataService = dataService;
            _musicPlayerService = musicPlayerService;

            eventAggregator.Subscribe(this);

            _musicPlayerService.ProgressUpdated += (sender, e) =>
            {
                if (_syncPlayer)
                {
                    _currentProgress = e.NewProgress;
                    NotifyOfPropertyChange(nameof(CurrentProgress));
                }

                ActivePlaylist.Playlist.TrackProgress = e.NewProgress;
            };
            _musicPlayerService.TrackChanged += (sender, e) => NotifyOfPropertyChange(nameof(Duration));

            _musicPlayerService.Paused += (sender, e) => NotifyOfPropertyChange(nameof(Playing));
            _musicPlayerService.Played += (sender, e) => NotifyOfPropertyChange(nameof(Playing));

            _musicPlayerService.MediaEnded += (sender, e) =>
            {
                if (Loop)
                {
                    _musicPlayerService.Stop();
                    _musicPlayerService.Play();
                }
                else
                {
                    ActivePlaylist.SelectNext();
                }
            };

            hotKeyService.HotKeyActuated += (sender, e) =>
            {
                switch (e.Shortcut)
                {
                    case Shortcut.SkipBackward:
                        Previous();
                        break;

                    case Shortcut.SkipForward:
                        Next();
                        break;
                }
            };

            PlaybarState playbarState = dataService.Load(Constants.DataStoreNames.PlaybarState, () => new PlaybarState(0.5, false));
            Volume = playbarState.Volume;
            Loop = playbarState.Loop;
        }

        private IPlaylistViewModel _activePlaylist;
        public IPlaylistViewModel ActivePlaylist
        {
            get => _activePlaylist;

            set
            {
                if (_activePlaylist == value) return;

                _activePlaylist = value;
                NotifyOfPropertyChange(nameof(ActivePlaylist));
            }
        }

        private TimeSpan _currentProgress;
        public TimeSpan CurrentProgress
        {
            get => _currentProgress;

            set
            {
                if (_currentProgress == value) return;

                _currentProgress = value;
                NotifyOfPropertyChange(nameof(CurrentProgress));

                if (_syncPlayer)
                {
                    _musicPlayerService.Progress = value;
                }
            }
        }

        public TimeSpan Duration => _musicPlayerService.Duration;

        public double Volume
        {
            get => _musicPlayerService.Volume;

            set
            {
                _musicPlayerService.Volume = value;
                NotifyOfPropertyChange(nameof(Volume));
            }
        }

        private bool _loop;
        public bool Loop
        {
            get => _loop;

            set
            {
                if (Loop == value) return;

                _loop = value;
                NotifyOfPropertyChange(nameof(Loop));
            }
        }

        public bool Playing => _musicPlayerService.IsPlaying;

        public void SavePlaybarState()
        {
            _dataService.Save(Constants.DataStoreNames.PlaybarState, new PlaybarState(Volume, Loop));
        }

        public void Handle(IPlaylistViewModel message)
        {
            ActivePlaylist = message;
        }

        public void DragStarted()
        {
            _syncPlayer = false;
        }

        public void DragCompleted()
        {
            _syncPlayer = true;
            _musicPlayerService.Progress = CurrentProgress;
        }

        public void Previous()
        {
            ActivePlaylist.SelectPrevious();
        }

        public void Next()
        {
            ActivePlaylist.SelectNext();
        }

        public void TogglePlayback()
        {
            _musicPlayerService.TogglePlayback();
        }
    }
}