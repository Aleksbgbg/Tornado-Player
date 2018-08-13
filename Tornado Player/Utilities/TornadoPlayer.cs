namespace Tornado.Player.Utilities
{
    using System;
    using System.Windows.Media;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;

    internal class TornadoPlayer
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        private bool _isPlaying;

        internal TornadoPlayer()
        {
            _mediaPlayer.MediaOpened += (sender, e) => TrackChanged?.Invoke(this, new TrackChangedEventArgs(TrackIndex, _mediaPlayer.NaturalDuration.TimeSpan));
            _mediaPlayer.MediaEnded += (sender, e) => Next();
        }

        internal event EventHandler<TrackChangedEventArgs> TrackChanged;

        internal event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded;

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
        }

        internal void Pause()
        {
            _mediaPlayer.Pause();
            _isPlaying = false;
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