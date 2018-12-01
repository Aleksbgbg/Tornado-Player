namespace Tornado.Player.ViewModels
{
    using System;

    using Caliburn.Micro;

    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaybarViewModel : ViewModelBase, IPlaybarViewModel, IHandle<IPlaylistViewModel>
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private bool _syncPlayer = true;

        public PlaybarViewModel(IEventAggregator eventAggregator, IMusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;

            eventAggregator.Subscribe(this);

            _musicPlayerService.ProgressUpdated += (sender, e) =>
            {
                if (_syncPlayer)
                {
                    _currentProgress = e.NewProgress;
                    NotifyOfPropertyChange(() => CurrentProgress);
                }
            };
            _musicPlayerService.TrackChanged += (sender, e) => NotifyOfPropertyChange(() => Duration);

            _musicPlayerService.Paused += (sender, e) => NotifyOfPropertyChange(() => Playing);
            _musicPlayerService.Played += (sender, e) => NotifyOfPropertyChange(() => Playing);

            _musicPlayerService.MediaEnded += (sender, e) =>
            {
                if (Loop)
                {
                    _musicPlayerService.Stop();
                    _musicPlayerService.Play();
                }
                else
                {
                    ActivePlaylist.PlayNext();
                }
            };
        }

        private IPlaylistViewModel _activePlaylist;
        public IPlaylistViewModel ActivePlaylist
        {
            get => _activePlaylist;

            set
            {
                if (_activePlaylist == value) return;

                _activePlaylist = value;
                NotifyOfPropertyChange(() => ActivePlaylist);
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
                NotifyOfPropertyChange(() => CurrentProgress);

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
                NotifyOfPropertyChange(() => Volume);
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
                NotifyOfPropertyChange(() => Loop);
            }
        }

        public bool Playing => _musicPlayerService.IsPlaying;

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
            ActivePlaylist.PlayPrevious();
        }

        public void Next()
        {
            ActivePlaylist.PlayNext();
        }

        public void TogglePlayback()
        {
            _musicPlayerService.TogglePlayback();
        }
    }
}