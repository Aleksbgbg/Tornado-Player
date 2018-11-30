namespace Tornado.Player.Utilities
{
    using System;
    using System.Windows.Media;
    using System.Windows.Threading;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;

    internal class TornadoPlayer
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        private readonly DispatcherTimer _progressUpdateTimer;

        internal TornadoPlayer()
        {
            _progressUpdateTimer = new DispatcherTimer(TimeSpan.FromSeconds(0.1),
                                                       DispatcherPriority.Render,
                                                       (sender, e) => ProgressUpdated?.Invoke(this, new ProgressUpdatedEventArgs(Progress)),
                                                       Dispatcher.CurrentDispatcher);
            _progressUpdateTimer.Start();
        }

        internal event EventHandler MediaOpened
        {
            add => _mediaPlayer.MediaOpened += value;

            remove => _mediaPlayer.MediaOpened -= value;
        }

        internal event EventHandler MediaEnded
        {
            add => _mediaPlayer.MediaEnded += value;

            remove => _mediaPlayer.MediaEnded -= value;
        }

        internal event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        internal event EventHandler Paused;

        internal event EventHandler Played;

        internal bool IsPlaying { get; private set; }

        internal TimeSpan Progress
        {
            get => _mediaPlayer.Position;

            set => _mediaPlayer.Position = value;
        }

        internal TimeSpan Duration => _mediaPlayer.NaturalDuration.HasTimeSpan ? _mediaPlayer.NaturalDuration.TimeSpan : new TimeSpan();

        internal double Volume
        {
            get => _mediaPlayer.Volume;

            set => _mediaPlayer.Volume = value;
        }

        internal void Stop()
        {
            _mediaPlayer.Stop();
            IsPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
            _progressUpdateTimer.Stop();
        }

        internal void Play()
        {
            _mediaPlayer.Play();
            IsPlaying = true;
            Played?.Invoke(this, EventArgs.Empty);
            _progressUpdateTimer.Start();
        }

        internal void Pause()
        {
            _mediaPlayer.Pause();
            IsPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
            _progressUpdateTimer.Stop();
        }

        internal void TogglePlay()
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        internal void Open(Track track)
        {
            _mediaPlayer.Open(new Uri(track.Filepath));
            Play();
        }
    }
}