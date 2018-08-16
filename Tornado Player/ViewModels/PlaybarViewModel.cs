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
            _musicPlayerService.TrackChanged += (sender, e) => Duration = e.Duration;

            _musicPlayerService.Paused += (sender, e) => Playing = false;
            _musicPlayerService.Played += (sender, e) => Playing = true;
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

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;

            private set
            {
                if (_duration == value) return;

                _duration = value;
                NotifyOfPropertyChange(() => Duration);
            }
        }

        private bool _playing = true;
        public bool Playing
        {
            get => _playing;

            private set
            {
                if (_playing == value) return;

                _playing = value;
                NotifyOfPropertyChange(() => Playing);
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