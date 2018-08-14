namespace Tornado.Player.ViewModels
{
    using System;

    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaybarViewModel : ViewModelBase, IPlaybarViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private bool _acceptingPlayerUpdates = true;

        public PlaybarViewModel(IMusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;

            _musicPlayerService.ProgressUpdated += (sender, e) =>
            {
                if (_acceptingPlayerUpdates)
                {
                    CurrentProgress = e.NewProgress;
                }
            };
            _musicPlayerService.TrackChanged += (sender, e) => Duration = e.Duration;
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

        public void DragStarted()
        {
            _acceptingPlayerUpdates = false;
        }

        public void DragCompleted()
        {
            _acceptingPlayerUpdates = true;
            _musicPlayerService.Progress = CurrentProgress;
        }
    }
}