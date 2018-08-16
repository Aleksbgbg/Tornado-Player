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

        private bool _isPlaying;

        internal TornadoPlayer()
        {
            _mediaPlayer.MediaOpened += (sender, e) => TrackChanged?.Invoke(this, new TrackChangedEventArgs(TrackIndex, _mediaPlayer.NaturalDuration.TimeSpan));
            _mediaPlayer.MediaEnded += (sender, e) => Next();

            _progressUpdateTimer = new DispatcherTimer(TimeSpan.FromSeconds(0.1),
                                                       DispatcherPriority.Render,
                                                       (sender, e) => ProgressUpdated?.Invoke(this, new ProgressUpdatedEventArgs(Progress)),
                                                       Dispatcher.CurrentDispatcher);
            _progressUpdateTimer.Start();
        }

        internal event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        internal event EventHandler<TrackChangedEventArgs> TrackChanged;

        internal event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded;

        internal event EventHandler Paused;

        internal event EventHandler Played;

        internal TimeSpan Progress
        {
            get => _mediaPlayer.Position;

            set => _mediaPlayer.Position = value;
        }

        private int _trackIndex = -1;
        private int TrackIndex
        {
            get => _trackIndex;

            set
            {
                if (value < 0)
                {
                    _trackIndex = Tracks.Length - ((-value) % Tracks.Length);
                }
                else if (value > Tracks.Length)
                {
                    _trackIndex = value % Tracks.Length;
                }
                else
                {
                    _trackIndex = value;
                }
            }
        }

        internal Track[] Tracks { get; private set; }

        internal void Load(Track[] tracks)
        {
            Tracks = tracks;
            PlaylistLoaded?.Invoke(this, new PlaylistLoadedEventArgs(tracks));
            Switch(0);
        }

        internal void Previous()
        {
            Switch(TrackIndex - 1);
        }

        internal void Next()
        {
            Switch(TrackIndex + 1);
        }

        internal void Play()
        {
            _mediaPlayer.Play();
            _isPlaying = true;
            Played?.Invoke(this, EventArgs.Empty);
        }

        internal void Pause()
        {
            _mediaPlayer.Pause();
            _isPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
        }

        internal void TogglePlay()
        {
            if (_isPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        internal void Switch(int index)
        {
            if (TrackIndex == index) return;

            TrackIndex = index;

            _mediaPlayer.Open(new Uri(Tracks[TrackIndex].Filepath));
            Play();
        }
    }
}