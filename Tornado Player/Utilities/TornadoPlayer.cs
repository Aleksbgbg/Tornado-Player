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
            _mediaPlayer.MediaOpened += (sender, e) => TrackChanged?.Invoke(this, new TrackChangedEventArgs(TrackIndex, _mediaPlayer.NaturalDuration.TimeSpan));
            _mediaPlayer.MediaEnded += (sender, e) =>
            {
                if (Loop)
                {
                    _mediaPlayer.Stop();
                    _mediaPlayer.Play();
                }
                else
                {
                    Next();
                }
            };

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

        internal event EventHandler<TrackChangedEventArgs> TrackChanged;

        internal event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded;

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

        internal bool Loop { get; set; }

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
                else if (value >= Tracks.Length)
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
            OnPlaylistLoaded();
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
            IsPlaying = true;
            Played?.Invoke(this, EventArgs.Empty);
        }

        internal void Pause()
        {
            _mediaPlayer.Pause();
            IsPlaying = false;
            Paused?.Invoke(this, EventArgs.Empty);
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

        internal void Switch(int index)
        {
            if (TrackIndex == index) return;

            TrackIndex = index;

            _mediaPlayer.Open(new Uri(Tracks[TrackIndex].Filepath));
            Play();
        }

        internal void Shuffle()
        {
            Random random = new Random();

            for (int index = 0; index < Tracks.Length; index++)
            {
                int swapIndex = index + random.Next(Tracks.Length - index);

                Track track = Tracks[swapIndex];
                Tracks[swapIndex] = Tracks[index];
                Tracks[index] = track;
            }

            OnPlaylistLoaded();
        }

        internal void Sort()
        {
            Array.Sort(Tracks);
            OnPlaylistLoaded();
        }

        private void OnPlaylistLoaded()
        {
            PlaylistLoaded?.Invoke(this, new PlaylistLoadedEventArgs(Tracks));
        }
    }
}