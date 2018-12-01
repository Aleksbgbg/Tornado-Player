namespace Tornado.Player.Services
{
    using System;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.Utilities;

    internal class MusicPlayerService : IMusicPlayerService
    {
        private readonly TornadoPlayer _tornadoPlayer = new TornadoPlayer();

        public MusicPlayerService()
        {
            _tornadoPlayer.MediaOpened += (sender, e) => OnTrackChanged();
        }

        public event EventHandler Played
        {
            add => _tornadoPlayer.Played += value;

            remove => _tornadoPlayer.Played -= value;
        }

        public event EventHandler Paused
        {
            add => _tornadoPlayer.Paused += value;

            remove => _tornadoPlayer.Paused -= value;
        }

        public event EventHandler MediaEnded
        {
            add => _tornadoPlayer.MediaEnded += value;

            remove => _tornadoPlayer.MediaEnded -= value;
        }

        public event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated
        {
            add => _tornadoPlayer.ProgressUpdated += value;

            remove => _tornadoPlayer.ProgressUpdated -= value;
        }

        public event EventHandler<TrackChangedEventArgs> TrackChanged;

        public bool IsPlaying => _tornadoPlayer.IsPlaying;

        private Track _track;
        public Track Track
        {
            get => _track;

            private set
            {
                _track = value;
                _tornadoPlayer.Open(_track);
            }
        }

        public TimeSpan Progress
        {
            get => _tornadoPlayer.Progress;

            set => _tornadoPlayer.Progress = value;
        }

        public TimeSpan Duration => _tornadoPlayer.Duration;

        public double Volume
        {
            get => _tornadoPlayer.Volume;

            set => _tornadoPlayer.Volume = value;
        }

        public void Play()
        {
            _tornadoPlayer.Play();
        }

        public void Pause()
        {
            _tornadoPlayer.Pause();
        }

        public void TogglePlayback()
        {
            _tornadoPlayer.TogglePlay();
        }

        public void SelectTrack(Track track)
        {
            Track = track;
        }

        private void OnTrackChanged()
        {
            TrackChanged?.Invoke(this, new TrackChangedEventArgs(Track, Duration));
        }
    }
}