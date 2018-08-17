namespace Tornado.Player.ViewModels
{
    using System;

    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaybarViewModel : ViewModelBase, IPlaybarViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private bool _syncPlayer = true;

        public PlaybarViewModel(IMusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;

            _musicPlayerService.ProgressUpdated += (sender, e) =>
            {
                if (_syncPlayer)
                {
                    _syncPlayer = false;
                    CurrentProgress = e.NewProgress;
                    _syncPlayer = true;
                }
            };
            _musicPlayerService.TrackChanged += (sender, e) => NotifyOfPropertyChange(() => Duration);

            _musicPlayerService.Paused += (sender, e) => NotifyOfPropertyChange(() => Playing);
            _musicPlayerService.Played += (sender, e) => NotifyOfPropertyChange(() => Playing);
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

        public bool Loop
        {
            get => _musicPlayerService.Loop;

            set
            {
                if (Loop == value) return;

                _musicPlayerService.Loop = value;
                NotifyOfPropertyChange(() => Loop);
            }
        }

        public bool Playing => _musicPlayerService.IsPlaying;

        private bool _shuffle;
        public bool Shuffle
        {
            get => _shuffle;

            set
            {
                if (_shuffle == value) return;

                _shuffle = value;
                NotifyOfPropertyChange(() => Shuffle);

                if (_shuffle)
                {
                    _musicPlayerService.Shuffle();
                }
                else
                {
                    _musicPlayerService.Sort();
                }
            }
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
            _musicPlayerService.Previous();
        }

        public void Next()
        {
            _musicPlayerService.Next();
        }

        public void TogglePlayback()
        {
            _musicPlayerService.TogglePlayback();
        }
    }
}